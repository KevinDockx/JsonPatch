using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvin.JsonPatch.Test
{
    [TestClass]
    public class SimpleObjectAdapterTests
    {

            [TestMethod]
            public void TestAddToList()
            {
                var doc = new SimpleDTO()
                {
                    IntegerList = new List<int>() { 1, 2, 3 }
                };

                // create patch
                JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
                patchDoc.Add<int>(o => o.IntegerList, 0, 4);

                patchDoc.ApplyTo(doc);


            }
    }
}
