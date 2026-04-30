using BinarySearch.Core.Answer;

namespace BinarySearch.Demos;

/// <summary>
/// 答案二分 Demo：Koko 吃香蕉（LC 875）與分割陣列最大和（LC 410）。
/// </summary>
public sealed class BinarySearchOnAnswerDemo : IDemo
{
    public string Title => "答案二分：Koko 吃香蕉";

    public void Run()
    {
        int[] piles = [3, 6, 7, 11];
        int h = 8;
        int speed = KokoEatingBananas.MinEatingSpeed(piles, h);
        Console.WriteLine($"piles = [{string.Join(", ", piles)}]，h = {h}");
        Console.WriteLine($"最小可行速度 = {speed}");
        Console.WriteLine($"暴力線性掃描驗證 = {BruteMinSpeed(piles, h)}");
    }

    private static int BruteMinSpeed(int[] piles, int h)
    {
        int max = piles.Max();
        for (int k = 1; k <= max; k++)
        {
            long hours = 0;
            foreach (int p in piles)
            {
                hours += (p + k - 1) / k;
            }
            if (hours <= h)
            {
                return k;
            }
        }
        return max;
    }
}
