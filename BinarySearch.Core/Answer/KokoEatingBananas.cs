// 概念簡介：答案二分（Binary Search on Answer）— LeetCode 875。
// 搜尋空間：吃香蕉速度 k ∈ [1, max(piles)]，對速度具單調性：
//   若 k 可在 h 小時吃完，則 k+1 亦可 ⇒ 「可行」集合是右閉的單調區間 ⇒ 找最小可行值即 lower bound。
// 時間複雜度：O(n log M)，n = piles.Length，M = max(piles)
// 空間複雜度：O(1)

namespace BinarySearch.Core.Answer;

/// <summary>
/// 愛吃香蕉的 Koko：在 <paramref name="h"/> 小時內吃完所有香蕉的最小時速。
/// </summary>
public static class KokoEatingBananas
{
    /// <summary>
    /// 求最小吃香蕉速度 k，使得以速度 k 可在 h 小時內吃完所有香蕉。
    /// </summary>
    /// <remarks>
    /// 搜尋空間 <c>[lo, hi]</c>：
    /// <list type="bullet">
    ///   <item><c>lo = 1</c>：每小時至少 1 根。</item>
    ///   <item><c>hi = max(piles)</c>：以最大堆為速度則每堆 1 小時吃完，必可在 piles.Length &lt;= h 小時完成。</item>
    /// </list>
    /// 判定函式 <c>canFinish(k)</c> 計算所需總時數 <c>Σ ceil(piles[i] / k)</c>，相對 k 單調遞減：
    /// k 越快、所需時數越少。故「能在 h 小時內完成」之 k 形成右閉單調集合，使用半開區間 lower-bound 寫法。
    /// </remarks>
    /// <param name="piles">每堆香蕉數量；<c>piles.Length &lt;= h</c> 由題目保證。</param>
    /// <param name="h">總時數。</param>
    /// <returns>最小可行時速。</returns>
    /// <exception cref="ArgumentNullException">當 <paramref name="piles"/> 為 <see langword="null"/>。</exception>
    /// <exception cref="ArgumentException">當 <paramref name="piles"/> 為空或 <paramref name="h"/> &lt; <paramref name="piles"/>.Length。</exception>
    public static int MinEatingSpeed(int[] piles, int h)
    {
        ArgumentNullException.ThrowIfNull(piles);

        if (piles.Length == 0)
        {
            throw new ArgumentException("piles 不可為空。", nameof(piles));
        }

        if (h < piles.Length)
        {
            throw new ArgumentException("h 必須 >= piles.Length，否則無解。", nameof(h));
        }

        int lo = 1;
        int hi = 0;
        foreach (int p in piles)
        {
            if (p > hi)
            {
                hi = p;
            }
        }

        // 半開區間 lower bound 寫法：尋找最小可行 k
        // 為避免比較 hi 邊界外，使用 [lo, hi] 閉區間 + 額外比較亦可；此處採半開 [lo, hi+1)。
        int left = lo;
        int right = hi + 1;
        while (left < right)
        {
            int mid = left + ((right - left) / 2);
            if (CanFinish(piles, mid, h))
            {
                right = mid; // mid 可行，但可能還有更小可行解
            }
            else
            {
                left = mid + 1;
            }
        }

        return left;
    }

    // canFinish(k) 對 k 單調：k 越大耗時越少 ⇒ 可行集為 [k*, ∞)
    private static bool CanFinish(int[] piles, int k, int h)
    {
        long hours = 0;
        foreach (int p in piles)
        {
            // ceil(p / k) 用整數運算寫法
            hours += (p + k - 1) / k;
            if (hours > h)
            {
                return false; // 提前剪枝
            }
        }

        return hours <= h;
    }
}
