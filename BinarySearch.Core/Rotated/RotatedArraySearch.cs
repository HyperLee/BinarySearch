// 概念簡介：在「升序陣列旋轉一次」後的陣列中搜尋 target。
// 關鍵觀察：以 mid 為界，左右兩半至少有一半仍是有序的；判斷 target 是否落入該有序半，再決定收斂方向。
// 時間複雜度：不重複版 O(log n)；含重複值最壞退化至 O(n)。
// 空間複雜度：O(1)

namespace BinarySearch.Core.Rotated;

/// <summary>
/// 旋轉排序陣列搜尋（LeetCode 33 / 81）。
/// </summary>
public static class RotatedArraySearch
{
    /// <summary>
    /// 元素皆不重複版本（LC 33）：找到 target 的索引；不存在回傳 <c>-1</c>。
    /// </summary>
    /// <remarks>
    /// 採閉區間 <c>[l, r]</c>。每一輪以 <c>nums[left] &lt;= nums[mid]</c> 判斷「左半段是否有序」：
    /// <list type="bullet">
    ///   <item>若左半有序，且 target 在 <c>[nums[left], nums[mid])</c> 內，往左收斂；否則往右。</item>
    ///   <item>否則右半必有序，類似處理。</item>
    /// </list>
    /// </remarks>
    /// <exception cref="ArgumentNullException">當 <paramref name="nums"/> 為 <see langword="null"/>。</exception>
    public static int Search(int[] nums, int target)
    {
        ArgumentNullException.ThrowIfNull(nums);

        int left = 0;
        int right = nums.Length - 1;

        while (left <= right)
        {
            int mid = left + ((right - left) / 2);

            if (nums[mid] == target)
            {
                return mid;
            }

            // 判斷哪一半「絕對有序」：左半 nums[left..mid] 有序 ⇔ nums[left] <= nums[mid]
            if (nums[left] <= nums[mid])
            {
                // 左半有序：若 target 落在 [nums[left], nums[mid]) 區間內則往左收斂
                if (nums[left] <= target && target < nums[mid])
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }
            else
            {
                // 右半 nums[mid..right] 必有序
                if (nums[mid] < target && target <= nums[right])
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
        }

        return -1;
    }

    /// <summary>
    /// 含重複值版本（LC 81）：回傳是否存在 <paramref name="target"/>。
    /// </summary>
    /// <remarks>
    /// 重複值情境下，<c>nums[left] == nums[mid] == nums[right]</c> 時無法判定哪半有序，
    /// 必須以 <c>left++; right--;</c> 線性收縮兩端，最壞退化為 O(n)。
    /// </remarks>
    /// <exception cref="ArgumentNullException">當 <paramref name="nums"/> 為 <see langword="null"/>。</exception>
    public static bool SearchWithDuplicates(int[] nums, int target)
    {
        ArgumentNullException.ThrowIfNull(nums);

        int left = 0;
        int right = nums.Length - 1;

        while (left <= right)
        {
            int mid = left + ((right - left) / 2);

            if (nums[mid] == target)
            {
                return true;
            }

            // 處理三端相等：無法判定有序的那一半，只能線性收縮
            if (nums[left] == nums[mid] && nums[mid] == nums[right])
            {
                left++;
                right--;
            }
            else if (nums[left] <= nums[mid])
            {
                if (nums[left] <= target && target < nums[mid])
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }
            else
            {
                if (nums[mid] < target && target <= nums[right])
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
        }

        return false;
    }
}
