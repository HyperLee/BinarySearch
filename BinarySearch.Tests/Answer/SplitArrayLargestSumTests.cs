using BinarySearch.Core.Answer;

namespace BinarySearch.Tests.Answer;

public class SplitArrayLargestSumTests
{
    [Theory]
    [InlineData(new[] { 7, 2, 5, 10, 8 }, 2, 18)]
    [InlineData(new[] { 1, 2, 3, 4, 5 }, 2, 9)]
    [InlineData(new[] { 1, 4, 4 }, 3, 4)]
    [InlineData(new[] { 1, 2, 3, 4, 5 }, 1, 15)]
    [InlineData(new[] { 1, 2, 3, 4, 5 }, 5, 5)]
    [InlineData(new[] { 10 }, 1, 10)]
    public void SplitArray_ReturnsMinimisedLargestSum(int[] nums, int k, int expected)
    {
        Assert.Equal(expected, SplitArrayLargestSum.SplitArray(nums, k));
    }

    [Fact]
    public void SplitArray_NullNums_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => SplitArrayLargestSum.SplitArray(null!, 1));
    }

    [Fact]
    public void SplitArray_EmptyNums_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => SplitArrayLargestSum.SplitArray([], 1));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(10)]
    public void SplitArray_KOutOfRange_ThrowsArgumentException(int k)
    {
        Assert.Throws<ArgumentException>(() => SplitArrayLargestSum.SplitArray([1, 2, 3], k));
    }

    [Fact]
    public void SplitArray_NegativeNumber_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => SplitArrayLargestSum.SplitArray([1, -2, 3], 2));
    }
}
