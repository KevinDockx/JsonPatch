using Marvin.JsonPatch.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit;

namespace Marvin.JsonPatch.XUnitTest
{

    public class ObjectAdapterTests
    {

        [Fact]
        public void AddResultsShouldReplace()
        {
            var doc = new SimpleDTO()
            {
                StringProperty = "A"
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Add<string>(o => o.StringProperty, "B");

            patchDoc.ApplyTo(doc);

            Assert.Equal("B", doc.StringProperty);

        }

        [Fact]
        public void AddResultsShouldReplaceWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                StringProperty = "A"
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Add<string>(o => o.StringProperty, "B");


            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            deserialized.ApplyTo(doc);

            Assert.Equal("B", doc.StringProperty);

        }


        [Fact]
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

            Assert.Equal(new List<int>() { 4, 1, 2, 3 }, doc.IntegerList);
        }

        [Fact]
        public void AddToGenericList()
        {
            var doc = new SimpleDTO
            {
                IntegerGenericList = new List<int> { 1, 2, 3 }
            };

            // create patch
            var patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Add(o => o.IntegerGenericList, 4, 0);

            patchDoc.ApplyTo(doc);

            Assert.Equal(new List<int> { 4, 1, 2, 3 }, doc.IntegerGenericList);
        }


        [Fact]
        public void AddToListWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Add<int>(o => o.IntegerList, 4, 0);


            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);


            deserialized.ApplyTo(doc);

            Assert.Equal(new List<int>() { 4, 1, 2, 3 }, doc.IntegerList);
        }

        [Fact]
        public void AddDecimal()
        {
            var doc = new SimpleDTO()
            {
                DecimalValue = 10
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Add(d => d.DecimalValue, 12);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            deserialized.ApplyTo(doc);

            Assert.Equal(12, doc.DecimalValue);
        }


        [Fact]
        public void AddToListInvalidPositionTooLarge()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Add<int>(o => o.IntegerList, 4, 4);

            Assert.Throws<JsonPatchException>(() => { patchDoc.ApplyTo(doc); });
        }

        [Fact]
        public void AddToListInvalidPositionTooLargeWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Add<int>(o => o.IntegerList, 4, 4);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            Assert.Throws<JsonPatchException>(() => { deserialized.ApplyTo(doc); });
        }

        [Fact]
        public void AddToListAtEnd()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Add<int>(o => o.IntegerList, 4, 3);

            patchDoc.ApplyTo(doc);

            Assert.Equal(new List<int>() { 1, 2, 3, 4 }, doc.IntegerList);
        }

        [Fact]
        public void AddToListAtEndWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Add<int>(o => o.IntegerList, 4, 3);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            deserialized.ApplyTo(doc);

            Assert.Equal(new List<int>() { 1, 2, 3, 4 }, doc.IntegerList);
        }

        [Fact]
        public void AddToListAtBeginning()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Add<int>(o => o.IntegerList, 4, 0);

            patchDoc.ApplyTo(doc);

            Assert.Equal(new List<int>() { 4, 1, 2, 3 }, doc.IntegerList);
        }

        [Fact]
        public void AddToListAtBeginningWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Add<int>(o => o.IntegerList, 4, 0);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            deserialized.ApplyTo(doc);

            Assert.Equal(new List<int>() { 4, 1, 2, 3 }, doc.IntegerList);
        }

        [Fact]
        public void AddToListInvalidPositionTooSmall()
        {

            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Add<int>(o => o.IntegerList, 4, -1);

            Assert.Throws<JsonPatchException>(() => { patchDoc.ApplyTo(doc); });

        }

        [Fact]
        public void AddToListInvalidPositionTooSmallWithSerialization()
        {

            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Add<int>(o => o.IntegerList, 4, -1);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            Assert.Throws<JsonPatchException>(() => { deserialized.ApplyTo(doc); });

        }


        [Fact]
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

            Assert.Equal(new List<int>() { 1, 2, 3, 4 }, doc.IntegerList);

        }




        [Fact]
        public void AddToListAppendWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Add<int>(o => o.IntegerList, 4);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            deserialized.ApplyTo(doc);

            Assert.Equal(new List<int>() { 1, 2, 3, 4 }, doc.IntegerList);

        }


        [Fact]
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

            Assert.Equal(null, doc.StringProperty);
        }

        [Fact]
        public void RemoveDecimal()
        {
            var doc = new SimpleDTO()
            {
                DecimalValue = 10
            };

            // create patch
            var patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Remove(o => o.DecimalValue);

            patchDoc.ApplyTo(doc);

            Assert.Equal(0, doc.DecimalValue);
        }

        [Fact]
        public void RemoveInteger()
        {
            var doc = new SimpleDTO()
            {
                IntegerValue = 10
            };

            // create patch
            var patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Remove(o => o.IntegerValue);

            patchDoc.ApplyTo(doc);

            Assert.Equal(0, doc.IntegerValue);
        }



        [Fact]
        public void RemoveWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                StringProperty = "A"
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Remove<string>(o => o.StringProperty);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            deserialized.ApplyTo(doc);

            Assert.Equal(null, doc.StringProperty);

        }



        [Fact]
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

            Assert.Equal(new List<int>() { 1, 2 }, doc.IntegerList);
        }

        [Fact]
        public void RemoveFromGenericList()
        {
            var doc = new SimpleDTO
            {
                IntegerGenericList = new List<int> { 1, 2, 3 }
            };

            // create patch
            var patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Remove(o => o.IntegerGenericList, 2);

            patchDoc.ApplyTo(doc);

            Assert.Equal(new List<int> { 1, 2 }, doc.IntegerGenericList);
        }


        [Fact]
        public void RemoveFromListWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Remove<int>(o => o.IntegerList, 2);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            deserialized.ApplyTo(doc);

            Assert.Equal(new List<int>() { 1, 2 }, doc.IntegerList);
        }


        [Fact]
        public void RemoveFromListInvalidPositionTooLarge()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Remove<int>(o => o.IntegerList, 3);


            Assert.Throws<JsonPatchException>(() => { patchDoc.ApplyTo(doc); });

        }


        [Fact]
        public void RemoveFromListInvalidPositionTooLargeWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Remove<int>(o => o.IntegerList, 3);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            Assert.Throws<JsonPatchException>(() => { deserialized.ApplyTo(doc); });

        }


        [Fact]
        public void RemoveFromListInvalidPositionTooSmall()
        {

            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Remove<int>(o => o.IntegerList, -1);

            Assert.Throws<JsonPatchException>(() => { patchDoc.ApplyTo(doc); });

        }

        [Fact]
        public void RemoveFromListInvalidPositionTooSmallWithSerialization()
        {

            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Remove<int>(o => o.IntegerList, -1);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            Assert.Throws<JsonPatchException>(() => { deserialized.ApplyTo(doc); });

        }


        [Fact]
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

            Assert.Equal(new List<int>() { 1, 2 }, doc.IntegerList);

        }

        [Fact]
        public void RemoveFromEndOfListWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Remove<int>(o => o.IntegerList);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            deserialized.ApplyTo(doc);

            Assert.Equal(new List<int>() { 1, 2 }, doc.IntegerList);

        }


        [Fact]
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

            patchDoc.Replace(o => o.DecimalValue, 12);

            patchDoc.ApplyTo(doc);

            Assert.Equal("B", doc.StringProperty);
            Assert.Equal(12, doc.DecimalValue);

        }


        [Fact]
        public void ReplaceWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                StringProperty = "A",
                DecimalValue = 10
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Replace<string>(o => o.StringProperty, "B");

            patchDoc.Replace(o => o.DecimalValue, 12);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            deserialized.ApplyTo(doc);

            Assert.Equal("B", doc.StringProperty);
            Assert.Equal(12, doc.DecimalValue);

        }


        [Fact]
        public void SerializationNoEnvelope()
        {
            var doc = new SimpleDTO()
            {
                StringProperty = "A"
            };

            // create patch
            var patch = "[{ \"op\": \"replace\", \"path\": \"/stringproperty\", \"value\": \"B\" }]";

            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(patch);
            deserialized.ApplyTo(doc);

            Assert.Equal("B", doc.StringProperty);
        }



        [Fact]
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

            Assert.Equal(false, serialized.Contains("operations"));
            Assert.Equal(false, serialized.Contains("Operations"));


        }



        [Fact]
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

            Assert.IsType<JsonPatchDocument<SimpleDTO>>(deserialized);


        }


        [Fact]
        public void DeserializationMustFailWithEnvelope()
        {
            string serialized = "{\"Operations\": [{ \"op\": \"replace\", \"path\": \"/title\", \"value\": \"New Title\"}]}";

            Assert.Throws<JsonPatchException>(() =>
            {
                var deserialized
                    = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);
            });

        }



        [Fact]
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

            Assert.Equal("B", doc.StringProperty);
            Assert.Equal(12, doc.DecimalValue);
            Assert.Equal(12, doc.DoubleValue);
            Assert.Equal(12, doc.FloatValue);
            Assert.Equal(12, doc.IntegerValue);

        }



        [Fact]
        public void SerializeAndReplaceGuidTest()
        {
            var doc = new SimpleDTO()
            {
                GuidValue = Guid.NewGuid()
            };

            var newGuid = Guid.NewGuid();
            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Replace(o => o.GuidValue, newGuid);


            // serialize & deserialize 
            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserizalized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);


            deserizalized.ApplyTo(doc);

            Assert.Equal(newGuid, doc.GuidValue);


        }




        [Fact]
        public void SerializeAndReplaceNestedObjectTest()
        {
            var doc = new SimpleDTOWithNestedDTO()
            {
                SimpleDTO = new SimpleDTO()
                {
                    IntegerValue = 5,
                    IntegerList = new List<int>() { 1, 2, 3 }
                }
            };


            var newDTO = new SimpleDTO()
            {
                DoubleValue = 1
            };

            // create patch
            JsonPatchDocument<SimpleDTOWithNestedDTO> patchDoc = new JsonPatchDocument<SimpleDTOWithNestedDTO>();
            patchDoc.Replace(o => o.SimpleDTO, newDTO);


            // serialize & deserialize 
            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTOWithNestedDTO>>(serialized);

            deserialized.ApplyTo(doc);

            Assert.Equal(1, doc.SimpleDTO.DoubleValue);
            Assert.Equal(0, doc.SimpleDTO.IntegerValue);
            Assert.Equal(null, doc.SimpleDTO.IntegerList);


        }




        [Fact]
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

            Assert.Equal(new List<int>() { 5, 2, 3 }, doc.IntegerList);
        }

        [Fact]
        public void ReplaceInGenericList()
        {
            var doc = new SimpleDTO
            {
                IntegerGenericList = new List<int> { 1, 2, 3 }
            };

            // create patch
            var patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Replace(o => o.IntegerGenericList, 5, 0);

            patchDoc.ApplyTo(doc);

            Assert.Equal(new List<int> { 5, 2, 3 }, doc.IntegerGenericList);

        }


        [Fact]
        public void ReplaceInListWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Replace<int>(o => o.IntegerList, 5, 0);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            deserialized.ApplyTo(doc);

            Assert.Equal(new List<int>() { 5, 2, 3 }, doc.IntegerList);

        }



        [Fact]
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


            Assert.Equal(new List<int>() { 4, 5, 6 }, doc.IntegerList);

        }

        [Fact]
        public void ReplaceFullListWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Replace<List<int>>(o => o.IntegerList, new List<int>() { 4, 5, 6 });

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            deserialized.ApplyTo(doc);


            Assert.Equal(new List<int>() { 4, 5, 6 }, doc.IntegerList);

        }



        [Fact]
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


            Assert.Equal(new List<int>() { 4, 5, 6 }, doc.IntegerList);

        }

        [Fact]
        public void ReplaceFullListFromEnumerableWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Replace<IEnumerable<int>>(o => o.IntegerList, new List<int>() { 4, 5, 6 });

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            deserialized.ApplyTo(doc);


            Assert.Equal(new List<int>() { 4, 5, 6 }, doc.IntegerList);

        }



        [Fact]
        public void ReplaceFullListWithCollection()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Replace<IEnumerable<int>>(o => o.IntegerList, new Collection<int>() { 4, 5, 6 });

            patchDoc.ApplyTo(doc);

            Assert.Equal(new List<int>() { 4, 5, 6 }, doc.IntegerList);



        }

        [Fact]
        public void ReplaceFullListWithCollectionWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Replace<IEnumerable<int>>(o => o.IntegerList, new Collection<int>() { 4, 5, 6 });

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            deserialized.ApplyTo(doc);

            Assert.Equal(new List<int>() { 4, 5, 6 }, doc.IntegerList);


        }



        [Fact]
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


            Assert.Equal(new List<int>() { 1, 2, 5 }, doc.IntegerList);

        }

        [Fact]
        public void ReplaceAtEndOfListWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Replace<int>(o => o.IntegerList, 5);
            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);
            deserialized.ApplyTo(doc);


            Assert.Equal(new List<int>() { 1, 2, 5 }, doc.IntegerList);

        }

        [Fact]
        public void ReplaceInListInvalidInvalidPositionTooLarge()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Replace<int>(o => o.IntegerList, 5, 3);

            Assert.Throws<JsonPatchException>(() =>
            {
                patchDoc.ApplyTo(doc);
            });


        }

        [Fact]
        public void ReplaceInListInvalidInvalidPositionTooLargeWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Replace<int>(o => o.IntegerList, 5, 3);
            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            Assert.Throws<JsonPatchException>(() =>
            {
                deserialized.ApplyTo(doc);
            });


        }


        [Fact]
        public void ReplaceInListInvalidPositionTooSmall()
        {

            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Replace<int>(o => o.IntegerList, 5, -1);

            Assert.Throws<JsonPatchException>(() =>
            {
                patchDoc.ApplyTo(doc);
            });


        }


        [Fact]
        public void ReplaceInListInvalidPositionTooSmallWithSerialization()
        {

            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Replace<int>(o => o.IntegerList, 5, -1);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            Assert.Throws<JsonPatchException>(() =>
            {
                deserialized.ApplyTo(doc);
            });


        }





        [Fact]
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

            Assert.Equal("A", doc.AnotherStringProperty);

        }


        [Fact]
        public void CopyWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                StringProperty = "A",
                AnotherStringProperty = "B"
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Copy<string>(o => o.StringProperty, o => o.AnotherStringProperty);
            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);
            deserialized.ApplyTo(doc);

            Assert.Equal("A", doc.AnotherStringProperty);

        }



        [Fact]
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

            Assert.Equal(new List<int>() { 1, 1, 2, 3 }, doc.IntegerList);
        }

        [Fact]
        public void CopyInGenericList()
        {
            var doc = new SimpleDTO
            {
                IntegerGenericList = new List<int> { 1, 2, 3 }
            };

            // create patch
            var patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Copy(o => o.IntegerGenericList, 0, o => o.IntegerGenericList, 1);

            patchDoc.ApplyTo(doc);

            Assert.Equal(new List<int> { 1, 1, 2, 3 }, doc.IntegerGenericList);
        }

        [Fact]
        public void CopyInListWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Copy<int>(o => o.IntegerList, 0, o => o.IntegerList, 1);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            deserialized.ApplyTo(doc);

            Assert.Equal(new List<int>() { 1, 1, 2, 3 }, doc.IntegerList);
        }


        [Fact]
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

            Assert.Equal(new List<int>() { 1, 2, 3, 1 }, doc.IntegerList);
        }


        [Fact]
        public void CopyFromListToEndOfListWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Copy<int>(o => o.IntegerList, 0, o => o.IntegerList);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);
            deserialized.ApplyTo(doc);

            Assert.Equal(new List<int>() { 1, 2, 3, 1 }, doc.IntegerList);
        }




        [Fact]
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

            Assert.Equal(1, doc.IntegerValue);
        }


        [Fact]
        public void CopyFromListToNonListWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Copy<int>(o => o.IntegerList, 0, o => o.IntegerValue);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            deserialized.ApplyTo(doc);

            Assert.Equal(1, doc.IntegerValue);
        }


        [Fact]
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

            Assert.Equal(new List<int>() { 5, 1, 2, 3 }, doc.IntegerList);
        }


        [Fact]
        public void CopyFromNonListToListWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                IntegerValue = 5,
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Copy<int>(o => o.IntegerValue, o => o.IntegerList, 0);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);
            deserialized.ApplyTo(doc);

            Assert.Equal(new List<int>() { 5, 1, 2, 3 }, doc.IntegerList);
        }




        [Fact]
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


            Assert.Equal(new List<int>() { 1, 2, 3, 5 }, doc.IntegerList);

        }


        [Fact]
        public void CopyToEndOfListWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                IntegerValue = 5,
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Copy<int>(o => o.IntegerValue, o => o.IntegerList);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            deserialized.ApplyTo(doc);


            Assert.Equal(new List<int>() { 1, 2, 3, 5 }, doc.IntegerList);

        }


        [Fact]
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

            Assert.Equal("A", doc.AnotherStringProperty);
            Assert.Equal(null, doc.StringProperty);
        }

        [Fact]
        public void MoveWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                StringProperty = "A",
                AnotherStringProperty = "B"
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Move<string>(o => o.StringProperty, o => o.AnotherStringProperty);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            deserialized.ApplyTo(doc);

            Assert.Equal("A", doc.AnotherStringProperty);
            Assert.Equal(null, doc.StringProperty);
        }





        [Fact]
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

            Assert.Equal(new List<int>() { 2, 1, 3 }, doc.IntegerList);
        }

        [Fact]
        public void MoveInGenericList()
        {
            var doc = new SimpleDTO
            {
                IntegerGenericList = new List<int> { 1, 2, 3 }
            };

            // create patch
            var patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Move(o => o.IntegerGenericList, 0, o => o.IntegerGenericList, 1);

            patchDoc.ApplyTo(doc);

            Assert.Equal(new List<int> { 2, 1, 3 }, doc.IntegerGenericList);
        }


        [Fact]
        public void MoveInListWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Move<int>(o => o.IntegerList, 0, o => o.IntegerList, 1);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            deserialized.ApplyTo(doc);

            Assert.Equal(new List<int>() { 2, 1, 3 }, doc.IntegerList);
        }


        [Fact]
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

            Assert.Equal(new List<int>() { 2, 3, 1 }, doc.IntegerList);
        }



        [Fact]
        public void MoveFromListToEndOfListWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Move<int>(o => o.IntegerList, 0, o => o.IntegerList);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);
            deserialized.ApplyTo(doc);

            Assert.Equal(new List<int>() { 2, 3, 1 }, doc.IntegerList);
        }


        [Fact]
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

            Assert.Equal(new List<int>() { 2, 3 }, doc.IntegerList);
            Assert.Equal(1, doc.IntegerValue);
        }


        [Fact]
        public void MoveFomListToNonListWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Move<int>(o => o.IntegerList, 0, o => o.IntegerValue);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            deserialized.ApplyTo(doc);

            Assert.Equal(new List<int>() { 2, 3 }, doc.IntegerList);
            Assert.Equal(1, doc.IntegerValue);
        }


        [Fact]
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

            Assert.Equal(0, doc.IntegerValue);
            Assert.Equal(new List<int>() { 5, 1, 2, 3 }, doc.IntegerList);
        }

        [Fact]
        public void MoveFromNonListToListWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                IntegerValue = 5,
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Move<int>(o => o.IntegerValue, o => o.IntegerList, 0);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            deserialized.ApplyTo(doc);

            Assert.Equal(0, doc.IntegerValue);
            Assert.Equal(new List<int>() { 5, 1, 2, 3 }, doc.IntegerList);
        }

        [Fact]
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

            Assert.Equal(0, doc.IntegerValue);
            Assert.Equal(new List<int>() { 1, 2, 3, 5 }, doc.IntegerList);
        }

        [Fact]
        public void MoveToEndOfListWithSerialization()
        {
            var doc = new SimpleDTO()
            {
                IntegerValue = 5,
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Move<int>(o => o.IntegerValue, o => o.IntegerList);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleDTO>>(serialized);

            deserialized.ApplyTo(doc);

            Assert.Equal(0, doc.IntegerValue);
            Assert.Equal(new List<int>() { 1, 2, 3, 5 }, doc.IntegerList);
        }

        [Fact]
        public void AddOperation_AddsItem_WhenAddingToCustomList()
        {
            var doc = new DtoWithDerivedListProperty()
            {
                DerivedList = new DerivedList() { "a" },
            };

            // create patch
            var patchDoc = new JsonPatchDocument<DtoWithDerivedListProperty>();
            patchDoc.Add<string>(o => o.DerivedList, "b");

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert
                .DeserializeObject<JsonPatchDocument<DtoWithDerivedListProperty>>(serialized);

            deserialized.ApplyTo(doc);

            Assert.Equal(new DerivedList() { "a", "b" }, doc.DerivedList);
        }

        [Fact]
        public void AddOperation_AddsItem_WhenAddingToCustomListOfT()
        {
            var doc = new DtoWithDerivedListProperty()
            {
                DerivedListOfT = new DerivedListOfT<int>() { 1 }
            };

            // create patch
            var patchDoc = new JsonPatchDocument<DtoWithDerivedListProperty>();
            patchDoc.Add<int>(o => o.DerivedListOfT, 2);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert
                .DeserializeObject<JsonPatchDocument<DtoWithDerivedListProperty>>(serialized);

            deserialized.ApplyTo(doc);

            Assert.Equal(new DerivedListOfT<int>() { 1, 2 }, doc.DerivedListOfT);
        }

        private class Class6
        {
            public IDictionary<string, int> DictionaryOfStringToInteger { get; } = new Dictionary<string, int>();
        }

        [Fact]
        public void Add_WhenDictionary_ValueIsNonObject_Succeeds()
        {
            // Arrange
            var model = new Class6();
            model.DictionaryOfStringToInteger["one"] = 1;
            model.DictionaryOfStringToInteger["two"] = 2;
            var patchDocument = new JsonPatchDocument();
            patchDocument.Add("/DictionaryOfStringToInteger/three", 3);

            // Act
            patchDocument.ApplyTo(model);

            // Assert
            Assert.Equal(3, model.DictionaryOfStringToInteger.Count);
            Assert.Equal(1, model.DictionaryOfStringToInteger["one"]);
            Assert.Equal(2, model.DictionaryOfStringToInteger["two"]);
            Assert.Equal(3, model.DictionaryOfStringToInteger["three"]);
        }

        [Fact]
        public void Remove_WhenDictionary_ValueIsNonObject_Succeeds()
        {
            // Arrange
            var model = new Class6();
            model.DictionaryOfStringToInteger["one"] = 1;
            model.DictionaryOfStringToInteger["two"] = 2;
            var patchDocument = new JsonPatchDocument();
            patchDocument.Remove("/DictionaryOfStringToInteger/two");

            // Act
            patchDocument.ApplyTo(model);

            // Assert
            Assert.Equal(1, model.DictionaryOfStringToInteger.Count);
            Assert.Equal(1, model.DictionaryOfStringToInteger["one"]);
        }

        [Fact]
        public void Replace_WhenDictionary_ValueIsNonObject_Succeeds()
        {
            // Arrange
            var model = new Class6();
            model.DictionaryOfStringToInteger["one"] = 1;
            model.DictionaryOfStringToInteger["two"] = 2;
            var patchDocument = new JsonPatchDocument();
            patchDocument.Replace("/DictionaryOfStringToInteger/two", 20);

            // Act
            patchDocument.ApplyTo(model);

            // Assert
            Assert.Equal(2, model.DictionaryOfStringToInteger.Count);
            Assert.Equal(1, model.DictionaryOfStringToInteger["one"]);
            Assert.Equal(20, model.DictionaryOfStringToInteger["two"]);
        }

        private class Customer
        {
            public string Name { get; set; }
            public Address Address { get; set; }
        }

        private class Address
        {
            public string City { get; set; }
        }

        private class Class8
        {
            public IDictionary<string, Customer> DictionaryOfStringToCustomer { get; } = new Dictionary<string, Customer>();
        }

        [Fact]
        public void Replace_WhenDictionary_ValueAPocoType_Succeeds()
        {
            // Arrange
            var key1 = "key1";
            var value1 = new Customer() { Name = "Jamesss" };
            var key2 = "key2";
            var value2 = new Customer() { Name = "Mike" };
            var model = new Class8();
            model.DictionaryOfStringToCustomer[key1] = value1;
            model.DictionaryOfStringToCustomer[key2] = value2;
            var patchDocument = new JsonPatchDocument();
            patchDocument.Replace($"/DictionaryOfStringToCustomer/{key1}/Name", "James");

            // Act
            patchDocument.ApplyTo(model);

            // Assert
            Assert.Equal(2, model.DictionaryOfStringToCustomer.Count);
            var actualValue1 = model.DictionaryOfStringToCustomer[key1];
            Assert.NotNull(actualValue1);
            Assert.Equal("James", actualValue1.Name);
        }

        [Fact]
        public void Replace_WhenDictionary_ValueAPocoType_Succeeds_WithSerialization()
        {
            // Arrange
            var key1 = "key1";
            var value1 = new Customer() { Name = "Jamesss" };
            var key2 = "key2";
            var value2 = new Customer() { Name = "Mike" };
            var model = new Class8();
            model.DictionaryOfStringToCustomer[key1] = value1;
            model.DictionaryOfStringToCustomer[key2] = value2;
            var patchDocument = new JsonPatchDocument();
            patchDocument.Replace($"/DictionaryOfStringToCustomer/{key1}/Name", "James");
            var serialized = JsonConvert.SerializeObject(patchDocument);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<Class8>>(serialized);

            // Act
            patchDocument.ApplyTo(model);

            // Assert
            Assert.Equal(2, model.DictionaryOfStringToCustomer.Count);
            var actualValue1 = model.DictionaryOfStringToCustomer[key1];
            Assert.NotNull(actualValue1);
            Assert.Equal("James", actualValue1.Name);
        }

        [Fact]
        public void Replace_WhenDictionary_ValueAPocoType_WithEscaping_Succeeds()
        {
            // Arrange
            var key1 = "Foo/Name";
            var value1 = new Customer() { Name = "Jamesss" };
            var key2 = "Foo";
            var value2 = new Customer() { Name = "Mike" };
            var model = new Class8();
            model.DictionaryOfStringToCustomer[key1] = value1;
            model.DictionaryOfStringToCustomer[key2] = value2;
            var patchDocument = new JsonPatchDocument();
            patchDocument.Replace($"/DictionaryOfStringToCustomer/Foo~1Name/Name", "James");

            // Act
            patchDocument.ApplyTo(model);

            // Assert
            Assert.Equal(2, model.DictionaryOfStringToCustomer.Count);
            var actualValue1 = model.DictionaryOfStringToCustomer[key1];
            var actualValue2 = model.DictionaryOfStringToCustomer[key2];
            Assert.NotNull(actualValue1);
            Assert.Equal("James", actualValue1.Name);
            Assert.Equal("Mike", actualValue2.Name);

        }

        [Fact]
        public void Replace_DeepNested_DictionaryValue_Succeeds()
        {
            // Arrange
            var key1 = "key1";
            var value1 = new Customer() { Name = "Jamesss" };
            var key2 = "key2";
            var value2 = new Customer() { Name = "Mike" };
            var model = new Class8();
            model.DictionaryOfStringToCustomer[key1] = value1;
            model.DictionaryOfStringToCustomer[key2] = value2;
            var patchDocument = new JsonPatchDocument();
            patchDocument.Replace($"/DictionaryOfStringToCustomer/{key1}/Name", "James");

            // Act
            patchDocument.ApplyTo(model);

            // Assert
            Assert.Equal(2, model.DictionaryOfStringToCustomer.Count);
            var actualValue1 = model.DictionaryOfStringToCustomer[key1];
            Assert.NotNull(actualValue1);
            Assert.Equal("James", actualValue1.Name);
        }

        [Fact]
        public void Replace_DeepNested_DictionaryValue_Succeeds_WithSerialization()
        {
            // Arrange
            var key1 = "key1";
            var value1 = new Customer() { Name = "James", Address = new Address { City = "Redmond" } };
            var key2 = "key2";
            var value2 = new Customer() { Name = "Mike", Address = new Address { City = "Seattle" } };
            var model = new Class8();
            model.DictionaryOfStringToCustomer[key1] = value1;
            model.DictionaryOfStringToCustomer[key2] = value2;
            var patchDocument = new JsonPatchDocument();
            patchDocument.Replace($"/DictionaryOfStringToCustomer/{key1}/Address/City", "Bellevue");
            var serialized = JsonConvert.SerializeObject(patchDocument);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<Class8>>(serialized);

            // Act
            patchDocument.ApplyTo(model);

            // Assert
            Assert.Equal(2, model.DictionaryOfStringToCustomer.Count);
            var actualValue1 = model.DictionaryOfStringToCustomer[key1];
            Assert.NotNull(actualValue1);
            Assert.Equal("James", actualValue1.Name);
            var address = actualValue1.Address;
            Assert.NotNull(address);
            Assert.Equal("Bellevue", address.City);
        }

        class Class9
        {
            public List<string> StringList { get; set; } = new List<string>();
        }

        [Fact]
        public void AddToNonIntegerListAtEnd()
        {
            // Arrange
            var model = new Class9()
            {
                StringList = new List<string>()
            };
            model.StringList.Add("string1");
            model.StringList.Add("string2");
            var patchDoc = new JsonPatchDocument();
            patchDoc.Add("/StringList/0", "string3");

            // Act
            patchDoc.ApplyTo(model);

            // Assert
            Assert.Equal(new List<string>() { "string3", "string1", "string2" }, model.StringList);
        }

        [Fact]
        public void AddMember_OnPOCO_WithNullPropertyValue_ShouldAddPropertyValue()
        {
            // Arrange
            var doc = new SimpleObject()
            {
                StringProperty = null
            };

            // create patch
            var patchDoc = new JsonPatchDocument<SimpleObject>();
            patchDoc.Add<string>(o => o.StringProperty, "B");

            // Act
            patchDoc.ApplyTo(doc);

            // Assert
            Assert.Equal("B", doc.StringProperty);
        }

        private class Class1
        {
            public IDictionary<string, string> USStates { get; set; } = new Dictionary<string, string>();
        }

        [Fact]
        public void AddMember_OnDictionaryProperty_ShouldAddKeyValueMember()
        {
            // Arrange
            var expected = "Washington";
            var model = new Class1();
            var patchDoc = new JsonPatchDocument();
            patchDoc.Add("/USStates/WA", expected);

            // Act
            patchDoc.ApplyTo(model);

            // Assert
            Assert.Equal(1, model.USStates.Count);
            Assert.Equal(expected, model.USStates["WA"]);
        }

        [Fact]
        public void AddMember_OnDictionaryProperty_ShouldAddKeyValueMember_WithSerialization()
        {
            // Arrange
            var expected = "Washington";
            var model = new Class1();
            var patchDoc = new JsonPatchDocument();
            patchDoc.Add("/USStates/WA", expected);
            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<Class1>>(serialized);

            // Act
            deserialized.ApplyTo(model);

            // Assert
            Assert.Equal(1, model.USStates.Count);
            Assert.Equal(expected, model.USStates["WA"]);
        }

        private class Class2
        {
            public Class1 Class1Property { get; set; } = new Class1();
        }

        [Fact]
        public void AddMember_OnDictionaryPropertyDeeplyNested_ShouldAddKeyValueMember()
        {
            // Arrange
            var expected = "Washington";
            var model = new Class2();
            var patchDoc = new JsonPatchDocument();
            patchDoc.Add("/Class1Property/USStates/WA", expected);

            // Act
            patchDoc.ApplyTo(model);

            // Assert
            Assert.Equal(1, model.Class1Property.USStates.Count);
            Assert.Equal(expected, model.Class1Property.USStates["WA"]);
        }

        [Fact]
        public void AddMember_OnDictionaryPropertyDeeplyNested_ShouldAddKeyValueMember_WithSerialization()
        {
            // Arrange
            var expected = "Washington";
            var model = new Class2();
            var patchDoc = new JsonPatchDocument();
            patchDoc.Add("/Class1Property/USStates/WA", expected);
            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<Class2>>(serialized);

            // Act
            deserialized.ApplyTo(model);

            // Assert
            Assert.Equal(1, model.Class1Property.USStates.Count);
            Assert.Equal(expected, model.Class1Property.USStates["WA"]);
        }

        [Fact]
        public void AddMember_OnDictionaryObjectDirectly_ShouldAddKeyValueMember()
        {
            // Arrange
            var expected = "Washington";
            var model = new Dictionary<string, string>();
            var patchDoc = new JsonPatchDocument();
            patchDoc.Add("/WA", expected);

            // Act
            patchDoc.ApplyTo(model);

            // Assert
            Assert.Single(model);
            Assert.Equal(expected, model["WA"]);
        }

        [Fact]
        public void AddMember_OnDictionaryObjectDirectly_ShouldAddKeyValueMember_WithSerialization()
        {
            // Arrange
            var expected = "Washington";
            var model = new Dictionary<string, string>();
            var patchDoc = new JsonPatchDocument();
            patchDoc.Add("/WA", expected);
            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<Dictionary<string, string>>>(serialized);

            // Act
            deserialized.ApplyTo(model);

            // Assert
            Assert.Single(model);
            Assert.Equal(expected, model["WA"]);
        }

        [Fact]
        public void AddElement_ToListDirectly_ShouldAppendValue()
        {
            // Arrange
            var model = new List<int>() { 1, 2, 3 };
            var patchDoc = new JsonPatchDocument();
            patchDoc.Add("/-", value: 4);
            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<List<int>>>(serialized);

            // Act
            deserialized.ApplyTo(model);

            // Assert
            Assert.Equal(new List<int>() { 1, 2, 3, 4 }, model);
        }

        [Fact]
        public void AddElement_ToListDirectly_ShouldAppendValue_WithSerialization()
        {
            // Arrange
            var model = new List<int>() { 1, 2, 3 };
            var patchDoc = new JsonPatchDocument();
            patchDoc.Add("/-", value: 4);

            // Act
            patchDoc.ApplyTo(model);

            // Assert
            Assert.Equal(new List<int>() { 1, 2, 3, 4 }, model);
        }

        [Fact]
        public void AddElement_ToListDirectly_ShouldAddValue_AtSuppliedPosition()
        {
            // Arrange
            var model = new List<int>() { 1, 2, 3 };
            var patchDoc = new JsonPatchDocument();
            patchDoc.Add("/0", value: 4);

            // Act
            patchDoc.ApplyTo(model);

            // Assert
            Assert.Equal(new List<int>() { 4, 1, 2, 3 }, model);
        }

        [Fact]
        public void AddElement_ToListDirectly_ShouldAddValue_AtSuppliedPosition_WithSerialization()
        {
            // Arrange
            var model = new List<int>() { 1, 2, 3 };
            var patchDoc = new JsonPatchDocument();
            patchDoc.Add("/0", value: 4);
            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<List<int>>>(serialized);

            // Act
            deserialized.ApplyTo(model);

            // Assert
            Assert.Equal(new List<int>() { 4, 1, 2, 3 }, model);
        }

        class ListOnDictionary
        {
            public IDictionary<string, List<int>> NamesAndBadgeIds { get; set; } = new Dictionary<string, List<int>>();
        }

        [Fact]
        public void AddElement_ToList_OnDictionary_ShouldAddValue_AtSuppliedPosition()
        {
            // Arrange
            var model = new ListOnDictionary();
            model.NamesAndBadgeIds["James"] = new List<int>();
            var patchDoc = new JsonPatchDocument();
            patchDoc.Add("/NamesAndBadgeIds/James/-", 200);

            // Act
            patchDoc.ApplyTo(model);

            // Assert
            var list = model.NamesAndBadgeIds["James"];
            Assert.NotNull(list);
            Assert.Equal(new List<int>() { 200 }, list);
        }

        [Fact]
        public void AddElement_ToList_OnPOCO_ShouldAddValue_AtSuppliedPosition()
        {
            // Arrange
            var doc = new SimpleObject()
            {
                IntegerIList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            var patchDoc = new JsonPatchDocument<SimpleObject>();
            patchDoc.Add<int>(o => o.IntegerIList, 4, 0);

            // Act
            patchDoc.ApplyTo(doc);

            // Assert
            Assert.Equal(new List<int>() { 4, 1, 2, 3 }, doc.IntegerIList);
        }

        class Class3
        {
            public SimpleObject SimpleObjectProperty { get; set; } = new SimpleObject();
        }

        [Fact]
        public void AddElement_ToDeeplyNestedListProperty_OnPOCO_ShouldAddValue_AtSuppliedPosition()
        {
            // Arrange
            var model = new Class3()
            {
                SimpleObjectProperty = new SimpleObject()
                {
                    IntegerIList = new List<int>() { 1, 2, 3 }
                }
            };
            var patchDoc = new JsonPatchDocument<Class3>();
            patchDoc.Add<int>(o => o.SimpleObjectProperty.IntegerIList, value: 4, position: 0);

            // Act
            patchDoc.ApplyTo(model);

            // Assert
            Assert.Equal(new List<int>() { 4, 1, 2, 3 }, model.SimpleObjectProperty.IntegerIList);
        }

        [Fact]
        public void AddElement_ToDeeplyNestedListProperty_OnPOCO_ShouldAddValue_AtSuppliedPosition_WithSerialization()
        {
            // Arrange
            var model = new Class3()
            {
                SimpleObjectProperty = new SimpleObject()
                {
                    IntegerIList = new List<int>() { 1, 2, 3 }
                }
            };
            var patchDoc = new JsonPatchDocument<Class3>();
            patchDoc.Add<int>(o => o.SimpleObjectProperty.IntegerIList, value: 4, position: 0);
            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<Class3>>(serialized);

            // Act
            deserialized.ApplyTo(model);

            // Assert
            Assert.Equal(new List<int>() { 4, 1, 2, 3 }, model.SimpleObjectProperty.IntegerIList);
        }

        class Class4
        {
            public int IntegerProperty { get; set; }
        }

        [Fact]
        public void Remove_OnNonReferenceType_POCOProperty_ShouldSetDefaultValue()
        {
            // Arrange
            var model = new Class4()
            {
                IntegerProperty = 10
            };
            var patchDoc = new JsonPatchDocument<Class4>();
            patchDoc.Remove<int>(o => o.IntegerProperty);

            // Act
            patchDoc.ApplyTo(model);

            // Assert
            Assert.Equal(0, model.IntegerProperty);
        }

        [Fact]
        public void Remove_OnNonReferenceType_POCOProperty_ShouldSetDefaultValue_WithSerialization()
        {
            // Arrange
            var doc = new SimpleObject()
            {
                StringProperty = "A"
            };

            // create patch
            var patchDoc = new JsonPatchDocument<SimpleObject>();
            patchDoc.Remove<string>(o => o.StringProperty);

            var serialized = JsonConvert.SerializeObject(patchDoc);
            var deserialized = JsonConvert.DeserializeObject<JsonPatchDocument<SimpleObject>>(serialized);

            // Act
            deserialized.ApplyTo(doc);

            // Assert
            Assert.Null(doc.StringProperty);
        }

        class ClassWithPrivateProperties
        {
            public string Name { get; set; }
            private int Age { get; set; } = 45;
        }

        [Fact]
        public void Add_OnPrivateProperties_FailesWithException()
        {
            // Arrange
            var doc = new ClassWithPrivateProperties()
            {
                Name = "James"
            };

            // create patch
            var patchDoc = new JsonPatchDocument();
            patchDoc.Add("/Age", 30);

            // Act & Assert
            var exception = Assert.Throws<JsonPatchException>(() => patchDoc.ApplyTo(doc));
            Assert.Equal(
                string.Format("The target location specified by path segment '{0}' was not found.", "Age"),
                exception.Message);
        }
    }
}
