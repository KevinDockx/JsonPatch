using Marvin.JsonPatch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Marvin.JsonPatch.XUnitTest
{
    public class JsonPropertyTests
    {
        [Fact]
        public void HonourJsonPropertyOnSerialization()
        {
            // create patch
            JsonPatchDocument<JsonPropertyDTO> patchDoc = new JsonPatchDocument<JsonPropertyDTO>();
            patchDoc.Add(p => p.Name, "Kevin");

            var serialized = JsonConvert.SerializeObject(patchDoc);
            // serialized value should have "AnotherName" as path
            // deserialize to a JsonPatchDocument<JsonPropertyWithAnotherNameDTO> to check
            var deserialized = 
                JsonConvert.DeserializeObject<JsonPatchDocument<JsonPropertyWithAnotherNameDTO>>(serialized);

            Assert.Equal(deserialized.Operations.First().path, "AnotherName");
        }
        
        [Fact]
        public void HonourJsonPropertyOnApplyForAdd()
        {
            var doc = new JsonPropertyDTO()
            {
                Name = "InitialValue"
            };

            // create patch
            //var patchDoc = new JsonPatchDocument<JsonPropertyDTO>();
            //patchDoc.Add(p => p.Name, "Kevin");

            // serialization should serialize to "AnotherName"
            var serialized = "[{\"value\":\"Kevin\",\"path\":\"/AnotherName\",\"op\":\"add\"}]";
            // that means we can deserialize to JsonPatchDocument<JsonPropertyWithAnotherNameDTO>      
            var deserialized = 
                JsonConvert.DeserializeObject<JsonPatchDocument<JsonPropertyDTO>>(serialized);

            deserialized.ApplyTo(doc);

            Assert.Equal("Kevin", doc.Name);
        }


        [Fact]
        public void HonourJsonPropertyOnApplyForRemove()
        {
            var doc = new JsonPropertyDTO()
            {
                Name = "InitialValue"
            };

            // create patch
            //var patchDoc = new JsonPatchDocument<JsonPropertyDTO>();
            //patchDoc.Add(p => p.Name, "Kevin");

            // serialization should serialize to "AnotherName"
            var serialized = "[{\"path\":\"/AnotherName\",\"op\":\"remove\"}]";
            // that means we can deserialize to JsonPatchDocument<JsonPropertyWithAnotherNameDTO>      
            var deserialized =
                JsonConvert.DeserializeObject<JsonPatchDocument<JsonPropertyDTO>>(serialized);

            deserialized.ApplyTo(doc);

            Assert.Equal(null, doc.Name);
        }

    }
}
