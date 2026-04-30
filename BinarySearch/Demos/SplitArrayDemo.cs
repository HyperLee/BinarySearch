using BinarySearch.Core.Answer;

namespace BinarySearch.Demos;

/// <summary>
/// 答案二分 Demo：分割陣列最大和（LC 410）。
/// </summary>
public sealed class SplitArrayDemo : IDemo
{
    public string Title => "答案二分：分割陣列最大和";

    public void Run()
    {
        int[] nums = [7, 2, 5, 10, 8];
        int k = 2;
        int result = SplitArrayLargestSum.SplitArray(nums, k);
        Console.WriteLine($"nums = [{string.Join(", ", nums)}]，k = {k}");
        Console.WriteLine($"分割後各子陣列和的最大值之最小值 = {result}");
        Console.WriteLine("（理論最佳分割：[7,2,5] 與 [10,8] ⇒ max(14, 18) = 18）");
    }
}
