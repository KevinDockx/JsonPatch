using System;
using System.Reflection;
using Marvin.JsonPatch.Exceptions;

namespace Marvin.JsonPatch
{
    public class DefaultJsonPatchPropertyResolver : IJsonPatchPropertyResolver
    {
        public string GetName(MemberInfo member)
        {
            return member.Name.ToLowerInvariant();
        }

        public MemberInfo GetMember(Type type, string name)
        {
            var member = type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            
            if (member == null)
                throw new JsonPatchException(string.Format("Cannot find property with the name '{0}' on the type '{1}'", name, type), null);

            return member;
        }
    }
}