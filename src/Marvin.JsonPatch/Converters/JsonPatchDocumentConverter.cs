// Any comments, input: @KevinDockx
// Any issues, requests: https://github.com/KevinDockx/JsonPatch
//
// Enjoy :-)

using Marvin.JsonPatch.Exceptions;
using Marvin.JsonPatch.Operations;
using Marvin.JsonPatch.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace Marvin.JsonPatch.Converters
{
    public class JsonPatchDocumentConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (objectType != typeof(JsonPatchDocument))
            {
                throw new ArgumentException(Resources.FormatParameterMustMatchType("objectType", "JsonPatchDocumentNew"), "objectType");
            }

            try
            {
                if (reader.TokenType == JsonToken.Null)
                {
                    return null;
                }

                // load jObject
                var jObject = JArray.Load(reader);

                // Create target object for Json => list of operations
                var targetOperations = new List<Operation>();

                // Create a new reader for this jObject, and set all properties 
                // to match the original reader.
                var jObjectReader = jObject.CreateReader();
                jObjectReader.Culture = reader.Culture;
                jObjectReader.DateParseHandling = reader.DateParseHandling;
                jObjectReader.DateTimeZoneHandling = reader.DateTimeZoneHandling;
                jObjectReader.FloatParseHandling = reader.FloatParseHandling;

                // Populate the object properties
                serializer.Populate(jObjectReader, targetOperations);

                // container target: the JsonPatchDocument. 
                var container = new JsonPatchDocument(targetOperations, new DefaultContractResolver());

                return container;
            }
            catch (Exception ex)
            {
                throw new JsonPatchException(Resources.FormatInvalidJsonPatchDocument(objectType.Name), ex);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is IJsonPatchDocument)
            {
                var jsonPatchDoc = (IJsonPatchDocument)value;
                var lst = jsonPatchDoc.GetOperations();

                // write out the operations, no envelope
                serializer.Serialize(writer, lst);
            }
        }
    }
}
