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
            var doc = new
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<dynamic> patchDoc = new JsonPatchDocument<dynamic>();
            patchDoc.Add<int>("IntegerList", 4, 1);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<dynamic>>(serialized);


            deserialized.ApplyTo(doc);

            Assert.Equal(new List<int>() { 1, 2, 3, 4 }, doc.IntegerList);


        }


    }
}
