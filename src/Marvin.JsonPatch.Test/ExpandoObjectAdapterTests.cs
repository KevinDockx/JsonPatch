using Marvin.JsonPatch.Adapters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvin.JsonPatch.Test
{
    [TestClass]
    public class ExpandoObjectAdapterTests
    {

        [TestMethod]
        public void Remove()
        {
            dynamic doc = new ExpandoObject();
            doc.StringProperty = "A";
          
            // create patch
            JsonPatchDocument patchDoc = new JsonPatchDocument();
            patchDoc.Remove("StringProperty");

            patchDoc.ApplyTo(doc, new ExpandoObjectAdapter());

            Assert.AreEqual(null, doc.StringProperty);

        }

    }
}
