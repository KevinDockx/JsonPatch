using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Marvin.JsonPatch.Test
{
    public class SimpleDTO
    {
        public List<int> IntegerList { get; set; }
        public int IntegerValue { get; set; }
        public string StringProperty { get; set; }
        public string AnotherStringProperty { get; set; }
        public decimal DecimalValue { get; set; }

        public double DoubleValue { get; set; }

        public float FloatValue { get; set; }
    }
}
