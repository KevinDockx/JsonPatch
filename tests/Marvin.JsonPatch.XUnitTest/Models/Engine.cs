using Newtonsoft.Json;

namespace Marvin.JsonPatch.XUnitTest.Models
{
    public class Engine
    {      
        public float HorsePower { get; set; }

        [JsonProperty(Required = Required.Always)]
        public float HorsePowerAlwaysRequired { get; set; }
    }
}
