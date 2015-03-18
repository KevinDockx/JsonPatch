using Marvin.JsonPatch.Dynamic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Marvin.JsonPatch.Dynamic.XUnitTest
{
    public class DynamicObjectAdapterTests
    {


        [Fact]
        public void AddNewProperty()
        {

            dynamic obj = new
            {
                Test = 1
            };

            // create patch
            JsonPatchDocument patchDoc = new JsonPatchDocument();
            patchDoc.Add<int>("NewInt", 1);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument>(serialized);


            var newObject = deserialized.CreateFrom(obj);

            Assert.Equal(1, newObject.newint);
            Assert.Equal(1, newObject.Test);


        }


    }
}
