using BinarySearch.Core.Peak;

namespace BinarySearch.Demos;

/// <summary>
/// 峰值元素 Demo（LC 162）。
/// </summary>
public sealed class PeakElementDemo : IDemo
{
    public string Title => "峰值元素（FindPeakElement）";

    public void Run()
    {
        int[][] cases =
        [
            [1, 2, 3, 1],
            [1, 2, 1, 3, 5, 6, 4],
            [1],
            [5, 4, 3, 2, 1],
            [1, 2, 3, 4, 5],
        ];

        foreach (int[] nums in cases)
        {
            int idx = FindPeakElement.FindPeak(nums);
            bool valid = IsPeak(nums, idx);
            Console.WriteLine($"陣列：[{string.Join(", ", nums)}] → 峰值索引 = {idx}，nums[idx] = {nums[idx]}，是否為合法峰值 = {valid}");
        }
    }

    private static bool IsPeak(int[] nums, int i)
    {
        bool leftOk = i == 0 || nums[i] > nums[i - 1];
        bool rightOk = i == nums.Length - 1 || nums[i] > nums[i + 1];
        return leftOk && rightOk;
    }
}
