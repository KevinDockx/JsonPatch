using Newtonsoft.Json;

namespace Marvin.JsonPatch.XUnitTest
{
    public class Car
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Engine Engine { get; set; }
    }
}
