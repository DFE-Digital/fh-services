using System.Text.Json;
using FamilyHubs.SharedKernel.Razor.AddAnother;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;

namespace FamilyHubs.SharedKernel.Razor.UnitTests.AddAnother;

public class AddAnotherAutocompleteErrorCheckerTests
{
    [Fact]
    public void ShouldBeRoundTripSerializableTest()
    {
        // Arrange
        var original = new AddAnotherAutocompleteErrorChecker(
            Enumerable.Range(1, 3),
            Enumerable.Range(10, 2),
            Enumerable.Repeat(Enumerable.Range(1, 3), 2));

        // Act
        string serialized = JsonSerializer.Serialize(original);
        var deserialized = JsonSerializer.Deserialize<AddAnotherAutocompleteErrorChecker>(serialized);

        // Assert
        Assert.NotNull(deserialized);
        Assert.Equal(original.EmptyIndexes.ToArray(), deserialized.EmptyIndexes.ToArray());
        Assert.Equal(original.EmptyIndexes.ToArray(), deserialized.EmptyIndexes.ToArray());
        AssertNestedCollectionsAreEqual(original.DuplicateIndexes, deserialized.DuplicateIndexes);
    }

    [Fact]
    public void JavascriptDisabled_NoLanguageSelected_ShouldFindFirstEmptyIndex()
    {
        // Arrange
        var form = new FormCollection(new Dictionary<string, StringValues>());

        // Act
        var result = AddAnotherAutocompleteErrorChecker.Create(form, "values", "texts",
            new List<SelectListItem> { new("a", "1"), new("c", "3") });

        // Assert
        Assert.Equal(new[] {0}, result.EmptyIndexes);
        Assert.Empty(result.InvalidIndexes);
        Assert.Empty(result.InvalidIndexes);
    }

    [Theory]
    [MemberData(nameof(JavascriptEnabled_ShouldReturnCorrectIndexes_TestData))]
    public void JavascriptEnabled_ShouldReturnCorrectIndexes(
        IEnumerable<int> expectedEmptyIndexes,
        IEnumerable<int> expectedInvalidIndexes,
        IEnumerable<IEnumerable<int>> expectedDuplicateIndexes,
        string[] texts)
    {
        // Arrange
        var form = new FormCollection(new Dictionary<string, StringValues>
        {
            // when javascript is enabled, you get the values, but they don't necessarily match the texts
            // when languages are pre-populated when editing, you get the original values, rather than those matching the texts
            { "values", new[] { "100", "101", "102" } },
            { "texts", texts }
        });

        // Act
        var result = AddAnotherAutocompleteErrorChecker.Create(form, "values", "texts",
            new List<SelectListItem>
            {
                new("a", "1"),
                new("b", "2"),
                new("c", "3")
            });

        // Assert
        Assert.Equal(expectedEmptyIndexes.ToArray(), result.EmptyIndexes.ToArray());
        Assert.Equal(expectedInvalidIndexes.ToArray(), result.InvalidIndexes.ToArray());
        AssertNestedCollectionsAreEqual(expectedDuplicateIndexes, result.DuplicateIndexes);
    }

    private static void AssertNestedCollectionsAreEqual(
        IEnumerable<IEnumerable<int>> expected,
        IEnumerable<IEnumerable<int>> actual)
    {
        var expectedList = expected.ToArray();
        var actualList = actual.ToArray();

        Assert.Equal(expectedList.Length, actualList.Length);

        for (int i = 0; i < expectedList.Length; ++i)
        {
            Assert.Equal(expectedList[i], actualList[i]);
        }
    }

    public static IEnumerable<object[]> JavascriptEnabled_ShouldReturnCorrectIndexes_TestData()
    {
        yield return new object[] { new[] { 0 }, Array.Empty<int>(), Array.Empty<int[]>(), new[] {""} };
        yield return new object[] { new[] { 0 }, Array.Empty<int>(), Array.Empty<int[]>(), new[] { "", "b", "c" } };
        yield return new object[] { new[] { 1 }, Array.Empty<int>(), Array.Empty<int[]>(), new[] { "a", "", "c" } };
        yield return new object[] { new[] { 1, 3 }, Array.Empty<int>(), Array.Empty<int[]>(), new[] { "a", "", "c", "" } };
        yield return new object[] { Array.Empty<int>(), new[] { 0 }, Array.Empty<int[]>(), new[] { "smurf" } };
        yield return new object[] { Array.Empty<int>(), new[] { 1 }, Array.Empty<int[]>(), new[] { "a", "smurf", "c" } };
        yield return new object[] { Array.Empty<int>(), Array.Empty<int>(), new[] { new[] { 0, 1 }}, new[] { "a", "a" } };
        yield return new object[] { Array.Empty<int>(), Array.Empty<int>(), new[] { new[] { 1, 2 }}, new[] { "b", "a", "a", "c" } };
        yield return new object[] { Array.Empty<int>(), Array.Empty<int>(), new[] { new[] { 0, 3 }}, new[] { "a", "b", "c", "a" } };
        yield return new object[] { new[] { 1 }, new[] { 3 }, Array.Empty<int[]>(), new[] { "b", "", "a", "smurf" } };
        yield return new object[] { new[] { 1, 4 }, new[] { 2, 5 }, Array.Empty<int[]>(), new[] { "b", "", "klingon", "a", "", "smurf" } };
        yield return new object[] { new[] { 3 }, Array.Empty<int>(), new[] { new[] { 1, 2 }}, new[] { "b", "a", "a", "" } };
        yield return new object[] { new[] { 1 }, Array.Empty<int>(), new[] { new[] { 0, 2 }}, new[] { "a", "", "a" } };
        yield return new object[] { Array.Empty<int>(), new[] { 2 }, new[] { new[] { 0, 3 }}, new[] { "a", "b", "womble", "a" } };
        yield return new object[] { Array.Empty<int>(), new[] { 2 }, new[] { new[] { 1, 3 }}, new[] { "a", "b", "womble", "b" } };
        yield return new object[] { new[] { 2 }, new[] { 3 }, new[] { new[] { 1, 4 }}, new[] { "a", "b", "", "womble", "b" } };
        yield return new object[] { new[] { 2 }, new[] { 3 }, new[] { new[] { 0, 5 }, new[] { 1, 4 }}, new[] { "a", "b", "", "womble", "b", "a" } };
        yield return new object[] { new[] { 0 }, new[] { 6 }, new[] { new[] { 1, 4, 8 }, new[] { 2, 7 }, new[] { 3, 5 } }, new[] { "", "a", "b", "c", "a", "c", "womble", "b", "a" } };
    }
}