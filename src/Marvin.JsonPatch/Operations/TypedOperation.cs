using Marvin.JsonPatch.Adapters;
using Marvin.JsonPatch.Exceptions;
using Marvin.JsonPatch.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Marvin.JsonPatch.Operations
{



    public class Operation<T> : OperationBase where T : class  
    {

        [JsonProperty("value")]
        public object value { get; set; }


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



        public void Apply(T objectToApplyTo, IObjectAdapter<T> adapter)
        {
            switch (OperationType)
            {
                case OperationType.Add:
                    adapter.Add(this, objectToApplyTo);
                    break;
                case OperationType.Remove:
                    adapter.Remove(this, objectToApplyTo);
                    break;
                case OperationType.Replace:
                    adapter.Replace(this, objectToApplyTo);
                    break;
                case OperationType.Move:
                    adapter.Move(this, objectToApplyTo);
                    break;
                case OperationType.Copy:
                    adapter.Copy(this, objectToApplyTo);
                    break;
                case OperationType.Test:
                    adapter.Test(this, objectToApplyTo);
                    break;
                default:
                    break;
            }
        }




        public bool ShouldSerializevalue()
        {
            return (OperationType == Operations.OperationType.Add
                || OperationType == OperationType.Replace
                || OperationType == OperationType.Test);
        }


    }
}
