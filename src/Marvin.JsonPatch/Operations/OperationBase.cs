// Any comments, input: @KevinDockx
// Any issues, requests: https://github.com/KevinDockx/JsonPatch
//
// Enjoy :-)

using Newtonsoft.Json;
using System;

namespace Marvin.JsonPatch.Operations
{
    public class OperationBase
    {
        private string _op;
        private OperationType _operationType;

        [JsonIgnore]
        public OperationType OperationType
        {
            get
            {
                return _operationType;
            }
        }

        [JsonProperty("value")]
        public object value { get; set; }

        [JsonProperty("path")]
        public string path { get; set; }

        [JsonProperty("op")]
        public string op
        {
            get
            {
                return _op;
            }
            set
            {
                OperationType result;
                if (!Enum.TryParse(value, ignoreCase: true, result: out result))
                {
                    result = OperationType.Invalid;
                }
                _operationType = result;
                _op = value;
            }
        }

        [JsonProperty("from")]
        public string from { get; set; }
        
        public OperationBase()
        {
        }

        public OperationBase(string op, string path, string from)
        {
            this.op = op;
            this.path = path;
            this.from = from;
        }
     
         public OperationBase(string op, string path, string from, object value)
             : this(op, path, from)
         {
             this.value = value;
         } 

        public bool ShouldSerializefrom()
        {
            return (OperationType == Operations.OperationType.Move
                || OperationType == OperationType.Copy);
        }
        
        public bool ShouldSerializevalue()
        {
            return (OperationType == Operations.OperationType.Add
                || OperationType == OperationType.Replace
                || OperationType == OperationType.Test);
        }
    }
}
