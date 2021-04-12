﻿using Marvin.JsonPatch.XUnitTest.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit;

namespace Marvin.JsonPatch.XUnitTest.Tests
{
    public class ObjectAdapterTestsOnDerived
    {
        #if !NET40
        [Fact]
        public void ReplacePropertyWithNewKeyword()
        {
            var doc = new DerivedDTO();
            var replacementCollection = new ReadOnlyCollection<int>(new List<int>() { 1, 2, 3, 4 });
            JsonPatchDocument<DerivedDTO> patchDoc = new JsonPatchDocument<DerivedDTO>();
            patchDoc.Replace(o => o.IntegerList, replacementCollection);
            patchDoc.ApplyTo(doc);
            Assert.Equal(replacementCollection, doc.IntegerList);
        }
        #endif
    }
}
