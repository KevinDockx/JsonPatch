using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvin.JsonPatch.XUnitTest
{
    public class DtoWithDerivedListProperty
    {
        public DerivedList DerivedList { get; set; }

        public DerivedListOfT<int> DerivedListOfT { get; set; }

    }
}
