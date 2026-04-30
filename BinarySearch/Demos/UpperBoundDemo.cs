using BinarySearch.Core.Boundary;

namespace BinarySearch.Demos;

/// <summary>
/// Upper Bound Demo：第一個 &gt; target，並示範 CountEqual。
/// </summary>
public sealed class UpperBoundDemo : IDemo
{
    public string Title => "Upper Bound（第一個 > target）";

    public void Run()
    {
        int[] nums = [1, 2, 2, 2, 3, 5, 8];
        int[] targets = [0, 2, 4, 9];

        Console.WriteLine($"輸入陣列：[{string.Join(", ", nums)}]");
        foreach (int t in targets)
        {
            int upper = UpperBound.Find(nums, t);
            int count = UpperBound.CountEqual(nums, t);
            int bruteCount = nums.Count(x => x == t);
            Console.WriteLine($"  target={t}：UpperBound = {upper}，CountEqual = {count}（暴力解計數 = {bruteCount}）");
        }
    }
}
