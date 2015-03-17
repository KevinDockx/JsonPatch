using Marvin.JsonPatch.Dynamic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Marvin.JsonPatch.XUnitTest
{
    public class DynamicObjectAdapterTests
    {


        [Fact]
        public void AddToListAtEndWithSerialization()
        {
            dynamic doc = new
            {
                Test = 1
            };

            // create patch
            JsonPatchDocument patchDoc = new JsonPatchDocument();
            patchDoc.Add<int>("NewInt", 1);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument>(serialized);


            deserialized.ApplyTo(doc);

            Assert.Equal(1, doc.NewInt);
            Assert.Equal(1, doc.Test);


        }


    }
}
