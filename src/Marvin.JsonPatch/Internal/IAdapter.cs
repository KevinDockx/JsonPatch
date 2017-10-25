using Newtonsoft.Json.Serialization;

namespace Marvin.JsonPatch.Internal
{
    public interface IAdapter
    {
        bool TryTraverse(
            object target,
            string segment,
            IContractResolver contractResolver,
            out object nextTarget,
            out string errorMessage);

        bool TryAdd(
            object target,
            string segment,
            IContractResolver contractResolver,
            object value,
            out string errorMessage);

        bool TryRemove(
            object target,
            string segment,
            IContractResolver contractResolver,
            out string errorMessage);

        bool TryGet(
            object target,
            string segment,
            IContractResolver contractResolver,
            out object value,
            out string errorMessage);

        bool TryReplace(
            object target,
            string segment,
            IContractResolver contractResolver,
            object value,
            out string errorMessage);
    }
}
