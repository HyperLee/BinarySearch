// 概念簡介：在含重複值的已排序陣列中找 target 的「第一個 / 最後一個」位置。
// 時間複雜度：O(log n)
// 空間複雜度：O(1)
// 適用情境：LeetCode 34、計算重複次數。

namespace BinarySearch.Core.Boundary;

/// <summary>
/// 在已排序陣列中尋找 <c>target</c> 的第一個與最後一個出現位置；找不到回傳 <c>-1</c>。
/// </summary>
public static class FirstLastOccurrence
{
    /// <summary>
    /// 第一個等於 <c>target</c> 的索引；不存在回傳 <c>-1</c>。
    /// </summary>
    /// <remarks>
    /// 實作策略：直接呼叫 <see cref="LowerBound.Find(int[], int)"/>，再驗證該位置確實等於 target。
    /// </remarks>
    /// <exception cref="ArgumentNullException">當 <paramref name="nums"/> 為 <see langword="null"/>。</exception>
    public static int FindFirst(int[] nums, int target)
    {
        ArgumentNullException.ThrowIfNull(nums);

        int idx = LowerBound.Find(nums, target);
        return idx < nums.Length && nums[idx] == target ? idx : -1;
    }

    /// <summary>
    /// 最後一個等於 <c>target</c> 的索引；不存在回傳 <c>-1</c>。
    /// 採用閉區間 <c>[l, r]</c> + <b>偏右 mid</b> 直接示範該寫法。
    /// </summary>
    /// <remarks>
    /// 區間語義：閉區間 <c>[left, right]</c>，皆為合法索引。
    /// 迴圈不變式：若答案存在，必在 <c>[left, right]</c> 中；<c>nums[left]</c> 可能等於 target。
    /// 為何使用偏右 mid（<c>mid = left + (right - left + 1) / 2</c>）：
    ///   本實作在 <c>nums[mid] &lt;= target</c> 分支採用 <c>left = mid</c>（不 +1，因 mid 自身可能即是最後一個 target），
    ///   此時若採用一般 mid（向下取整），當 <c>right = left + 1</c> 時 <c>mid == left</c>，
    ///   一旦進入 <c>left = mid</c> 分支即原地踏步，造成死迴圈。
    ///   改用「向上取整」的偏右 mid 可保證 <c>mid &gt; left</c>，每輪皆有實質進展。
    /// 終止條件：<c>left == right</c>，再驗證 <c>nums[left] == target</c>。
    /// </remarks>
    /// <exception cref="ArgumentNullException">當 <paramref name="nums"/> 為 <see langword="null"/>。</exception>
    public static int FindLast(int[] nums, int target)
    {
        ArgumentNullException.ThrowIfNull(nums);

        if (nums.Length == 0)
        {
            return -1;
        }

        int left = 0;
        int right = nums.Length - 1;

        while (left < right)
        {
            // 偏右 mid：向上取整。配合下方 left = mid 的更新方式以避免死迴圈。
            int mid = left + ((right - left + 1) / 2);

            if (nums[mid] <= target)
            {
                // mid 自身可能就是最後一個 target，故保留 mid（left = mid，不 +1）
                left = mid;
            }
            else
            {
                right = mid - 1;
            }
        }

        return nums[left] == target ? left : -1;
    }
}
