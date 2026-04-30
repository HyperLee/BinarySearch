# BinarySearch — 二分搜尋完整教學專案

> 一個專門介紹並完整實作 **Binary Search（二分搜尋）** 各種變體的 .NET 10 教學型專案。所有程式碼註解、文件、範例皆以 **繁體中文** 撰寫。

---

## 1. 專案簡介

本專案以「Library + Console Demo + xUnit 測試」三層式組織，系統性地涵蓋二分搜尋的各種主流變體：

- 經典等值查找（**閉區間 / 半開區間** 雙寫法對照）
- 邊界查找（Lower Bound / Upper Bound / First & Last Occurrence）
- 旋轉排序陣列（含重複 / 不含重複）
- 無序陣列上的峰值搜尋
- **答案二分（Binary Search on Answer）**：Koko 吃香蕉、分割陣列最大和
- **浮點數二分**：平方根、單調函數求根

每個變體都附上完整 XML 文件註解、單元測試與互動式 Demo，並在 README 結尾提供 **20+ 題 LeetCode 對照題單**。

---

## 2. 二分搜尋核心概念

### 2.1 適用前提

- 資料**已排序**（升序或降序），或具備**單調性（monotonic property）**。
- 對於「答案二分」類型，搜尋空間的判定函數需滿足：若 `f(x) = true` 則 `f(x+1) = true`（或反之）。

### 2.2 時間複雜度 O(log n)

設搜尋空間長度為 `n`，每次將其縮減約一半，故經過 `k` 次後剩餘長度為 `n / 2^k`。當 `n / 2^k < 1` 時迴圈結束，即 `k > log₂ n`，所以**最壞時間複雜度為 O(log n)**。

---

## 3. 區間寫法對照（本專案重點）

| 寫法 | 迴圈條件 | 初始 `right` | mid 計算 | 收斂方向 | 結束時 |
|---|---|---|---|---|---|
| **閉區間 `[left, right]`** | `while (left <= right)` | `nums.Length - 1` | `left + (right - left) / 2` | `left = mid + 1` / `right = mid - 1` | `left == right + 1` |
| **半開區間 `[left, right)`** | `while (left < right)` | `nums.Length` | `left + (right - left) / 2` | `left = mid + 1` / `right = mid` | `left == right` |

### 程式碼片段

```csharp
// 閉區間
int left = 0, right = nums.Length - 1;
while (left <= right)
{
    int mid = left + (right - left) / 2;
    if (nums[mid] == target) return mid;
    if (nums[mid] < target) left = mid + 1;
    else                    right = mid - 1;
}
return -1;

// 半開區間
int left = 0, right = nums.Length;
while (left < right)
{
    int mid = left + (right - left) / 2;
    if (nums[mid] == target) return mid;
    if (nums[mid] < target) left = mid + 1;
    else                    right = mid;       // 注意：不是 mid - 1
}
return -1;
```

### 適用建議

- 找**精確值**：兩種皆可，閉區間直觀。
- 找**邊界（lower / upper bound、插入位置）**：半開區間較簡潔。

---

## 4. mid 取值與 Overflow 防範

- ❌ **錯誤**：`mid = (left + right) / 2;`
  - 當 `left + right` 超過 `int.MaxValue` 會 **整數溢位**，得到負值索引。
- ✅ **正確**：`mid = left + (right - left) / 2;`
  - 等價於上式但永不溢位。
- 🔁 **偏右取值（避免死迴圈）**：`mid = left + (right - left + 1) / 2;`
  - 適用於 `left = mid` 的情境（如 `FindLast`）。一般 mid 為「向下取整」，當 `right = left + 1` 且採用 `left = mid` 更新時 `mid == left`，會原地踏步死迴圈；改用「向上取整」的偏右 mid 即可保證每輪有實質進展。

---

## 5. 終止條件解析

- **`while (left <= right)` 結束時** → `left == right + 1`：
  - 表示閉區間 `[left, right]` 已清空（變為 `[r+1, r]`）。
  - 在「Lower Bound 風格」的閉區間實作中，最終 `left` 即為「第一個 >= target」的位置。
- **`while (left < right)` 結束時** → `left == right`：
  - 表示半開區間 `[left, right)` 已縮成空集合，但 `left` 本身指向「答案位置」（插入位置 / 邊界）。
  - 半開區間天然契合邊界類問題。

---

## 6. 變體導覽

