// Any comments, input: @KevinDockx
// Any issues, requests: https://github.com/KevinDockx/JsonPatch
//
// Enjoy :-)

using Marvin.JsonPatch.Adapters;
using System;

namespace Marvin.JsonPatch.Operations
{
    public class Operation<T> : Operation where T : class
    {   
        public Operation()
        {
        }

        public Operation(string op, string path, string from, object value)
            : base(op, path, from)
        {
            this.value = value;
        }

        public Operation(string op, string path, string from)
            : base(op, path, from)
        {
        } 

        internal void Apply(T objectToApplyTo, IObjectAdapter<T> adapter, IJsonPatchPropertyResolver resolver)
        {
            switch (OperationType)
            {
                case OperationType.Add:
                    adapter.Add(this, objectToApplyTo, resolver);
                    break;
                case OperationType.Remove:
                    adapter.Remove(this, objectToApplyTo, resolver);
                    break;
                case OperationType.Replace:
                    adapter.Replace(this, objectToApplyTo, resolver);
                    break;
                case OperationType.Move:
                    adapter.Move(this, objectToApplyTo, resolver);
                    break;
                case OperationType.Copy:
                    adapter.Copy(this, objectToApplyTo, resolver);
                    break;
                case OperationType.Test:
                    adapter.Test(this, objectToApplyTo, resolver);
                    break;
                default:
                    break;
            }
        }
    }
}
