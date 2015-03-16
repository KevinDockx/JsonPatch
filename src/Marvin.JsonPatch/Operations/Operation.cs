// Kevin Dockx
//
// Any comments, input: @KevinDockx
// Any issues, requests: https://github.com/KevinDockx/JsonPatch
//
// Enjoy :-)

using Marvin.JsonPatch.Adapters;
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


         public Operation(string op, string path, string from, object value)
             : base(op, path, from)
         {
             this.value = value;
         }

         public Operation(string op, string path, string from)
             : base(op, path, from)
         {

         }
         


    }
      

    }
 