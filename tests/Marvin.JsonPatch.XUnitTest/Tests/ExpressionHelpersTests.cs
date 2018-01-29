using System;
using System.Linq;
using Marvin.JsonPatch.Helpers;
using Xunit;

namespace Marvin.JsonPatch.XUnitTest.Tests
{
    public class ExpressionHelpersTests
    {
        [Fact]
        public void CaseTransform_AllCaseTransformTypesAreImplemented()
        {
            var propertyName = "MyPropertyName";
            foreach (var value in Enum.GetValues(typeof(CaseTransformType)).Cast<CaseTransformType>())
            {
                ExpressionHelpers.CaseTransform(propertyName, value);
            }

            //assert no exception is thrown
        }

        [Fact]
        public void CaseTransform_CamelCaseReturnsCorrectly()
        {
            var propertyName = "MyPropertyName";
            var result = ExpressionHelpers.CaseTransform(propertyName, CaseTransformType.CamelCase);

            Assert.Equal("myPropertyName", result);
        }

        [Fact]
        public void CaseTransform_SingleCharacterCamelCaseReturnsCorrectly()
        {
            var propertyName = "M";
            var result = ExpressionHelpers.CaseTransform(propertyName, CaseTransformType.CamelCase);

            Assert.Equal("m", result);
        }

        [Fact]
        public void CaseTransform_LowerCaseReturnsCorrectly()
        {
            var propertyName = "MyPropertyName";
            var result = ExpressionHelpers.CaseTransform(propertyName, CaseTransformType.LowerCase);

            Assert.Equal("mypropertyname", result);
        }

        [Fact]
        public void CaseTransform_UpperCaseReturnsCorrectly()
        {
            var propertyName = "MyPropertyName";
            var result = ExpressionHelpers.CaseTransform(propertyName, CaseTransformType.UpperCase);

            Assert.Equal("MYPROPERTYNAME", result);
        }

        [Fact]
        public void CaseTransform_OriginalCaseReturnsCorrectly()
        {
            var propertyName = "MyPropertyName";
            var result = ExpressionHelpers.CaseTransform(propertyName, CaseTransformType.OriginalCase);

            Assert.Equal("MyPropertyName", result);
        }
    }
}