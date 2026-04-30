// 概念簡介：答案二分 — LeetCode 410。
// 將陣列分成 k 個非空連續子陣列，求「各子陣列和的最大值」之最小值。
// 搜尋空間 [max(nums), sum(nums)]：
//   - 下界：必有一個子陣列至少包含最大元素，故答案 >= max(nums)。
//   - 上界：k = 1 時整段為一個子陣列，答案 = sum(nums)。
// 判定函式：給定上限 capacity，貪心累加切段，看所需段數是否 <= k；對 capacity 單調非遞增。
// 時間複雜度：O(n log S)，S = sum(nums)
// 空間複雜度：O(1)

namespace BinarySearch.Core.Answer;

/// <summary>
/// 分割陣列的最大值（LeetCode 410）。
/// </summary>
public static class SplitArrayLargestSum
{
    /// <summary>
    /// 將 <paramref name="nums"/> 分為 <paramref name="k"/> 個非空連續子陣列，使各子陣列和的最大值最小，回傳該最小值。
    /// </summary>
    /// <remarks>
    /// 設 <c>canSplit(cap)</c> 為「是否能在每段和 &lt;= cap 的限制下將陣列切為 &lt;= k 段」，
    /// 顯然 cap 越大越容易切成更少段 ⇒ <c>canSplit</c> 對 cap 單調遞增 ⇒ 使用 lower bound 找最小可行 cap。
    /// </remarks>
    /// <param name="nums">非空、非負整數陣列。</param>
    /// <param name="k">切段數，<c>1 &lt;= k &lt;= nums.Length</c>。</param>
    /// <exception cref="ArgumentNullException">當 <paramref name="nums"/> 為 <see langword="null"/>。</exception>
    /// <exception cref="ArgumentException">當 <paramref name="nums"/> 為空、含負值或 <paramref name="k"/> 不在合法範圍。</exception>
    public static int SplitArray(int[] nums, int k)
    {
        ArgumentNullException.ThrowIfNull(nums);

        if (nums.Length == 0)
        {
            throw new ArgumentException("nums 不可為空。", nameof(nums));
        }

        if (k < 1 || k > nums.Length)
        {
            throw new ArgumentException("k 必須在 [1, nums.Length] 範圍內。", nameof(k));
        }

        long lo = 0; // = max(nums)
        long hi = 0; // = sum(nums)
        foreach (int v in nums)
        {
            if (v < 0)
            {
                throw new ArgumentException("nums 不可含負值。", nameof(nums));
            }
            if (v > lo)
            {
                lo = v;
            }
            hi += v;
        }

        // 半開區間 lower bound 寫法：尋找最小可行 capacity
        long left = lo;
        long right = hi + 1;
        while (left < right)
        {
            long mid = left + ((right - left) / 2);
            if (CanSplit(nums, mid, k))
            {
                right = mid;
            }
            else
            {
                left = mid + 1;
            }
        }

        return checked((int)left);
    }

    // 貪心：以 capacity 為上限累加，超過則新開一段；統計所需段數是否 <= k
    private static bool CanSplit(int[] nums, long capacity, int k)
    {
        int segments = 1;
        long currentSum = 0;
        foreach (int v in nums)
        {
            if (currentSum + v > capacity)
            {
                segments++;
                currentSum = v;
                if (segments > k)
                {
                    return false;
                }
            }
            else
            {
                currentSum += v;
            }
        }

        return true;
    }
}
