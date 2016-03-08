using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Marvin.JsonPatch.Exceptions;
using Newtonsoft.Json.Serialization;

namespace Marvin.JsonPatch
{
    public class DefaultJsonPatchPropertyResolver : IJsonPatchPropertyResolver
    {
        private readonly IContractResolver _contractResolver;
        private readonly Dictionary<Type, JsonContract> _contractsCache;
        private readonly Dictionary<MemberInfo, string> _namesCache;
        private readonly Dictionary<Tuple<Type, string>, MemberInfo> _membersCache;

        public DefaultJsonPatchPropertyResolver()
            : this(null)
        {
        }

        public DefaultJsonPatchPropertyResolver(IContractResolver contractResolver)
        {
            _contractResolver = contractResolver ?? new DefaultContractResolver();
            _contractsCache = new Dictionary<Type, JsonContract>();
            _namesCache = new Dictionary<MemberInfo, string>();
            _membersCache = new Dictionary<Tuple<Type, string>, MemberInfo>();
        }

        public string GetName(MemberInfo member)
        {
            string name;
            if (!_namesCache.TryGetValue(member, out name))
            {
                name = GetNameCore(member);
                _namesCache[member] = name;
                _membersCache[Tuple.Create(member.DeclaringType, name)] = member;
            }
            return name;
        }

        public MemberInfo GetMember(Type type, string name)
        {
            var key = Tuple.Create(type, name);
            MemberInfo member;
            if (!_membersCache.TryGetValue(key, out member))
            {
                member = GetMemberCore(type, name);
                _membersCache[key] = member;
                _namesCache[member] = name;
            }
            return member;
        }

        private JsonContract GetContract(Type type)
        {
            JsonContract contract;
            if (!_contractsCache.TryGetValue(type, out contract))
            {
                contract = _contractResolver.ResolveContract(type);
                _contractsCache[type] = contract;
            }
            return contract;
        }

        private string GetNameCore(MemberInfo member)
        {
            var contract = GetContract(member.DeclaringType) as JsonObjectContract;
            if (contract == null)
                throw new JsonPatchException(string.Format("The type '{0}' isn't an object and doesn't have properties", member.DeclaringType), null);

            var jsonProperty = contract.Properties.FirstOrDefault(p => p.UnderlyingName == member.Name);
            if (jsonProperty == null)
                throw new JsonPatchException(string.Format("Cannot find name for property '{0}'", member), null);

            return jsonProperty.PropertyName;
        }

        private MemberInfo GetMemberCore(Type type, string name)
        {
            var contract = GetContract(type) as JsonObjectContract;
            if (contract == null)
                throw new JsonPatchException(string.Format("The type '{0}' isn't an object and doesn't have properties", type), null);

            var jsonProperty = contract.Properties.FirstOrDefault(p => p.PropertyName == name);

            if (jsonProperty != null)
            {
                var prop = jsonProperty.DeclaringType.GetProperty(name, BindingFlags.Instance | BindingFlags.Public);
                if (prop != null)
                    return prop;
            }
            throw new JsonPatchException(string.Format("Cannot find property with the name '{0}' on the type '{1}'", name, type), null);
        }
    }
}