| # | 變體 | 適用情境 | 對應 `Core` 類別 |
|---|---|---|---|
| 4.1 | 經典二分搜尋（閉/半開雙寫法） | 在已排序陣列找精確值 | [`Basic/ClassicBinarySearch`](BinarySearch.Core/Basic/ClassicBinarySearch.cs) |
| 4.2 | Lower Bound | 求第一個 ≥ target / 插入位置 | [`Boundary/LowerBound`](BinarySearch.Core/Boundary/LowerBound.cs) |
| 4.3 | Upper Bound（含 CountEqual） | 求第一個 > target / 計算重複次數 | [`Boundary/UpperBound`](BinarySearch.Core/Boundary/UpperBound.cs) |
| 4.4 | First / Last Occurrence | 重複值的首尾位置（示範偏右 mid） | [`Boundary/FirstLastOccurrence`](BinarySearch.Core/Boundary/FirstLastOccurrence.cs) |
| 4.5 | 旋轉排序陣列搜尋 | 旋轉後仍保留半段有序的特性 | [`Rotated/RotatedArraySearch`](BinarySearch.Core/Rotated/RotatedArraySearch.cs) |
| 4.6 | 峰值元素 | 無序陣列利用「相鄰不等」隱含的單調性 | [`Peak/FindPeakElement`](BinarySearch.Core/Peak/FindPeakElement.cs) |
| 4.7a | 答案二分：Koko 吃香蕉 | 對「速度」搜尋最小可行值 | [`Answer/KokoEatingBananas`](BinarySearch.Core/Answer/KokoEatingBananas.cs) |
| 4.7b | 答案二分：分割陣列最大和 | 對「上限」搜尋最小可行值 | [`Answer/SplitArrayLargestSum`](BinarySearch.Core/Answer/SplitArrayLargestSum.cs) |
| 4.8 | 浮點數二分 | 連續搜尋空間的求根 / 求值 | [`FloatingPoint/FloatingPointBinarySearch`](BinarySearch.Core/FloatingPoint/FloatingPointBinarySearch.cs) |

---

## 7. 常見陷阱與除錯技巧

1. **死迴圈**
   - 區間更新與 mid 取值不一致：例如閉區間使用 `left = mid` 而非 `left = mid + 1`，且 mid 為向下取整 → 死迴圈。修正：改用偏右 mid。
   - 浮點數二分使用 `while (lo < hi)` → 永遠不收斂。修正：改用 `epsilon` 收斂或固定迭代次數。
2. **Off-by-one**
   - 閉區間誤寫成 `while (left < right)` 會漏判 `left == right` 的單點。
   - 半開區間誤寫成 `right = mid - 1` 會跳過真正的答案。
3. **邊界更新方向錯誤**
   - 收斂條件與「mid 是否仍是候選」必須一致：`<= target` 配 `left = mid + 1`，否則需要保留 mid。
4. **整數溢位**
   - 一律使用 `left + (right - left) / 2`。

---

## 8. 如何執行

```bash
# 還原與建置（路徑為本 repo 根目錄）
dotnet build

# 啟動互動式 Demo
dotnet run --project BinarySearch

# 一鍵執行所有 Demo（適合 CI 驗證）
dotnet run --project BinarySearch -- --all

# 執行單元測試
dotnet test
```

互動式選單範例：

```
=== Binary Search 教學 Demo ===
1) 經典二分搜尋（閉區間 vs 半開區間對照）
2) Lower Bound（第一個 >= target）
3) Upper Bound（第一個 > target）
4) First / Last Occurrence（重複值首尾位置）
5) 旋轉排序陣列搜尋
6) 峰值元素（FindPeakElement）
7) 答案二分：Koko 吃香蕉
8) 答案二分：分割陣列最大和
9) 浮點數二分：求平方根
0) 離開
```

---

## 9. LeetCode 推薦題單

> 共 **23 題**，依難度分組；每題附「題號 / 中文題名 / 對應變體 / 連結」。

### Easy（6 題）

