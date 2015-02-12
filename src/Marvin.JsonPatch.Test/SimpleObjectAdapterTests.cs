using Marvin.JsonPatch.Converters;
using Marvin.JsonPatch.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvin.JsonPatch.Test
{
    [TestClass]
    public class SimpleObjectAdapterTests
    {

        [TestMethod]
        public void AddResultsInReplace()
        {
            var doc = new SimpleDTO()
            {
                StringProperty = "A"
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Add<string>(o => o.StringProperty, "B");

            patchDoc.ApplyTo(doc);

            Assert.AreEqual("B", doc.StringProperty);
          
        }


        [TestMethod]
        public void AddToList()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Add<int>(o => o.IntegerList, 4, 0);

            patchDoc.ApplyTo(doc);

            CollectionAssert.AreEquivalent(new List<int>() { 4, 1, 2, 3 }, doc.IntegerList);
        }


        [TestMethod]
        public void AddToListInvalidPositionTooLarge()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Add<int>(o => o.IntegerList, 4, 3);


            try
            {
                patchDoc.ApplyTo(doc);

                // if we get here, we should fail b/c no exception was thrown
                Assert.Fail();

            }
            catch (JsonPatchException) { }

        }


        [TestMethod]
        public void AddToListInvalidPositionTooSmall()
        {

            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Add<int>(o => o.IntegerList, 4, -1);

            try
            {
                patchDoc.ApplyTo(doc);

                // if we get here, we should fail b/c no exception was thrown
                Assert.Fail();

            }
            catch (JsonPatchException) 
            { 
            }



        }

        [TestMethod]
        public void AddToListAppend()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Add<int>(o => o.IntegerList, 4);

            patchDoc.ApplyTo(doc);

            CollectionAssert.AreEquivalent(new List<int>() { 1, 2, 3, 4 }, doc.IntegerList);

        }


        [TestMethod]
        public void Remove()
        {
            var doc = new SimpleDTO()
            {
                StringProperty = "A"
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Remove<string>(o => o.StringProperty); 

            patchDoc.ApplyTo(doc);

            Assert.AreEqual(null, doc.StringProperty);

        }



        [TestMethod]
        public void RemoveFromList()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Remove<int>(o => o.IntegerList, 2);

            patchDoc.ApplyTo(doc);

            CollectionAssert.AreEquivalent(new List<int>() { 1, 2 }, doc.IntegerList);
        }


        [TestMethod]
        public void RemoveFromListInvalidPositionTooLarge()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Remove<int>(o => o.IntegerList, 3);


            try
            {
                patchDoc.ApplyTo(doc);

                // if we get here, we should fail b/c no exception was thrown
                Assert.Fail();

            }
            catch (JsonPatchException) { }

        }


        [TestMethod]
        public void RemoveFromListInvalidPositionTooSmall()
        {

            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Remove<int>(o => o.IntegerList,-1);

            try
            {
                patchDoc.ApplyTo(doc);

                // if we get here, we should fail b/c no exception was thrown
                Assert.Fail();

            }
            catch (JsonPatchException)
            {
            }

        }


        [TestMethod]
        public void RemoveFromEndOfList()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Remove<int>(o => o.IntegerList);

            patchDoc.ApplyTo(doc);

            CollectionAssert.AreEquivalent(new List<int>() { 1, 2 }, doc.IntegerList);

        }


        [TestMethod]
        public void Replace()
        {
            var doc = new SimpleDTO()
            {
                StringProperty = "A",
                DecimalValue = 10
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Replace<string>(o => o.StringProperty, "B");
          //  patchDoc.Replace<decimal>(o => o.DecimalValue, 12);
            patchDoc.Replace(o => o.DecimalValue, 12);

            patchDoc.ApplyTo(doc);

            Assert.AreEqual("B", doc.StringProperty);
            Assert.AreEqual(12, doc.DecimalValue);

 

        }

        [TestMethod]
        public void SerializationMustNotIncudeEnvelope()
        {
            var doc = new SimpleDTO()
            {
                StringProperty = "A",
                DecimalValue = 10,
                DoubleValue = 10,
                FloatValue = 10,
                IntegerValue = 10
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Replace(o => o.StringProperty, "B");
            patchDoc.Replace(o => o.DecimalValue, 12);
            patchDoc.Replace(o => o.DoubleValue, 12);
            patchDoc.Replace(o => o.FloatValue, 12);
            patchDoc.Replace(o => o.IntegerValue, 12);
                       
            var serialized = JsonConvert.SerializeObject(patchDoc); 

            Assert.AreEqual(false, serialized.Contains("operations"));
            Assert.AreEqual(false, serialized.Contains("Operations"));
     

        }

  

        [TestMethod]
        public void DeserializationMustWorkWithoutEnvelope()
        {
            var doc = new SimpleDTO()
            {
                StringProperty = "A",
                DecimalValue = 10,
                DoubleValue = 10,
                FloatValue = 10,
                IntegerValue = 10
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Replace(o => o.StringProperty, "B");
            patchDoc.Replace(o => o.DecimalValue, 12);
            patchDoc.Replace(o => o.DoubleValue, 12);
            patchDoc.Replace(o => o.FloatValue, 12);
            patchDoc.Replace(o => o.IntegerValue, 12);

            // default: no envelope
            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            Assert.IsInstanceOfType(deserialized, typeof(JsonPatchDocument<SimpleDTO>));
     
        }


        [TestMethod]
        public void DeserializationMustFailWithEnvelope()
        {
          
            try
            {
                string serialized = "{\"Operations\": [{ \"op\": \"replace\", \"path\": \"/title\", \"value\": \"New Title\"}]}";
                var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(JsonPatchException));
            }
        }



        [TestMethod]
        public void SerializationTests()
        {
            var doc = new SimpleDTO()
            {
                StringProperty = "A",
                DecimalValue = 10,
                DoubleValue = 10,
             FloatValue = 10,
             IntegerValue = 10
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Replace(o => o.StringProperty, "B");
            patchDoc.Replace(o => o.DecimalValue, 12);
            patchDoc.Replace(o => o.DoubleValue, 12);
            patchDoc.Replace(o => o.FloatValue, 12);
            patchDoc.Replace(o => o.IntegerValue, 12);



            // serialize & deserialize 
            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserizalized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);


            deserizalized.ApplyTo(doc);

            Assert.AreEqual("B", doc.StringProperty);
            Assert.AreEqual(12, doc.DecimalValue);
            Assert.AreEqual(12, doc.DoubleValue);
            Assert.AreEqual(12, doc.FloatValue);
            Assert.AreEqual(12, doc.IntegerValue);
             
        }

        [TestMethod]
        public void ReplaceInList()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Replace<int>(o => o.IntegerList, 5, 0);

            patchDoc.ApplyTo(doc);


            CollectionAssert.AreEquivalent(new List<int>() { 5, 2, 3 }, doc.IntegerList);

        }



        [TestMethod]
        public void ReplaceFullList()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Replace<List<int>>(o => o.IntegerList, new List<int>() { 4, 5, 6 });

            patchDoc.ApplyTo(doc);


            CollectionAssert.AreEquivalent(new List<int>() { 4, 5, 6 }, doc.IntegerList);

        }



        [TestMethod]
        public void ReplaceFullListFromEnumerable()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Replace<IEnumerable<int>>(o => o.IntegerList, new List<int>() { 4, 5, 6 });

            patchDoc.ApplyTo(doc);


            CollectionAssert.AreEquivalent(new List<int>() { 4, 5, 6 }, doc.IntegerList);

        }



        [TestMethod]
        public void ReplaceFullListWithCollection()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Replace<IEnumerable<int>>(o => o.IntegerList, new Collection<int>() { 4, 5, 6 });


            try
            {
                // should trhow an exception due to list/collection cast.

                patchDoc.ApplyTo(doc);

                // if we get here, we should fail b/c no exception was thrown
                Assert.Fail();

            }
            catch (JsonPatchException)
            {
            }

        }



        [TestMethod]
        public void ReplaceAtEndOfList()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Replace<int>(o => o.IntegerList, 5);

            patchDoc.ApplyTo(doc);


            CollectionAssert.AreEquivalent(new List<int>() { 1, 2, 5 }, doc.IntegerList);

        }

        [TestMethod]
        public void ReplaceInListInvalidInvalidPositionTooLarge()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Replace<int>(o => o.IntegerList, 5, 3);


            try
            {
                patchDoc.ApplyTo(doc);

                // if we get here, we should fail b/c no exception was thrown
                Assert.Fail();

            }
            catch (JsonPatchException) { }

        }


        [TestMethod]
        public void ReplaceInListInvalidPositionTooSmall()
        {

            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Replace<int>(o => o.IntegerList, 5, -1);

            try
            {
                patchDoc.ApplyTo(doc);

                // if we get here, we should fail b/c no exception was thrown
                Assert.Fail();

            }
            catch (JsonPatchException)
            {
            }

        }





        [TestMethod]
        public void Copy()
        {
            var doc = new SimpleDTO()
            {
                StringProperty = "A",
                AnotherStringProperty = "B"
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Copy<string>(o => o.StringProperty, o => o.AnotherStringProperty);

            patchDoc.ApplyTo(doc);

            Assert.AreEqual("A", doc.AnotherStringProperty);

        }



        [TestMethod]
        public void CopyInList()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Copy<int>(o => o.IntegerList, 0, o => o.IntegerList, 1);

            patchDoc.ApplyTo(doc);
            
            CollectionAssert.AreEquivalent(new List<int>() { 1, 1, 2, 3 }, doc.IntegerList);
        }


        [TestMethod]
        public void CopyFromListToEndOfList()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Copy<int>(o => o.IntegerList, 0, o => o.IntegerList);

            patchDoc.ApplyTo(doc);

            CollectionAssert.AreEquivalent(new List<int>() { 1, 2, 3, 1 }, doc.IntegerList);
        }




        [TestMethod]
        public void CopyFromListToNonList()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Copy<int>(o => o.IntegerList, 0, o => o.IntegerValue);

            patchDoc.ApplyTo(doc);

            Assert.AreEqual(1, doc.IntegerValue);
        }


        [TestMethod]
        public void CopyFromNonListToList()
        {
            var doc = new SimpleDTO()
            {
                IntegerValue = 5,
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Copy<int>(o => o.IntegerValue, o => o.IntegerList, 0);

            patchDoc.ApplyTo(doc);

            CollectionAssert.AreEqual(new List<int>() {5, 1, 2, 3} , doc.IntegerList);
        }

   


        [TestMethod]
        public void CopyToEndOfList()
        {
            var doc = new SimpleDTO()
            {
                IntegerValue = 5,
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Copy<int>(o => o.IntegerValue, o => o.IntegerList);

            patchDoc.ApplyTo(doc);


            CollectionAssert.AreEquivalent(new List<int>() { 1, 2, 3, 5 }, doc.IntegerList);

        }


        [TestMethod]
        public void Move()
        {
            var doc = new SimpleDTO()
            {
                StringProperty = "A",
                AnotherStringProperty = "B"
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Move<string>(o => o.StringProperty, o => o.AnotherStringProperty);

            patchDoc.ApplyTo(doc);

            Assert.AreEqual("A", doc.AnotherStringProperty);
            Assert.AreEqual(null, doc.StringProperty);
        }





        [TestMethod]
        public void MoveInList()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Move<int>(o => o.IntegerList, 0, o => o.IntegerList, 1);

            patchDoc.ApplyTo(doc);

            CollectionAssert.AreEquivalent(new List<int>() { 2, 1, 3 }, doc.IntegerList);
        }

        [TestMethod]
        public void MoveFromListToEndOfList()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Move<int>(o => o.IntegerList, 0, o => o.IntegerList);

            patchDoc.ApplyTo(doc);

            CollectionAssert.AreEquivalent(new List<int>() { 2, 3, 1 }, doc.IntegerList);
        }





        [TestMethod]
        public void MoveFomListToNonList()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Move<int>(o => o.IntegerList, 0, o => o.IntegerValue);

            patchDoc.ApplyTo(doc);

            CollectionAssert.AreEquivalent(new List<int>() { 2, 3 }, doc.IntegerList);
            Assert.AreEqual(1, doc.IntegerValue);
        }


        [TestMethod]
        public void MoveFromNonListToList()
        {
            var doc = new SimpleDTO()
            {
                IntegerValue = 5,
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Move<int>(o => o.IntegerValue, o => o.IntegerList, 0);

            patchDoc.ApplyTo(doc);

            Assert.AreEqual(0, doc.IntegerValue);
            CollectionAssert.AreEqual(new List<int>() { 5, 1, 2, 3 }, doc.IntegerList);
        }


 




        [TestMethod]
        public void MoveToEndOfList()
        {
            var doc = new SimpleDTO()
            {
                IntegerValue = 5,
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Move<int>(o => o.IntegerValue, o => o.IntegerList);

            patchDoc.ApplyTo(doc);

            Assert.AreEqual(0, doc.IntegerValue);
            CollectionAssert.AreEquivalent(new List<int>() { 1, 2, 3, 5 }, doc.IntegerList);

        } 

    }
}
