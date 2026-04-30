using BinarySearch.Core.Answer;

namespace BinarySearch.Tests.Answer;

public class KokoEatingBananasTests
{
    [Theory]
    [InlineData(new[] { 3, 6, 7, 11 }, 8, 4)]
    [InlineData(new[] { 30, 11, 23, 4, 20 }, 5, 30)]
    [InlineData(new[] { 30, 11, 23, 4, 20 }, 6, 23)]
    [InlineData(new[] { 1 }, 1, 1)]
    [InlineData(new[] { 1, 1, 1, 1 }, 4, 1)]
    [InlineData(new[] { 1000000000 }, 2, 500000000)]
    public void MinEatingSpeed_ReturnsMinimumViableSpeed(int[] piles, int h, int expected)
    {
        Assert.Equal(expected, KokoEatingBananas.MinEatingSpeed(piles, h));
    }

    [Fact]
    public void MinEatingSpeed_NullPiles_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => KokoEatingBananas.MinEatingSpeed(null!, 1));
    }

    [Fact]
    public void MinEatingSpeed_EmptyPiles_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => KokoEatingBananas.MinEatingSpeed([], 1));
    }

    [Fact]
    public void MinEatingSpeed_HoursLessThanPilesCount_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => KokoEatingBananas.MinEatingSpeed([1, 2, 3], 2));
    }
}
