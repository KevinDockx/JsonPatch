// Any comments, input: @KevinDockx
// Any issues, requests: https://github.com/KevinDockx/JsonPatch
//
// Enjoy :-)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marvin.JsonPatch.Helpers
{
    internal class ActualPropertyPathResult
    {
        public int NumericEnd { get; private set; }
        public string PathToProperty { get; set; }
        public bool ExecuteAtEnd { get; set; }

        public ActualPropertyPathResult(
            int numericEnd,
            string pathToProperty,
            bool executeAtEnd)
        {
            NumericEnd = numericEnd;
            PathToProperty = pathToProperty;
            ExecuteAtEnd = executeAtEnd;
        }
    }
}
