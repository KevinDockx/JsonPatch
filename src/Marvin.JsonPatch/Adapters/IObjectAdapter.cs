// Any comments, input: @KevinDockx
// Any issues, requests: https://github.com/KevinDockx/JsonPatch
//
// Enjoy :-)

using Marvin.JsonPatch.Operations;

namespace Marvin.JsonPatch.Adapters
{
    public interface IObjectAdapter
    {
        void Add(Operation operation, object objectToApplyTo);
        void Copy(Operation operation, object objectToApplyTo);
        void Move(Operation operation, object objectToApplyTo);
        void Remove(Operation operation, object objectToApplyTo);
        void Replace(Operation operation, object objectToApplyTo);
    }

   
}
