// 概念簡介：在「已排序」陣列中以對數時間定位 target 的索引。
// 時間複雜度：O(log n)
// 空間複雜度：O(1)
// 適用情境：標準等值查找；同時提供「閉區間」與「半開區間」兩種寫法以對照差異。

namespace BinarySearch.Core.Basic;

/// <summary>
/// 經典二分搜尋：同時提供閉區間 [l, r] 與半開區間 [l, r) 兩種等價實作，
/// 兩者皆使用 <c>mid = left + (right - left) / 2</c> 防止整數溢位。
/// </summary>
public static class ClassicBinarySearch
{
    /// <summary>
    /// 以「閉區間 [left, right]」實作的經典二分搜尋。
    /// </summary>
    /// <remarks>
    /// 區間語義：搜尋範圍為 <c>[left, right]</c>，<c>left</c> 與 <c>right</c> 皆為「合法可達」的索引。
    /// 迴圈不變式：若答案存在，必落於 <c>[left, right]</c>。
    /// 終止條件：<c>left > right</c>（亦即 <c>left == right + 1</c>），表示搜尋範圍清空、答案不存在。
    /// </remarks>
    /// <param name="nums">已遞增排序的整數陣列，不可為 <see langword="null"/>。</param>
    /// <param name="target">欲尋找的目標值。</param>
    /// <returns>命中索引；若不存在回傳 <c>-1</c>。</returns>
    /// <exception cref="ArgumentNullException">當 <paramref name="nums"/> 為 <see langword="null"/>。</exception>
    /// <example>
    /// <code>
    /// int idx = ClassicBinarySearch.SearchClosed(new[] { 1, 3, 5, 7, 9 }, 7); // 3
    /// </code>
    /// </example>
    public static int SearchClosed(int[] nums, int target)
    {
        ArgumentNullException.ThrowIfNull(nums);

        int left = 0;
        int right = nums.Length - 1; // 閉區間：right 為最後一個合法索引

        while (left <= right) // 閉區間使用 <=，否則會漏判 left == right 的單點
        {
            // 採 left + (right - left) / 2 而非 (left + right) / 2，避免相加 overflow
            int mid = left + ((right - left) / 2);

            if (nums[mid] == target)
            {
                return mid;
            }
            else if (nums[mid] < target)
            {
                left = mid + 1; // 閉區間：mid 已驗證過，必須 +1 排除，否則死迴圈
            }
            else
            {
                right = mid - 1; // 閉區間：同理 -1
            }
        }

        return -1;
    }

    /// <summary>
    /// 以「半開區間 [left, right)」實作的經典二分搜尋。
    /// </summary>
    /// <remarks>
    /// 區間語義：搜尋範圍為 <c>[left, right)</c>，<c>right</c> 為「不可達」的開界（哨兵）。
    /// 迴圈不變式：若答案存在，必落於 <c>[left, right)</c>。
    /// 終止條件：<c>left == right</c>，表示搜尋範圍清空。
    /// 與閉區間的關鍵差異：右邊界更新為 <c>right = mid</c>（不是 <c>mid - 1</c>），因為 <c>mid</c> 本來就在開界外的「下一個」候選。
    /// </remarks>
    /// <param name="nums">已遞增排序的整數陣列。</param>
    /// <param name="target">欲尋找的目標值。</param>
    /// <returns>命中索引；若不存在回傳 <c>-1</c>。</returns>
    /// <exception cref="ArgumentNullException">當 <paramref name="nums"/> 為 <see langword="null"/>。</exception>
    public static int SearchHalfOpen(int[] nums, int target)
    {
        ArgumentNullException.ThrowIfNull(nums);

        int left = 0;
        int right = nums.Length; // 半開區間：right 為開界，初始為 Length

        while (left < right) // 半開區間使用 <，因 left == right 時範圍已空
        {
            int mid = left + ((right - left) / 2);

            if (nums[mid] == target)
            {
                return mid;
            }
            else if (nums[mid] < target)
            {
                left = mid + 1;
            }
            else
            {
                right = mid; // 半開區間：mid 直接成為新開界即可排除自身
            }
        }

        return -1;
    }
}
