using BinarySearch.Core.Basic;

namespace BinarySearch.Demos;

/// <summary>
/// 經典二分搜尋 Demo：閉區間 vs 半開區間並列展示，並印出每一步 (left, right, mid)。
/// </summary>
public sealed class ClassicSearchDemo : IDemo
{
    public string Title => "經典二分搜尋（閉區間 vs 半開區間對照）";

    public void Run()
    {
        int[] nums = [1, 3, 5, 7, 9, 11, 13, 15, 17, 19];
        int target = 13;

        Console.WriteLine($"輸入陣列：[{string.Join(", ", nums)}]");
        Console.WriteLine($"目標值：{target}");
        Console.WriteLine();

        Console.WriteLine("--- 閉區間 [left, right] 過程 ---");
        TraceClosed(nums, target);
        Console.WriteLine($"Core.SearchClosed 結果：{ClassicBinarySearch.SearchClosed(nums, target)}");

        Console.WriteLine();
        Console.WriteLine("--- 半開區間 [left, right) 過程 ---");
        TraceHalfOpen(nums, target);
        Console.WriteLine($"Core.SearchHalfOpen 結果：{ClassicBinarySearch.SearchHalfOpen(nums, target)}");

        Console.WriteLine();
        Console.WriteLine($"Array.IndexOf 暴力解對照：{Array.IndexOf(nums, target)}");
    }

    private static void TraceClosed(int[] nums, int target)
    {
        int left = 0;
        int right = nums.Length - 1;
        while (left <= right)
        {
            int mid = left + ((right - left) / 2);
            Console.WriteLine($"  left={left}, right={right}, mid={mid}, nums[mid]={nums[mid]}");
            if (nums[mid] == target)
            {
                Console.WriteLine($"  → 命中於索引 {mid}");
                return;
            }
            if (nums[mid] < target)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }
        Console.WriteLine("  → 未找到");
    }

    private static void TraceHalfOpen(int[] nums, int target)
    {
        int left = 0;
        int right = nums.Length;
        while (left < right)
        {
            int mid = left + ((right - left) / 2);
            Console.WriteLine($"  left={left}, right={right}, mid={mid}, nums[mid]={nums[mid]}");
            if (nums[mid] == target)
            {
                Console.WriteLine($"  → 命中於索引 {mid}");
                return;
            }
            if (nums[mid] < target)
            {
                left = mid + 1;
            }
            else
            {
                right = mid;
            }
        }
        Console.WriteLine("  → 未找到");
    }
}
