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
    public class NestedObjectTests
    {

        [TestMethod]
        public void ReplacePropertyInNestedObject()
        {
            var doc = new SimpleDTOWithNestedDTO()
            {
                 IntegerValue = 1
              
            };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Replace<string>(o => o.NestedDTO.StringProperty, "B");

            patchDoc.ApplyTo(doc);

            Assert.AreEqual("B", doc.NestedDTO.StringProperty);

        }


        [TestMethod]
        public void ReplaceNestedObject()
        {
            var doc = new SimpleDTOWithNestedDTO()
            {
                IntegerValue = 1

            };

            var newNested = new NestedDTO() { StringProperty = "B" };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Replace<NestedDTO>(o => o.NestedDTO, newNested);

            patchDoc.ApplyTo(doc);


            Assert.AreEqual(newNested, doc.NestedDTO);
            Assert.AreEqual("B", doc.NestedDTO.StringProperty);

        }


        [TestMethod]
        public void AddResultsInReplace()
        {
            var doc = new SimpleDTOWithNestedDTO()
            {
                SimpleDTO = new SimpleDTO()
                    {
                        StringProperty = "A"
                    }
            };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Add<string>(o => o.SimpleDTO.StringProperty, "B");

            patchDoc.ApplyTo(doc);

            Assert.AreEqual("B", doc.SimpleDTO.StringProperty);

        }


        [TestMethod]
        public void AddToList()
        {
            var doc = new SimpleDTOWithNestedDTO()
            {
                SimpleDTO = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            }
            };
             

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Add<int>(o => o.SimpleDTO.IntegerList, 4, 0);

            patchDoc.ApplyTo(doc);

            CollectionAssert.AreEquivalent(new List<int>() { 4, 1, 2, 3 }, doc.SimpleDTO.IntegerList);
        }


        [TestMethod]
        public void AddToListInvalidPositionTooLarge()
        {
            var doc = new SimpleDTOWithNestedDTO()
            {
                SimpleDTO = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            }
            }
            ;

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Add<int>(o => o.SimpleDTO.IntegerList, 4, 3);


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

            var doc = new SimpleDTOWithNestedDTO()
          {
              SimpleDTO = new SimpleDTO()
              {
                  IntegerList = new List<int>() { 1, 2, 3 }
              }
          };



            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Add<int>(o => o.SimpleDTO.IntegerList, 4, -1);

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
            var doc = new SimpleDTOWithNestedDTO()
                {
                    SimpleDTO = new SimpleDTO()
                  {
                      IntegerList = new List<int>() { 1, 2, 3 }
                  }
                };


            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Add<int>(o => o.SimpleDTO.IntegerList, 4);

            patchDoc.ApplyTo(doc);

            CollectionAssert.AreEquivalent(new List<int>() { 1, 2, 3, 4 }, doc.SimpleDTO.IntegerList);

        }


        [TestMethod]
        public void Remove()
        {
            var doc = new SimpleDTOWithNestedDTO()
                  {
                      SimpleDTO = new SimpleDTO()
              {
                  StringProperty = "A"
              }
                  };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Remove<string>(o => o.SimpleDTO.StringProperty);

            patchDoc.ApplyTo(doc);

            Assert.AreEqual(null, doc.SimpleDTO.StringProperty);

        }



        [TestMethod]
        public void RemoveFromList()
        {
            var doc = new SimpleDTOWithNestedDTO()
                  {
                      SimpleDTO = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            }
                  };


            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Remove<int>(o => o.SimpleDTO.IntegerList, 2);

            patchDoc.ApplyTo(doc);

            CollectionAssert.AreEquivalent(new List<int>() { 1, 2 }, doc.SimpleDTO.IntegerList);
        }


        [TestMethod]
        public void RemoveFromListInvalidPositionTooLarge()
        {
            var doc = new SimpleDTOWithNestedDTO()
                 {
                     SimpleDTO = new SimpleDTO()
           {
               IntegerList = new List<int>() { 1, 2, 3 }
           }
                 };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Remove<int>(o => o.SimpleDTO.IntegerList, 3);


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

            var doc = new SimpleDTOWithNestedDTO()
              {
                  SimpleDTO = new SimpleDTO()
         {
             IntegerList = new List<int>() { 1, 2, 3 }
         }
              }
              ;

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Remove<int>(o => o.SimpleDTO.IntegerList, -1);

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
            var doc = new SimpleDTOWithNestedDTO()
              {
                  SimpleDTO = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            }
              };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Remove<int>(o => o.SimpleDTO.IntegerList);

            patchDoc.ApplyTo(doc);

            CollectionAssert.AreEquivalent(new List<int>() { 1, 2 }, doc.SimpleDTO.IntegerList);

        }


        [TestMethod]
        public void Replace()
        {
            var doc = new SimpleDTOWithNestedDTO()
             {
                 SimpleDTO = new SimpleDTO()
           {
               StringProperty = "A",
               DecimalValue = 10
           }
             };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Replace<string>(o => o.SimpleDTO.StringProperty, "B");
            //  patchDoc.Replace<decimal>(o => o.DecimalValue, 12);
            patchDoc.Replace(o => o.SimpleDTO.DecimalValue, 12);

            patchDoc.ApplyTo(doc);

            Assert.AreEqual("B", doc.SimpleDTO.StringProperty);
            Assert.AreEqual(12, doc.SimpleDTO.DecimalValue);



        }




        [TestMethod]
        public void SerializationTests()
        {
            var doc = new SimpleDTOWithNestedDTO()
             {
                 SimpleDTO = new SimpleDTO()
            {
                StringProperty = "A",
                DecimalValue = 10,
                DoubleValue = 10,
                FloatValue = 10,
                IntegerValue = 10
            }
             };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Replace(o => o.SimpleDTO.StringProperty, "B");
            patchDoc.Replace(o => o.SimpleDTO.DecimalValue, 12);
            patchDoc.Replace(o => o.SimpleDTO.DoubleValue, 12);
            patchDoc.Replace(o => o.SimpleDTO.FloatValue, 12);
            patchDoc.Replace(o => o.SimpleDTO.IntegerValue, 12);



            // serialize & deserialize 
            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserizalized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTOWithNestedDTO>>(serialized);


            deserizalized.ApplyTo(doc);

            Assert.AreEqual("B", doc.SimpleDTO.StringProperty);
            Assert.AreEqual(12, doc.SimpleDTO.DecimalValue);
            Assert.AreEqual(12, doc.SimpleDTO.DoubleValue);
            Assert.AreEqual(12, doc.SimpleDTO.FloatValue);
            Assert.AreEqual(12, doc.SimpleDTO.IntegerValue);

        }

        [TestMethod]
        public void ReplaceInList()
        {
            var doc = new SimpleDTOWithNestedDTO()
             {
                 SimpleDTO = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            }
             };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Replace<int>(o => o.SimpleDTO.IntegerList, 5, 0);

            patchDoc.ApplyTo(doc);


            CollectionAssert.AreEquivalent(new List<int>() { 5, 2, 3 }, doc.SimpleDTO.IntegerList);

        }



        [TestMethod]
        public void ReplaceFullList()
        {
            var doc = new SimpleDTOWithNestedDTO()
            {
                SimpleDTO = new SimpleDTO()
           {
               IntegerList = new List<int>() { 1, 2, 3 }
           }
            };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Replace<List<int>>(o => o.SimpleDTO.IntegerList, new List<int>() { 4, 5, 6 });

            patchDoc.ApplyTo(doc);


            CollectionAssert.AreEquivalent(new List<int>() { 4, 5, 6 }, doc.SimpleDTO.IntegerList);

        }



        [TestMethod]
        public void ReplaceFullListFromEnumerable()
        {
            var doc = new SimpleDTOWithNestedDTO()
            {
                SimpleDTO = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            }
            };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Replace<IEnumerable<int>>(o => o.SimpleDTO.IntegerList, new List<int>() { 4, 5, 6 });

            patchDoc.ApplyTo(doc);


            CollectionAssert.AreEquivalent(new List<int>() { 4, 5, 6 }, doc.SimpleDTO.IntegerList);

        }



        [TestMethod]
        public void ReplaceFullListWithCollection()
        {
            var doc = new SimpleDTOWithNestedDTO()
            {
                SimpleDTO = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            }
            };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Replace<IEnumerable<int>>(o => o.SimpleDTO.IntegerList, new Collection<int>() { 4, 5, 6 });


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
            var doc = new SimpleDTOWithNestedDTO()
           {
               SimpleDTO = new SimpleDTO()
           {
               IntegerList = new List<int>() { 1, 2, 3 }
           }
           };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Replace<int>(o => o.SimpleDTO.IntegerList, 5);

            patchDoc.ApplyTo(doc);


            CollectionAssert.AreEquivalent(new List<int>() { 1, 2, 5 }, doc.SimpleDTO.IntegerList);

        }

        [TestMethod]
        public void ReplaceInListInvalidInvalidPositionTooLarge()
        {
            var doc = new SimpleDTOWithNestedDTO()
              {
                  SimpleDTO = new SimpleDTO()
               {
                   IntegerList = new List<int>() { 1, 2, 3 }
               }
              };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Replace<int>(o => o.SimpleDTO.IntegerList, 5, 3);


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


            var doc = new SimpleDTOWithNestedDTO()
              {
                  SimpleDTO = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            }
              };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Replace<int>(o => o.SimpleDTO.IntegerList, 5, -1);

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
            var doc = new SimpleDTOWithNestedDTO()
               {
                   SimpleDTO = new SimpleDTO()
             {
                 StringProperty = "A",
                 AnotherStringProperty = "B"
             }
               };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Copy<string>(o => o.SimpleDTO.StringProperty, o => o.SimpleDTO.AnotherStringProperty);

            patchDoc.ApplyTo(doc);

            Assert.AreEqual("A", doc.SimpleDTO.AnotherStringProperty);

        }



        [TestMethod]
        public void CopyInList()
        {
            var doc = new SimpleDTOWithNestedDTO()
              {
                  SimpleDTO = new SimpleDTO()
           {
               IntegerList = new List<int>() { 1, 2, 3 }
           }
              };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Copy<int>(o => o.SimpleDTO.IntegerList, 0, o => o.SimpleDTO.IntegerList, 1);

            patchDoc.ApplyTo(doc);

            CollectionAssert.AreEquivalent(new List<int>() { 1, 1, 2, 3 }, doc.SimpleDTO.IntegerList);
        }


        [TestMethod]
        public void CopyFromListToEndOfList()
        {
            var doc = new SimpleDTOWithNestedDTO()
             {
                 SimpleDTO = new SimpleDTO()
           {
               IntegerList = new List<int>() { 1, 2, 3 }
           }
             };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Copy<int>(o => o.SimpleDTO.IntegerList, 0, o => o.SimpleDTO.IntegerList);

            patchDoc.ApplyTo(doc);

            CollectionAssert.AreEquivalent(new List<int>() { 1, 2, 3, 1 }, doc.SimpleDTO.IntegerList);
        }




        [TestMethod]
        public void CopyFromListToNonList()
        {
            var doc = new SimpleDTOWithNestedDTO()
             {
                 SimpleDTO = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            }
             };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Copy<int>(o => o.SimpleDTO.IntegerList, 0, o => o.SimpleDTO.IntegerValue);

            patchDoc.ApplyTo(doc);

            Assert.AreEqual(1, doc.SimpleDTO.IntegerValue);
        }


        [TestMethod]
        public void CopyFromNonListToList()
        {
            var doc = new SimpleDTOWithNestedDTO()
             {
                 SimpleDTO = new SimpleDTO()
            {
                IntegerValue = 5,
                IntegerList = new List<int>() { 1, 2, 3 }
            }
             };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Copy<int>(o => o.SimpleDTO.IntegerValue, o => o.SimpleDTO.IntegerList, 0);

            patchDoc.ApplyTo(doc);

            CollectionAssert.AreEqual(new List<int>() { 5, 1, 2, 3 }, doc.SimpleDTO.IntegerList);
        }




        [TestMethod]
        public void CopyToEndOfList()
        {
            var doc = new SimpleDTOWithNestedDTO()
            {
                SimpleDTO = new SimpleDTO()
           {
               IntegerValue = 5,
               IntegerList = new List<int>() { 1, 2, 3 }
           }
            };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Copy<int>(o => o.SimpleDTO.IntegerValue, o => o.SimpleDTO.IntegerList);

            patchDoc.ApplyTo(doc);


            CollectionAssert.AreEquivalent(new List<int>() { 1, 2, 3, 5 }, doc.SimpleDTO.IntegerList);

        }


        [TestMethod]
        public void Move()
        {
            var doc = new SimpleDTOWithNestedDTO()
           {
               SimpleDTO = new SimpleDTO()
           {
               StringProperty = "A",
               AnotherStringProperty = "B"
           }
           };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Move<string>(o => o.SimpleDTO.StringProperty, o => o.SimpleDTO.AnotherStringProperty);

            patchDoc.ApplyTo(doc);

            Assert.AreEqual("A", doc.SimpleDTO.AnotherStringProperty);
            Assert.AreEqual(null, doc.SimpleDTO.StringProperty);
        }





        [TestMethod]
        public void MoveInList()
        {
            var doc = new SimpleDTOWithNestedDTO()
         {
             SimpleDTO = new SimpleDTO()
          {
              IntegerList = new List<int>() { 1, 2, 3 }
          }
         };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Move<int>(o => o.SimpleDTO.IntegerList, 0, o => o.SimpleDTO.IntegerList, 1);

            patchDoc.ApplyTo(doc);

            CollectionAssert.AreEquivalent(new List<int>() { 2, 1, 3 }, doc.SimpleDTO.IntegerList);
        }

        [TestMethod]
        public void MoveFromListToEndOfList()
        {
            var doc = new SimpleDTOWithNestedDTO()
        {
            SimpleDTO = new SimpleDTO()
           {
               IntegerList = new List<int>() { 1, 2, 3 }
           }
        };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Move<int>(o => o.SimpleDTO.IntegerList, 0, o => o.SimpleDTO.IntegerList);

            patchDoc.ApplyTo(doc);

            CollectionAssert.AreEquivalent(new List<int>() { 2, 3, 1 }, doc.SimpleDTO.IntegerList);
        }





        [TestMethod]
        public void MoveFomListToNonList()
        {
            var doc = new SimpleDTOWithNestedDTO()
           {
               SimpleDTO = new SimpleDTO()
               {
                   IntegerList = new List<int>() { 1, 2, 3 }
               }
           };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Move<int>(o => o.SimpleDTO.IntegerList, 0, o => o.SimpleDTO.IntegerValue);

            patchDoc.ApplyTo(doc);

            CollectionAssert.AreEquivalent(new List<int>() { 2, 3 }, doc.SimpleDTO.IntegerList);
            Assert.AreEqual(1, doc.SimpleDTO.IntegerValue);
        }

         [TestMethod]
        public void MoveFomListToNonListBetweenHierarchy()
        {
            var doc = new SimpleDTOWithNestedDTO()
            {
                SimpleDTO = new SimpleDTO()
                {
                    IntegerList = new List<int>() { 1, 2, 3 }
                }
            };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Move<int>(o => o.SimpleDTO.IntegerList, 0, o => o.IntegerValue);

            patchDoc.ApplyTo(doc);

            CollectionAssert.AreEquivalent(new List<int>() { 2, 3 }, doc.SimpleDTO.IntegerList);
            Assert.AreEqual(1, doc.IntegerValue);
        }


        [TestMethod]
        public void MoveFromNonListToList()
        {
            var doc = new SimpleDTOWithNestedDTO()
      {
          SimpleDTO = new SimpleDTO()
           {
               IntegerValue = 5,
               IntegerList = new List<int>() { 1, 2, 3 }
           }
      };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Move<int>(o => o.SimpleDTO.IntegerValue, o => o.SimpleDTO.IntegerList, 0);

            patchDoc.ApplyTo(doc);

            Assert.AreEqual(0, doc.IntegerValue);
            CollectionAssert.AreEqual(new List<int>() { 5, 1, 2, 3 }, doc.SimpleDTO.IntegerList);
        }







        [TestMethod]
        public void MoveToEndOfList()
        {
            var doc = new SimpleDTOWithNestedDTO()
     {
         SimpleDTO = new SimpleDTO()
           {
               IntegerValue = 5,
               IntegerList = new List<int>() { 1, 2, 3 }
           }
     };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Move<int>(o => o.SimpleDTO.IntegerValue, o => o.SimpleDTO.IntegerList);

            patchDoc.ApplyTo(doc);

            Assert.AreEqual(0, doc.IntegerValue);
            CollectionAssert.AreEquivalent(new List<int>() { 1, 2, 3, 5 }, doc.SimpleDTO.IntegerList);

        } 







    }
}
