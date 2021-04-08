using System.Collections.Generic;

namespace Marvin.JsonPatch.XUnitTest.Models
{
#if !NET40

    internal class DerivedDTO : SimpleDTO
    {
        public new IReadOnlyCollection<int> IntegerList { get; set; }
    }
#endif
}
