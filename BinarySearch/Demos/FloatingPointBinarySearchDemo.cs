using BinarySearch.Core.FloatingPoint;

namespace BinarySearch.Demos;

/// <summary>
/// 浮點數二分 Demo：示範平方根與單調函數求根。
/// </summary>
public sealed class FloatingPointBinarySearchDemo : IDemo
{
    public string Title => "浮點數二分：求平方根";

    public void Run()
    {
        double[] xs = [2, 4, 10, 0.25, 1e6];
        Console.WriteLine("--- 平方根 ---");
        foreach (double x in xs)
        {
            double approx = FloatingPointBinarySearch.Sqrt(x);
            double native = Math.Sqrt(x);
            Console.WriteLine($"  Sqrt({x}) ≈ {approx:G15}，Math.Sqrt 對照 = {native:G15}，誤差 = {Math.Abs(approx - native):E2}");
        }

        Console.WriteLine();
        Console.WriteLine("--- FindRoot：求 x^3 - 8 = 0 ---");
        double root = FloatingPointBinarySearch.FindRoot(x => (x * x * x) - 8, 0, 10);
        Console.WriteLine($"  零點 ≈ {root:G15}（理論值 = 2）");
    }
}
