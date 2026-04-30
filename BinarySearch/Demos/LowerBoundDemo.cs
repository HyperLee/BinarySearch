using BinarySearch.Core.Boundary;

namespace BinarySearch.Demos;

/// <summary>
/// Lower Bound Demo：第一個 &gt;= target 的索引。
/// </summary>
public sealed class LowerBoundDemo : IDemo
{
    public string Title => "Lower Bound（第一個 >= target）";

    public void Run()
    {
        int[] nums = [1, 2, 2, 2, 3, 5, 8];
        int[] targets = [0, 2, 4, 9];

        Console.WriteLine($"輸入陣列：[{string.Join(", ", nums)}]");
        foreach (int t in targets)
        {
            int result = LowerBound.Find(nums, t);
            int brute = BruteLowerBound(nums, t);
            Console.WriteLine($"  target={t}：LowerBound = {result}，暴力解 = {brute}");
        }
    }

    private static int BruteLowerBound(int[] nums, int target)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] >= target)
            {
                return i;
            }
        }
        return nums.Length;
    }
}
