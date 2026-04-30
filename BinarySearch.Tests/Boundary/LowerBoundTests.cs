using BinarySearch.Core.Boundary;

namespace BinarySearch.Tests.Boundary;

public class LowerBoundTests
{
    [Theory]
    [InlineData(new[] { 1, 2, 2, 3 }, 2, 1)]
    [InlineData(new[] { 1, 2, 2, 3 }, 3, 3)]
    [InlineData(new[] { 1, 2, 2, 3 }, 0, 0)]
    [InlineData(new[] { 1, 2, 2, 3 }, 4, 4)]
    [InlineData(new int[] { }, 1, 0)]
    [InlineData(new[] { 5 }, 5, 0)]
    [InlineData(new[] { 5 }, 6, 1)]
    [InlineData(new[] { 5 }, 4, 0)]
    [InlineData(new[] { 1, 1, 1, 1 }, 1, 0)]
    [InlineData(new[] { 1, 1, 1, 1 }, 2, 4)]
    public void Find_ReturnsFirstIndexGreaterOrEqualTarget(int[] nums, int target, int expected)
    {
        Assert.Equal(expected, LowerBound.Find(nums, target));
    }

    [Fact]
    public void Find_NullArray_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => LowerBound.Find(null!, 0));
    }

    [Fact]
    public void Find_AllElementsLessThanTarget_ReturnsLength()
    {
        int[] nums = [1, 2, 3, 4, 5];
        Assert.Equal(nums.Length, LowerBound.Find(nums, 100));
    }
}
