using BinarySearch.Core.Rotated;

namespace BinarySearch.Demos;

/// <summary>
/// 旋轉排序陣列 Demo：示範不重複版本與允許重複版本。
/// </summary>
public sealed class RotatedArrayDemo : IDemo
{
    public string Title => "旋轉排序陣列搜尋";

    public void Run()
    {
        int[] noDup = [4, 5, 6, 7, 0, 1, 2];
        int[] withDup = [2, 5, 6, 0, 0, 1, 2];
        int[] targetsNoDup = [0, 3, 7];
        int[] targetsDup = [0, 3];

        Console.WriteLine($"不重複版陣列：[{string.Join(", ", noDup)}]");
        foreach (int t in targetsNoDup)
        {
            int result = RotatedArraySearch.Search(noDup, t);
            int brute = Array.IndexOf(noDup, t);
            Console.WriteLine($"  target={t}：Search = {result}，暴力解 = {brute}");
        }

        Console.WriteLine();
        Console.WriteLine($"含重複值版陣列：[{string.Join(", ", withDup)}]");
        foreach (int t in targetsDup)
        {
            bool result = RotatedArraySearch.SearchWithDuplicates(withDup, t);
            bool brute = Array.IndexOf(withDup, t) >= 0;
            Console.WriteLine($"  target={t}：SearchWithDuplicates = {result}，暴力解 = {brute}");
        }
    }
}
