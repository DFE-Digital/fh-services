using System.ComponentModel;
using FamilyHubs.SharedKernel.Enumerable;

namespace FamilyHubs.SharedKernel.UnitTests.Enumerable
{
    public class EnumerableExtensionsTests
    {
        public enum TestEnum
        {
            [Description("Value One")]
            Value1,
            [Description("value two")]
            Value2,
            [Description("Value three")]
            Value3,
            [Description("Value four")]
            Value4
        }

        public static IEnumerable<object[]> TestCases()
        {
            yield return new object[] { new List<TestEnum> { }, "" };
            yield return new object[] { new List<TestEnum> { TestEnum.Value1 }, "Value one" };
            yield return new object[] { new List<TestEnum> { TestEnum.Value1, TestEnum.Value2 }, "Value one and value two" };
            yield return new object[] { new List<TestEnum> { TestEnum.Value1, TestEnum.Value2, TestEnum.Value3 }, "Value one, value three and value two" };
            yield return new object[] { new List<TestEnum> { TestEnum.Value3, TestEnum.Value1, TestEnum.Value2 }, "Value one, value three and value two" };
            yield return new object[] { new List<TestEnum> { TestEnum.Value4, TestEnum.Value3, TestEnum.Value1, TestEnum.Value2 }, "Value four, value one, value three and value two" };
        }

        [Theory]
        [MemberData(nameof(TestCases))]
        public void ToDisplay_ReturnsExpectedDisplay(IEnumerable<TestEnum> enumValues, string expectedDisplay)
        {
            // Act
            var display = enumValues.ToDisplay();

            // Assert
            Assert.Equal(expectedDisplay, display);
        }
    }
}
