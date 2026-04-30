using BinarySearch.Core.Peak;

namespace BinarySearch.Tests.Peak;

public class FindPeakElementTests
{
    [Theory]
    [InlineData(new[] { 1, 2, 3, 1 }, new[] { 2 })]
    [InlineData(new[] { 1, 2, 1, 3, 5, 6, 4 }, new[] { 1, 5 })]
    [InlineData(new[] { 1 }, new[] { 0 })]
    [InlineData(new[] { 1, 2 }, new[] { 1 })]
    [InlineData(new[] { 2, 1 }, new[] { 0 })]
    [InlineData(new[] { 5, 4, 3, 2, 1 }, new[] { 0 })]
    [InlineData(new[] { 1, 2, 3, 4, 5 }, new[] { 4 })]
    public void FindPeak_ReturnsAnyValidPeakIndex(int[] nums, int[] validPeaks)
    {
        int idx = FindPeakElement.FindPeak(nums);
        Assert.Contains(idx, validPeaks);
    }

    [Fact]
    public void FindPeak_NullArray_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => FindPeakElement.FindPeak(null!));
    }

    [Fact]
    public void FindPeak_EmptyArray_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => FindPeakElement.FindPeak([]));
    }
}
