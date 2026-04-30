// 概念簡介：求「第一個 >= target」的索引（lower bound），等價於 target 的插入位置。
// 時間複雜度：O(log n)
// 空間複雜度：O(1)
// 適用情境：求插入位置、計數、配對 upper bound 求出現次數。

namespace BinarySearch.Core.Boundary;

/// <summary>
/// Lower Bound：回傳第一個「大於或等於」<c>target</c> 的索引；若所有元素皆小於 <c>target</c>，
/// 回傳 <c>nums.Length</c>（即 <c>target</c> 應插入的位置）。
/// </summary>
public static class LowerBound
{
    /// <summary>
    /// 採半開區間 <c>[left, right)</c> 寫法（求邊界類型最簡潔的範式）。
    /// </summary>
    /// <remarks>
    /// 區間語義：候選答案空間為 <c>[left, right)</c>。
    /// 迴圈不變式：<c>nums[left - 1] &lt; target</c>（若存在），<c>nums[right] &gt;= target</c>（若 <c>right &lt; n</c>）。
    /// 終止條件：<c>left == right</c>，此時 <c>left</c> 即為答案（第一個 &gt;= target 的位置；不存在則為 <c>n</c>）。
    /// </remarks>
    /// <param name="nums">已遞增排序的整數陣列。</param>
    /// <param name="target">目標值。</param>
    /// <returns>第一個 <c>nums[i] &gt;= target</c> 的索引 <c>i</c>；若不存在則為 <c>nums.Length</c>。</returns>
    /// <exception cref="ArgumentNullException">當 <paramref name="nums"/> 為 <see langword="null"/>。</exception>
    /// <example>
    /// <code>
    /// LowerBound.Find(new[] { 1, 2, 2, 3 }, 2); // 1
    /// LowerBound.Find(new[] { 1, 2, 2, 3 }, 4); // 4 (插入位置)
    /// </code>
    /// </example>
    public static int Find(int[] nums, int target)
    {
        ArgumentNullException.ThrowIfNull(nums);

        int left = 0;
        int right = nums.Length;

        while (left < right)
        {
            // 防 overflow 寫法
            int mid = left + ((right - left) / 2);

            if (nums[mid] < target)
            {
                // mid 已確認 < target，故第一個 >= target 必在 mid 右側
                left = mid + 1;
            }
            else
            {
                // nums[mid] >= target，mid 自身可能就是答案，故 right = mid（不可 -1）
                right = mid;
            }
        }

        return left;
    }
}
