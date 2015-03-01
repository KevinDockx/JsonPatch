using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marvin.JsonPatch.Helpers
{
    internal class ConversionResult
    {
        public bool CanBeConverted { get; set; }
        public object ConvertedInstance { get; set; }


        public ConversionResult(bool canBeConverted, object convertedInstance)
        {
            CanBeConverted = canBeConverted;
            ConvertedInstance = convertedInstance;

        }
    }
}
