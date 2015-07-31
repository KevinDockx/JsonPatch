// Any comments, input: @KevinDockx
// Any issues, requests: https://github.com/KevinDockx/JsonPatch
//
// Enjoy :-)
using System.Reflection;

namespace Marvin.JsonPatch.Helpers
{
    internal class SetValueResult
    {
        public bool CanSet { get; private set; }

        public bool Success { get; private set; }

        public PropertyInfo PropertyToSet { get; private set; }

        public SetValueResult(PropertyInfo propertyToSet, bool canSet, bool success)
        {
            PropertyToSet = propertyToSet;
            CanSet = canSet;
            Success = success;
        }
    }
}
