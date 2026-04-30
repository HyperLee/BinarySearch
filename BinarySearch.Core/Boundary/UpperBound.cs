// 概念簡介：求「第一個 > target」的索引（upper bound）。
// 時間複雜度：O(log n)
// 空間複雜度：O(1)
// 適用情境：與 LowerBound 配對求 target 出現次數；插入到「重複值之後」的位置。

namespace BinarySearch.Core.Boundary;

/// <summary>
/// Upper Bound：回傳第一個「嚴格大於」<c>target</c> 的索引；若所有元素皆 &lt;= <c>target</c>，
/// 回傳 <c>nums.Length</c>。並提供 <see cref="CountEqual"/> 工具方法以計算 target 出現次數。
/// </summary>
public static class UpperBound
{
    /// <summary>
    /// 採半開區間 <c>[left, right)</c> 寫法。
    /// </summary>
    /// <remarks>
    /// 區間語義：候選空間為 <c>[left, right)</c>。
    /// 與 <see cref="LowerBound.Find(int[], int)"/> 唯一差別僅在比較條件由 <c>&lt;</c> 改為 <c>&lt;=</c>。
    /// </remarks>
    /// <param name="nums">已遞增排序的整數陣列。</param>
    /// <param name="target">目標值。</param>
    /// <returns>第一個 <c>nums[i] &gt; target</c> 的索引；若不存在則為 <c>nums.Length</c>。</returns>
    /// <exception cref="ArgumentNullException">當 <paramref name="nums"/> 為 <see langword="null"/>。</exception>
    public static int Find(int[] nums, int target)
    {
        ArgumentNullException.ThrowIfNull(nums);

        int left = 0;
        int right = nums.Length;

        while (left < right)
        {
            int mid = left + ((right - left) / 2);

            // 與 LowerBound 的差異：這裡用 <= 而非 <。
            // 將 nums[mid] == target 也視為「太小」，目的是把答案推到 target 出現的最右側之後。
            if (nums[mid] <= target)
            {
                left = mid + 1;
            }
            else
            {
                right = mid;
            }
        }

        return left;
    }

    /// <summary>
    /// 計算 <paramref name="target"/> 在已排序 <paramref name="nums"/> 中的出現次數。
    /// </summary>
    /// <remarks>UpperBound − LowerBound 即為個數，O(log n)。</remarks>
    /// <param name="nums">已遞增排序的整數陣列。</param>
    /// <param name="target">目標值。</param>
    /// <returns>出現次數，>= 0。</returns>
    /// <exception cref="ArgumentNullException">當 <paramref name="nums"/> 為 <see langword="null"/>。</exception>
    public static int CountEqual(int[] nums, int target)
    {
        ArgumentNullException.ThrowIfNull(nums);

        return Find(nums, target) - LowerBound.Find(nums, target);
    }
}
