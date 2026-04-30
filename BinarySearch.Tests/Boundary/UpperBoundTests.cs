using BinarySearch.Core.Boundary;

namespace BinarySearch.Tests.Boundary;

public class UpperBoundTests
{
    [Theory]
    [InlineData(new[] { 1, 2, 2, 3 }, 2, 3)]
    [InlineData(new[] { 1, 2, 2, 3 }, 3, 4)]
    [InlineData(new[] { 1, 2, 2, 3 }, 0, 0)]
    [InlineData(new int[] { }, 1, 0)]
    [InlineData(new[] { 5 }, 5, 1)]
    [InlineData(new[] { 5 }, 4, 0)]
    [InlineData(new[] { 1, 1, 1, 1 }, 1, 4)]
    [InlineData(new[] { 1, 1, 1, 1 }, 0, 0)]
    public void Find_ReturnsFirstIndexStrictlyGreaterThanTarget(int[] nums, int target, int expected)
    {
        Assert.Equal(expected, UpperBound.Find(nums, target));
    }

    [Theory]
    [InlineData(new[] { 1, 2, 2, 2, 3 }, 2, 3)]
    [InlineData(new[] { 1, 2, 2, 2, 3 }, 3, 1)]
    [InlineData(new[] { 1, 2, 2, 2, 3 }, 4, 0)]
    [InlineData(new int[] { }, 1, 0)]
    [InlineData(new[] { 5, 5, 5 }, 5, 3)]
    public void CountEqual_ReturnsExpectedCount(int[] nums, int target, int expected)
    {
        Assert.Equal(expected, UpperBound.CountEqual(nums, target));
    }

    [Fact]
    public void Find_NullArray_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => UpperBound.Find(null!, 0));
    }

    [Fact]
    public void CountEqual_NullArray_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => UpperBound.CountEqual(null!, 0));
    }
}
