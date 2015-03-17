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



         public void Apply(dynamic objectToApplyTo, IDynamicObjectAdapter adapter)
         {
             switch (OperationType)
             {
                 case OperationType.Add:
                     adapter.Add(this, objectToApplyTo);
                     break;
                 //case OperationType.Remove:
                 //    adapter.Remove(this, objectToApplyTo);
                 //    break;
                 //case OperationType.Replace:
                 //    adapter.Replace(this, objectToApplyTo);
                 //    break;
                 //case OperationType.Move:
                 //    adapter.Move(this, objectToApplyTo);
                 //    break;
                 //case OperationType.Copy:
                 //    adapter.Copy(this, objectToApplyTo);
                 //    break;
                 //case OperationType.Test:
                 //    adapter.Test(this, objectToApplyTo);
                 //    break;
                 default:
                     break;
             }
         }

         

    }
      

    }
 