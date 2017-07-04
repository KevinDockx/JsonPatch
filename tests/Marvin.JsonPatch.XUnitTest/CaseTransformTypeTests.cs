using Marvin.JsonPatch.Helpers;
using Newtonsoft.Json;
using Xunit;

namespace Marvin.JsonPatch.XUnitTest
{
    public class CaseTransformTypeTests
    {
        [Fact]
        public void CaseTransformType_UpperCase_SerializeCorrectly()
        {
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>(CaseTransformType.UpperCase);
            patchDoc.Add<string>(o => o.StringProperty, "B");

            var result = JsonConvert.SerializeObject(patchDoc);

            Assert.Equal("[{\"value\":\"B\",\"path\":\"/STRINGPROPERTY\",\"op\":\"add\"}]", result);
        }

        [Fact]
        public void CaseTransformType_CamelCase_SerializeCorrectly()
        {
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>(CaseTransformType.CamelCase);
            patchDoc.Add<string>(o => o.StringProperty, "B");

            var result = JsonConvert.SerializeObject(patchDoc);

            Assert.Equal("[{\"value\":\"B\",\"path\":\"/stringProperty\",\"op\":\"add\"}]", result);
        }

        [Fact]
        public void CaseTransformType_Original_SerializeCorrectly()
        {
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>(CaseTransformType.OriginalCase);
            patchDoc.Add<string>(o => o.StringProperty, "B");

            var result = JsonConvert.SerializeObject(patchDoc);

            Assert.Equal("[{\"value\":\"B\",\"path\":\"/StringProperty\",\"op\":\"add\"}]", result);
        }

        [Fact]
        public void CaseTransformType_LowerCase_IsDefaultAndSerializeCorrectly()
        {
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Add<string>(o => o.StringProperty, "B");

            var result = JsonConvert.SerializeObject(patchDoc);

            Assert.Equal("[{\"value\":\"B\",\"path\":\"/stringproperty\",\"op\":\"add\"}]", result);
        }
    }
}