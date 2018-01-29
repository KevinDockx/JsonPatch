using Marvin.JsonPatch.XUnitTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Marvin.JsonPatch.XUnitTest.Tests
{
    public class JsonPropertyAttributeTests
    {
        [Fact]
        public void Replace_ShouldHonour_NullValue_Ignore()
        {
            var horsePower = 333;
            var document = new JsonPatchDocument<Car>();
            document.Replace(x => x.Engine.HorsePower, horsePower);

            var car = new Car();
            car.Engine = new Engine();

            document.ApplyTo(car);
            Assert.Equal(car.Engine.HorsePower, horsePower);
        }

        [Fact]
        public void Replace_ShouldFindPath_ForJsonPropertyWithNoName()
        {
            var horsePower = 333;
            var document = new JsonPatchDocument<Car>();
            document.Replace(x => x.Engine.HorsePowerAlwaysRequired, horsePower);

            var car = new Car();
            car.Engine = new Engine();

            document.ApplyTo(car);
            Assert.Equal(car.Engine.HorsePowerAlwaysRequired, horsePower);
        }

    }
}
