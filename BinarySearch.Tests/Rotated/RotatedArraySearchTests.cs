using BinarySearch.Core.Rotated;

namespace BinarySearch.Tests.Rotated;

public class RotatedArraySearchTests
{
    [Theory]
    [InlineData(new[] { 4, 5, 6, 7, 0, 1, 2 }, 0, 4)]
    [InlineData(new[] { 4, 5, 6, 7, 0, 1, 2 }, 3, -1)]
    [InlineData(new[] { 1 }, 0, -1)]
    [InlineData(new[] { 1 }, 1, 0)]
    [InlineData(new int[] { }, 5, -1)]
    [InlineData(new[] { 1, 3 }, 3, 1)]
    [InlineData(new[] { 3, 1 }, 1, 1)]
    [InlineData(new[] { 5, 1, 3 }, 5, 0)]
    [InlineData(new[] { 5, 1, 3 }, 3, 2)]
    [InlineData(new[] { 1, 2, 3, 4, 5 }, 4, 3)]
    public void Search_NoDuplicates_ReturnsExpected(int[] nums, int target, int expected)
    {
        Assert.Equal(expected, RotatedArraySearch.Search(nums, target));
    }

    [Theory]
    [InlineData(new[] { 2, 5, 6, 0, 0, 1, 2 }, 0, true)]
    [InlineData(new[] { 2, 5, 6, 0, 0, 1, 2 }, 3, false)]
    [InlineData(new[] { 1, 0, 1, 1, 1 }, 0, true)]
    [InlineData(new[] { 1, 1, 1, 1, 1 }, 2, false)]
    [InlineData(new[] { 1, 1, 1, 1, 1 }, 1, true)]
    [InlineData(new int[] { }, 1, false)]
    [InlineData(new[] { 1 }, 1, true)]
    public void SearchWithDuplicates_ReturnsExpected(int[] nums, int target, bool expected)
    {
        Assert.Equal(expected, RotatedArraySearch.SearchWithDuplicates(nums, target));
    }

    [Fact]
    public void Search_NullArray_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => RotatedArraySearch.Search(null!, 0));
    }

    [Fact]
    public void SearchWithDuplicates_NullArray_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => RotatedArraySearch.SearchWithDuplicates(null!, 0));
    }
}
