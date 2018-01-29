using Marvin.JsonPatch;
using Marvin.JsonPatch.XUnitTest.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Marvin.JsonPatch.XUnitTest.Tests
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

            Assert.Equal(deserialized.Operations.First().path, "/anothername");
        }

        [Fact]
        public void CanApplyToDifferentlyTypedClassWithPropertyMatchingJsonPropertyName()
        {
            // create patch
            JsonPatchDocument<JsonPropertyDTO> patchDocToSerialize =
                new JsonPatchDocument<JsonPropertyDTO>();
            patchDocToSerialize.Add(p => p.Name, "Kevin");

            // the patchdoc will deserialize to "anothername".  We should thus be able to apply 
            // it to a class that HAS that other property name.

            var doc = new JsonPropertyWithAnotherNameDTO()
            {
                AnotherName = "InitialValue"
            };

            var serialized = JsonConvert.SerializeObject(patchDocToSerialize);
            var deserialized =
                JsonConvert.DeserializeObject<JsonPatchDocument<JsonPropertyWithAnotherNameDTO>>
                (serialized);

            deserialized.ApplyTo(doc);

            Assert.Equal(doc.AnotherName, "Kevin");
        }

        [Fact]
        public void CanApplyToSameTypedClassWithMatchingJsonPropertyName()
        {
            // create patch
            JsonPatchDocument<JsonPropertyDTO> patchDocToSerialize =
                new JsonPatchDocument<JsonPropertyDTO>();
            patchDocToSerialize.Add(p => p.Name, "Kevin");

            // the patchdoc will deserialize to "anothername".  As JsonPropertyDTO has
            // a JsonProperty signifying that "Name" should be deseriallized from "AnotherName",
            // we should be able to apply the patchDoc.

            var doc = new JsonPropertyDTO()
            {
                Name = "InitialValue"
            };

            var serialized = JsonConvert.SerializeObject(patchDocToSerialize);
            var deserialized =
                JsonConvert.DeserializeObject<JsonPatchDocument<JsonPropertyDTO>>
                (serialized);

            deserialized.ApplyTo(doc);

            Assert.Equal(doc.Name, "Kevin");
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
