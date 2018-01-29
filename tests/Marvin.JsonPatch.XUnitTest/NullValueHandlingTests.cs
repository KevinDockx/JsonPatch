using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Marvin.JsonPatch.XUnitTest
{
    public class NullValueHandlingTests
    {
        public class Replace_ShouldHonour_NullValue_Ignore
        {
            [Fact]
            public void Test()
            {
                var horsePower = 333;
                var document = new JsonPatchDocument<Car>();
                document.Replace(x => x.Engine.HorsePower, horsePower);

                var car = new Car();
                car.Engine = new Engine();

                document.ApplyTo(car);
                Assert.Equal(car.Engine.HorsePower, horsePower);
            }
        }
    }
}
