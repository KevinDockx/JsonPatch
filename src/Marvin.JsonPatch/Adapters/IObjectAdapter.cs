// Any comments, input: @KevinDockx
// Any issues, requests: https://github.com/KevinDockx/JsonPatch
//
// Enjoy :-)

using System;
namespace Marvin.JsonPatch.Adapters
{
    public interface IObjectAdapter<T>
     where T : class
    {
        void Add(Marvin.JsonPatch.Operations.Operation<T> operation, T objectToApplyTo, IJsonPatchPropertyResolver resolver);
        void Copy(Marvin.JsonPatch.Operations.Operation<T> operation, T objectToApplyTo, IJsonPatchPropertyResolver resolver);
        void Move(Marvin.JsonPatch.Operations.Operation<T> operation, T objectToApplyTo, IJsonPatchPropertyResolver resolver);
        void Remove(Marvin.JsonPatch.Operations.Operation<T> operation, T objectToApplyTo, IJsonPatchPropertyResolver resolver);
        void Replace(Marvin.JsonPatch.Operations.Operation<T> operation, T objectToApplyTo, IJsonPatchPropertyResolver resolver);
        void Test(Marvin.JsonPatch.Operations.Operation<T> operation, T objectToApplyTo, IJsonPatchPropertyResolver resolver);
    }

   
}
