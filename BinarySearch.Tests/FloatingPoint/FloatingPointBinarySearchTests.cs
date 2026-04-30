using BinarySearch.Core.FloatingPoint;

namespace BinarySearch.Tests.FloatingPoint;

public class FloatingPointBinarySearchTests
{
    private const double Tolerance = 1e-6;

    [Theory]
    [InlineData(0d)]
    [InlineData(1d)]
    [InlineData(2d)]
    [InlineData(4d)]
    [InlineData(0.25d)]
    [InlineData(1e6)]
    [InlineData(1e-6)]
    public void Sqrt_ReturnsApproximateSquareRoot(double x)
    {
        double result = FloatingPointBinarySearch.Sqrt(x);
        Assert.InRange(result * result, x - Tolerance, x + Tolerance);
    }

    [Fact]
    public void Sqrt_NegativeInput_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => FloatingPointBinarySearch.Sqrt(-1));
    }

    [Fact]
    public void Sqrt_NonPositiveEpsilon_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => FloatingPointBinarySearch.Sqrt(4, 0));
        Assert.Throws<ArgumentException>(() => FloatingPointBinarySearch.Sqrt(4, -1));
    }

    [Fact]
    public void FindRoot_LinearIncreasing_FindsZero()
    {
        // f(x) = x - 3，零點 = 3
        double root = FloatingPointBinarySearch.FindRoot(x => x - 3, 0, 10);
        Assert.InRange(root, 3 - Tolerance, 3 + Tolerance);
    }

    [Fact]
    public void FindRoot_LinearDecreasing_FindsZero()
    {
        // f(x) = 5 - x，零點 = 5（遞減函數）
        double root = FloatingPointBinarySearch.FindRoot(x => 5 - x, 0, 10);
        Assert.InRange(root, 5 - Tolerance, 5 + Tolerance);
    }

    [Fact]
    public void FindRoot_CubicMonotonic_FindsZero()
    {
        // f(x) = x^3 - 8，零點 = 2
        double root = FloatingPointBinarySearch.FindRoot(x => (x * x * x) - 8, 0, 10);
        Assert.InRange(root, 2 - Tolerance, 2 + Tolerance);
    }

    [Fact]
    public void FindRoot_NullFunc_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => FloatingPointBinarySearch.FindRoot(null!, 0, 1));
    }

    [Fact]
    public void FindRoot_HiNotGreaterThanLo_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => FloatingPointBinarySearch.FindRoot(x => x, 1, 1));
        Assert.Throws<ArgumentException>(() => FloatingPointBinarySearch.FindRoot(x => x, 2, 1));
    }

    [Fact]
    public void FindRoot_EndpointsSameSign_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => FloatingPointBinarySearch.FindRoot(x => x + 10, 0, 5));
    }

    [Fact]
    public void FindRoot_EndpointIsZero_ReturnsEndpoint()
    {
        Assert.Equal(0, FloatingPointBinarySearch.FindRoot(x => x, 0, 5));
        Assert.Equal(5, FloatingPointBinarySearch.FindRoot(x => x - 5, 0, 5));
    }
}
