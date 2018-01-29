using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvin.JsonPatch.XUnitTest.Models
{
    public class JsonPropertyDTO
    {
        [JsonProperty("AnotherName")]
        public string Name { get; set; }
    }


    public class JsonPropertyWithAnotherNameDTO
    {    
        public string AnotherName { get; set; }
    }
}
