using Newtonsoft.Json;

namespace Marvin.JsonPatch.XUnitTest
{
    public class Engine
    {      
        public float HorsePower { get; set; }

        [JsonProperty(Required = Required.Always)]
        public float HorsePowerAlwaysRequired { get; set; }
    }
}
