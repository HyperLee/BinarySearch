using BinarySearch.Core.Boundary;

namespace BinarySearch.Demos;

/// <summary>
/// First / Last Occurrence Demo：示範重複值首尾位置。
/// </summary>
public sealed class FirstLastOccurrenceDemo : IDemo
{
    public string Title => "First / Last Occurrence（重複值首尾位置）";

    public void Run()
    {
        int[] nums = [5, 7, 7, 8, 8, 8, 10];
        int[] targets = [8, 7, 6, 10];

        Console.WriteLine($"輸入陣列：[{string.Join(", ", nums)}]");
        foreach (int t in targets)
        {
            int first = FirstLastOccurrence.FindFirst(nums, t);
            int last = FirstLastOccurrence.FindLast(nums, t);
            int bruteFirst = Array.IndexOf(nums, t);
            int bruteLast = Array.LastIndexOf(nums, t);
            Console.WriteLine($"  target={t}：first = {first}，last = {last}（暴力解 first={bruteFirst}, last={bruteLast}）");
        }
    }
}
