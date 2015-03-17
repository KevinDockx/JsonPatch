using Marvin.JsonPatch.Adapters;
using Marvin.JsonPatch.Operations;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Marvin.JsonPatch.Dynamic
{
    public class JsonPatchDocument
    {

        public List<Operation> Operations { get; private set; }


        public JsonPatchDocument()
        {
            Operations = new List<Operation>();
        }


        // Create from list of operations  
        public JsonPatchDocument(List<Operation> operations)
        {
            Operations = operations;

        }

        public JsonPatchDocument Add<TProp>(string path, TProp value)
        {
            Operations.Add(new Operation("add",path.ToLower(), null, value));
            return this;
        }


        public void ApplyTo(dynamic objectToApplyTo)
        {
            ApplyTo(objectToApplyTo, new DynamicObjectAdapter());
        }


        public void ApplyTo(dynamic objectToApplyTo, IDynamicObjectAdapter adapter)
        {
            dynamic expandoObjectToApplyTo = new ExpandoObject();
            var propertyDictionary = (IDictionary<String, Object>)(expandoObjectToApplyTo);
           
            foreach (PropertyInfo propertyInfo in
                objectToApplyTo.GetType().GetProperties())
            {
                propertyDictionary[propertyInfo.Name] = propertyInfo.GetValue(objectToApplyTo, null);
            }

            // apply each operation in order
            foreach (var op in Operations)
            {
                op.Apply(expandoObjectToApplyTo, adapter);
            }

            objectToApplyTo = expandoObjectToApplyTo;
        }

    }
}
