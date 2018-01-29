using System.Collections.Generic;

namespace Marvin.JsonPatch.XUnitTest.Models
{
    internal class DerivedDTO : SimpleDTO
    {
        public new IReadOnlyCollection<int> IntegerList { get; set; } 
    }
}
