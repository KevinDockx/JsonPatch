using Marvin.JsonPatch.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;


namespace Marvin.JsonPatch.Operations
{
    public class Operation : OperationBase
    {
        [JsonProperty("value")]
        public object value { get; set; }


        public Operation()
        {

        }



        public Operation(string op, string path, string from)
            : base(op, path, from)
        {

        }
        public Operation(string op, string path, string from, object value)
            : base (op, path, from)
        {
       
            this.value = value;
        }


        public void Apply(object objectToApplyTo)
        {
            if (OperationType == Operations.OperationType.Replace)
            {
                // find the property in "path" on T
                PropertyHelpers.FindAndSetProperty(objectToApplyTo, path, value);

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
 