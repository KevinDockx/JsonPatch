using System.Collections.Generic;

namespace Marvin.JsonPatch.XUnitTest
{
    internal class DerivedDTO : SimpleDTO
    {
        public new IReadOnlyCollection<int> IntegerList { get; set; } 
    }
}
