// 概念簡介：找出陣列中任一「峰值」索引（嚴格大於兩側鄰居；兩端視為 -∞）。
// 為何在「無序」陣列也能用二分？關鍵：相鄰元素不相等，故區間端點存在「往上爬」方向，
// 於該方向必能抵達局部極大值——這就是支撐二分的單調性保證。
// 時間複雜度：O(log n)
// 空間複雜度：O(1)

namespace BinarySearch.Core.Peak;

/// <summary>
/// 尋找峰值元素（LeetCode 162）。
/// </summary>
public static class FindPeakElement
{
    /// <summary>
    /// 回傳任一峰值索引。假設輸入長度 &gt;= 1 且相鄰元素不相等。
    /// </summary>
    /// <remarks>
    /// 採半開區間 <c>[left, right)</c>，比較 <c>nums[mid]</c> 與 <c>nums[mid + 1]</c>：
    /// <list type="bullet">
    ///   <item>若 <c>nums[mid] &gt; nums[mid + 1]</c>：峰值必在左半（含 mid，因 mid 已是「下坡前」最高點之一）。</item>
    ///   <item>否則峰值必在右半（不含 mid）：因右側必存在「往上爬」方向，邊界視為 -∞，最終必形成峰值。</item>
    /// </list>
    /// </remarks>
    /// <param name="nums">非空整數陣列；相鄰元素不相等。</param>
    /// <returns>任一峰值索引。</returns>
    /// <exception cref="ArgumentNullException">當 <paramref name="nums"/> 為 <see langword="null"/>。</exception>
    /// <exception cref="ArgumentException">當 <paramref name="nums"/> 為空。</exception>
    public static int FindPeak(int[] nums)
    {
        ArgumentNullException.ThrowIfNull(nums);

        if (nums.Length == 0)
        {
            throw new ArgumentException("陣列不可為空。", nameof(nums));
        }

        int left = 0;
        int right = nums.Length - 1; // 注意：此處刻意使用閉區間風格但以 left < right 收斂

        while (left < right)
        {
            int mid = left + ((right - left) / 2);

            if (nums[mid] > nums[mid + 1])
            {
                // mid 為下坡起點，峰值必在 [left, mid]（保留 mid）
                right = mid;
            }
            else
            {
                // mid 為上坡，峰值必在 (mid, right]
                left = mid + 1;
            }
        }

        // 終止時 left == right，即為某個峰值
        return left;
    }
}
