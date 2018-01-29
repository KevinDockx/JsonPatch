using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvin.JsonPatch.XUnitTest.Models
{
    public class DerivedList : List<string>
    {
    }

    public class DerivedListOfT<T> : List<T>
    {
    }
}
