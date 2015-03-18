using Marvin.JsonPatch.Adapters;
using Marvin.JsonPatch.Operations;
using Newtonsoft.Json;
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


        /// <summary>
        /// Apply the patch document, and return a new ExpandoObject (dynamic) with the change applied.
        /// </summary>
        /// <param name="objectToCreateNewObjectFrom">The object to start from</param>
        public dynamic CreateFrom(dynamic objectToCreateNewObjectFrom)
        {
            return CreateFrom(objectToCreateNewObjectFrom, new DynamicObjectAdapter());
        }

        /// <summary>
        /// Apply the patch document, passing in a custom IObjectAdapter<typeparamref name=">"/>, 
        /// and return a new ExpandoObject (dynamic) with the change applied.
        /// </summary>
        /// <param name="objectToCreateNewObjectFrom">The object to start from</param>
        /// <param name="adapter">The IObjectAdapter instance to use</param>
        /// <returns></returns>
        public dynamic CreateFrom(dynamic objectToCreateNewObjectFrom, IDynamicObjectAdapter adapter)
        {

            dynamic clonedObject = JsonConvert.DeserializeObject<ExpandoObject>
                (JsonConvert.SerializeObject(objectToCreateNewObjectFrom));

            dynamic expandoObjectToApplyTo = new ExpandoObject();
            var propertyDictionary = (IDictionary<String, Object>)(expandoObjectToApplyTo);

            foreach (PropertyInfo propertyInfo in
                objectToCreateNewObjectFrom.GetType().GetProperties())
            {
                propertyDictionary[propertyInfo.Name] = propertyInfo.GetValue(objectToCreateNewObjectFrom, null);
            }

            // apply each operation in order
            foreach (var op in Operations)
            {
                op.Apply(expandoObjectToApplyTo, adapter);
            }

            return expandoObjectToApplyTo;

        }
         

         

    }
}