| 題號 | 題目 | 對應變體 | 連結 |
|---|---|---|---|
| 704 | 二分查詢（Binary Search） | 4.1 經典 | [link](https://leetcode.cn/problems/binary-search/) |
| 35  | 搜尋插入位置（Search Insert Position） | 4.2 Lower Bound | [link](https://leetcode.cn/problems/search-insert-position/) |
| 278 | 第一個錯誤的版本（First Bad Version） | 4.2 Lower Bound | [link](https://leetcode.cn/problems/first-bad-version/) |
| 374 | 猜數字大小（Guess Number Higher or Lower） | 4.1 經典 | [link](https://leetcode.cn/problems/guess-number-higher-or-lower/) |
| 69  | x 的平方根（Sqrt(x)） | 4.8 浮點 / 整數變形 | [link](https://leetcode.cn/problems/sqrtx/) |
| 367 | 有效的完全平方數（Valid Perfect Square） | 4.7 答案二分 | [link](https://leetcode.cn/problems/valid-perfect-square/) |

### Medium（12 題）

| 題號 | 題目 | 對應變體 | 連結 |
|---|---|---|---|
| 34  | 在排序陣列中查找元素的第一個和最後一個位置 | 4.4 First/Last | [link](https://leetcode.cn/problems/find-first-and-last-position-of-element-in-sorted-array/) |
| 33  | 搜尋旋轉排序陣列 | 4.5 旋轉（不重複） | [link](https://leetcode.cn/problems/search-in-rotated-sorted-array/) |
| 81  | 搜尋旋轉排序陣列 II | 4.5 旋轉（含重複） | [link](https://leetcode.cn/problems/search-in-rotated-sorted-array-ii/) |
| 153 | 尋找旋轉排序陣列中的最小值 | 4.5 旋轉變形 | [link](https://leetcode.cn/problems/find-minimum-in-rotated-sorted-array/) |
| 162 | 尋找峰值（Find Peak Element） | 4.6 峰值 | [link](https://leetcode.cn/problems/find-peak-element/) |
| 74  | 搜尋二維矩陣 | 4.1 經典（一維化） | [link](https://leetcode.cn/problems/search-a-2d-matrix/) |
| 240 | 搜尋二維矩陣 II | 階梯搜尋（延伸） | [link](https://leetcode.cn/problems/search-a-2d-matrix-ii/) |
| 875 | 愛吃香蕉的 Koko | 4.7 答案二分 | [link](https://leetcode.cn/problems/koko-eating-bananas/) |
| 1011| 在 D 天內送達包裹的能力 | 4.7 答案二分 | [link](https://leetcode.cn/problems/capacity-to-ship-packages-within-d-days/) |
| 540 | 有序陣列中的單一元素 | 4.1 經典變形 | [link](https://leetcode.cn/problems/single-element-in-a-sorted-array/) |
| 658 | 找到 K 個最接近的元素 | 4.2 Lower Bound + 雙指針 | [link](https://leetcode.cn/problems/find-k-closest-elements/) |
| 287 | 尋找重複數 | 4.7 答案二分 | [link](https://leetcode.cn/problems/find-the-duplicate-number/) |

### Hard（5 題）

| 題號 | 題目 | 對應變體 | 連結 |
|---|---|---|---|
| 4   | 兩個排序陣列的中位數 | 4.1 經典變形（雙陣列） | [link](https://leetcode.cn/problems/median-of-two-sorted-arrays/) |
| 410 | 分割陣列的最大值 | 4.7 答案二分 | [link](https://leetcode.cn/problems/split-array-largest-sum/) |
| 668 | 乘法表中第 K 小的數 | 4.7 答案二分 | [link](https://leetcode.cn/problems/kth-smallest-number-in-multiplication-table/) |
| 719 | 找出第 K 小的數對距離 | 4.7 答案二分 | [link](https://leetcode.cn/problems/find-k-th-smallest-pair-distance/) |
| 878 | 第 N 個神奇數字 | 4.7 答案二分 | [link](https://leetcode.cn/problems/nth-magical-number/) |

> 進階補充（皆為答案二分經典題）：LC 786、1283、1482、2226、2616。

---

## 10. 專案結構

```
BinarySearch/
├── BinarySearch.slnx
├── README.md                                 # 本文件
├── BinarySearch/                             # Console Demo（互動式選單）
│   ├── BinarySearch.csproj
│   ├── Program.cs
│   └── Demos/
├── BinarySearch.Core/                        # 演算法函式庫（純邏輯）
│   ├── Basic/ClassicBinarySearch.cs
│   ├── Boundary/{LowerBound,UpperBound,FirstLastOccurrence}.cs
│   ├── Rotated/RotatedArraySearch.cs
│   ├── Peak/FindPeakElement.cs
│   ├── Answer/{KokoEatingBananas,SplitArrayLargestSum}.cs
│   └── FloatingPoint/FloatingPointBinarySearch.cs
└── BinarySearch.Tests/                       # xUnit 單元測試
```

所有專案 `TargetFramework = net10.0`，啟用 `<Nullable>enable</Nullable>` 與 `<ImplicitUsings>enable</ImplicitUsings>`。

---

## 11. 驗收門檻

- ✅ `dotnet build` 0 警告 0 錯誤
- ✅ `dotnet test` 全部通過
- ✅ `dotnet run --project BinarySearch` 可進入互動選單並執行所有變體
- ✅ 經典版同時提供閉區間 + 半開區間實作，並對照註解差異
- ✅ `FindLast` 註解明確說明「為何使用偏右 mid」
- ✅ 所有 mid 計算皆使用 `left + (right - left) / 2` 形式
