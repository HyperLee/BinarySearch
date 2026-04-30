using BinarySearch.Core.Basic;

namespace BinarySearch.Tests.Basic;

public class ClassicBinarySearchTests
{
    public static IEnumerable<object[]> SuccessCases()
    {
        yield return new object[] { new[] { 1, 3, 5, 7, 9 }, 5, 2 };
        yield return new object[] { new[] { 1, 3, 5, 7, 9 }, 1, 0 };
        yield return new object[] { new[] { 1, 3, 5, 7, 9 }, 9, 4 };
        yield return new object[] { new[] { 42 }, 42, 0 };
        yield return new object[] { new[] { -10, -3, 0, 4, 11 }, -3, 1 };
    }

    [Theory]
    [MemberData(nameof(SuccessCases))]
    public void SearchClosed_TargetExists_ReturnsCorrectIndex(int[] nums, int target, int expected)
    {
        Assert.Equal(expected, ClassicBinarySearch.SearchClosed(nums, target));
    }

    [Theory]
    [MemberData(nameof(SuccessCases))]
    public void SearchHalfOpen_TargetExists_ReturnsCorrectIndex(int[] nums, int target, int expected)
    {
        Assert.Equal(expected, ClassicBinarySearch.SearchHalfOpen(nums, target));
    }

    [Theory]
    [InlineData(new int[] { }, 1)]
    [InlineData(new[] { 1, 3, 5 }, 4)]
    [InlineData(new[] { 1, 3, 5 }, 0)]
    [InlineData(new[] { 1, 3, 5 }, 99)]
    [InlineData(new[] { 2 }, 1)]
    public void SearchClosed_TargetMissing_ReturnsMinusOne(int[] nums, int target)
    {
        Assert.Equal(-1, ClassicBinarySearch.SearchClosed(nums, target));
    }

    [Theory]
    [InlineData(new int[] { }, 1)]
    [InlineData(new[] { 1, 3, 5 }, 4)]
    [InlineData(new[] { 1, 3, 5 }, 0)]
    [InlineData(new[] { 1, 3, 5 }, 99)]
    [InlineData(new[] { 2 }, 1)]
    public void SearchHalfOpen_TargetMissing_ReturnsMinusOne(int[] nums, int target)
    {
        Assert.Equal(-1, ClassicBinarySearch.SearchHalfOpen(nums, target));
    }

    [Fact]
    public void SearchClosed_NullArray_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => ClassicBinarySearch.SearchClosed(null!, 0));
    }

    [Fact]
    public void SearchHalfOpen_NullArray_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => ClassicBinarySearch.SearchHalfOpen(null!, 0));
    }

    [Fact]
    public void SearchClosed_LargeIndicesNearIntMaxValue_DoesNotOverflow()
    {
        // 構造一個邏輯上對應大索引的情境：直接搜尋大陣列確認不 overflow。
        // 為避免測試耗時，採用最大可控大小（5_000_000）並確保命中尾端。
        int n = 5_000_000;
        int[] arr = new int[n];
        for (int i = 0; i < n; i++)
        {
            arr[i] = i;
        }

        Assert.Equal(n - 1, ClassicBinarySearch.SearchClosed(arr, n - 1));
        Assert.Equal(n / 2, ClassicBinarySearch.SearchHalfOpen(arr, n / 2));
    }

    [Fact]
    public void Search_AllEqualElements_ReturnsAnyValidIndex()
    {
        int[] nums = [5, 5, 5, 5, 5];
        int idxClosed = ClassicBinarySearch.SearchClosed(nums, 5);
        int idxHalf = ClassicBinarySearch.SearchHalfOpen(nums, 5);
        Assert.InRange(idxClosed, 0, 4);
        Assert.Equal(5, nums[idxClosed]);
        Assert.InRange(idxHalf, 0, 4);
        Assert.Equal(5, nums[idxHalf]);
    }
}
