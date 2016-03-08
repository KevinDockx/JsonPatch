using System;
using System.Reflection;

namespace Marvin.JsonPatch
{
    public interface IJsonPatchPropertyResolver
    {
        string GetName(MemberInfo member);
        MemberInfo GetMember(Type type, string name);
    }
}
