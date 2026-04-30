using BinarySearch.Core.Boundary;

namespace BinarySearch.Tests.Boundary;

public class FirstLastOccurrenceTests
{
    [Theory]
    [InlineData(new[] { 5, 7, 7, 8, 8, 10 }, 8, 3)]
    [InlineData(new[] { 5, 7, 7, 8, 8, 10 }, 7, 1)]
    [InlineData(new[] { 5, 7, 7, 8, 8, 10 }, 6, -1)]
    [InlineData(new int[] { }, 0, -1)]
    [InlineData(new[] { 1 }, 1, 0)]
    [InlineData(new[] { 1 }, 2, -1)]
    [InlineData(new[] { 2, 2, 2 }, 2, 0)]
    public void FindFirst_ReturnsFirstIndex(int[] nums, int target, int expected)
    {
        Assert.Equal(expected, FirstLastOccurrence.FindFirst(nums, target));
    }

    [Theory]
    [InlineData(new[] { 5, 7, 7, 8, 8, 10 }, 8, 4)]
    [InlineData(new[] { 5, 7, 7, 8, 8, 10 }, 7, 2)]
    [InlineData(new[] { 5, 7, 7, 8, 8, 10 }, 6, -1)]
    [InlineData(new int[] { }, 0, -1)]
    [InlineData(new[] { 1 }, 1, 0)]
    [InlineData(new[] { 1 }, 2, -1)]
    [InlineData(new[] { 2, 2, 2 }, 2, 2)]
    [InlineData(new[] { 1, 2 }, 2, 1)]
    [InlineData(new[] { 1, 2 }, 1, 0)]
    public void FindLast_ReturnsLastIndex(int[] nums, int target, int expected)
    {
        Assert.Equal(expected, FirstLastOccurrence.FindLast(nums, target));
    }

    [Fact]
    public void FindFirst_NullArray_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => FirstLastOccurrence.FindFirst(null!, 0));
    }

    [Fact]
    public void FindLast_NullArray_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => FirstLastOccurrence.FindLast(null!, 0));
    }
}
