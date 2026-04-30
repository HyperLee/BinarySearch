# BinarySearch 開發規格書

> 本文件為 **BinarySearch** 專案的開發規格書（Development Specification），用於指引整體實作方向、模組劃分、程式碼風格與驗收標準。所有產出物（程式碼、註解、文件）皆使用 **繁體中文**。

---

## 1. 專案目標

打造一個專門介紹並完整實作 **Binary Search（二分搜尋）** 演算法的 .NET 10 教學型專案，目標讀者為：

- 想系統性學習二分搜尋各種變體的 C# 開發者
- 準備 LeetCode / 演算法面試的工程師
- 想理解「閉區間 vs 半開區間」、「mid 取值與 overflow 防範」等細節差異的學習者

成果應同時具備：
1. **可運行的範例（Demo）**：每個變體皆能透過 Console 執行展示。
2. **可驗證的測試（xUnit）**：每個變體皆有完整單元測試，涵蓋邊界 case。
3. **可閱讀的文件（README）**：說明概念、適用情境、常見陷阱與 LeetCode 練習題單。

---

## 2. 核心演算法概念（必須在 README 與註解中清楚闡述）

### 2.1 適用前提
- 資料必須是**已排序（sorted）**或具備**單調性（monotonic property）**。
- 對於「答案二分」類型，搜尋空間需滿足：若 `f(x) = true` 則 `f(x+1) = true`（或反之）。

### 2.2 區間寫法（**本專案重點之一，必須兩種並列**）

| 寫法 | 迴圈條件 | 初始 right | mid 計算 | 收斂方向 |
|------|----------|------------|----------|----------|
| **閉區間 `[left, right]`** | `while (left <= right)` | `nums.Length - 1` | `left + (right - left) / 2` | `left = mid + 1` / `right = mid - 1` |
| **半開區間 `[left, right)`** | `while (left < right)` | `nums.Length` | `left + (right - left) / 2` | `left = mid + 1` / `right = mid` |

- 終止條件差異：
  - 閉區間迴圈結束時 `left == right + 1`（搜尋失敗）。
  - 半開區間迴圈結束時 `left == right`（指向插入位置或下界）。
- 使用時機：
  - 找**精確值**：兩種皆可，閉區間直觀。
  - 找**邊界（lower / upper bound、插入位置）**：半開區間較簡潔。

### 2.3 mid 取值與 Overflow 防範
- **錯誤寫法**：`mid = (left + right) / 2;` → 當 `left + right` 超過 `int.MaxValue` 會 overflow。
- **正確寫法**：`mid = left + (right - left) / 2;`
- **偏右取值（用於避免某些變體死迴圈）**：`mid = left + (right - left + 1) / 2;`
  - 適用於 `left = mid` 的情境（例如 `right = mid - 1` 但 `left = mid` 時，若使用一般 mid 會死迴圈）。
- 註解中必須標示「為什麼這裡要用偏右 mid」。

### 2.4 常見死迴圈陷阱
- 區間更新與 mid 取值不一致（例如閉區間用 `left = mid` 而非 `left = mid + 1`）。
- 終止條件與區間定義不一致。
- 規格書要求每個變體在註解中標註：**區間語義、迴圈不變式、終止條件、回傳值意義**。

---

## 3. 專案結構

採 **Library + Demo + xUnit 測試** 三層式組織：

```
BinarySearch/
├── BinarySearch.slnx
├── README.md                              # 概念說明 + LeetCode 題單（繁中）
├── BinarySearch/                          # 主程式（Console Demo）
│   ├── BinarySearch.csproj
│   ├── Program.cs                         # 互動式選單，展示各變體
│   └── Demos/                             # 每個變體一支 Demo 類別
│       ├── ClassicSearchDemo.cs
│       ├── LowerBoundDemo.cs
│       ├── UpperBoundDemo.cs
│       ├── FirstLastOccurrenceDemo.cs
│       ├── RotatedArrayDemo.cs
│       ├── PeakElementDemo.cs
│       ├── BinarySearchOnAnswerDemo.cs
│       └── FloatingPointBinarySearchDemo.cs
├── BinarySearch.Core/                     # 演算法函式庫（純邏輯，無 IO）
│   ├── BinarySearch.Core.csproj
│   ├── Basic/
│   │   ├── ClassicBinarySearch.cs         # 經典版（閉區間 + 半開區間兩種實作）
│   ├── Boundary/
│   │   ├── LowerBound.cs                  # 第一個 >= target
│   │   ├── UpperBound.cs                  # 第一個 > target
│   │   └── FirstLastOccurrence.cs         # 重複值首尾位置
│   ├── Rotated/
│   │   └── RotatedArraySearch.cs          # 旋轉排序陣列（含含重複/不含重複版本）
│   ├── Peak/
│   │   └── FindPeakElement.cs
│   ├── Answer/                            # 答案二分（Binary Search on Answer）
│   │   ├── KokoEatingBananas.cs
│   │   └── SplitArrayLargestSum.cs
│   └── FloatingPoint/
│       └── FloatingPointBinarySearch.cs   # 實數二分（如平方根、單調函數求根）
└── BinarySearch.Tests/                    # xUnit 單元測試
    ├── BinarySearch.Tests.csproj
    ├── Basic/
    │   └── ClassicBinarySearchTests.cs
    ├── Boundary/
    │   ├── LowerBoundTests.cs
    │   ├── UpperBoundTests.cs
    │   └── FirstLastOccurrenceTests.cs
    ├── Rotated/
    │   └── RotatedArraySearchTests.cs
    ├── Peak/
    │   └── FindPeakElementTests.cs
    ├── Answer/
    │   ├── KokoEatingBananasTests.cs
    │   └── SplitArrayLargestSumTests.cs
    └── FloatingPoint/
        └── FloatingPointBinarySearchTests.cs
```

### 解決方案檔（BinarySearch.slnx）需包含三個專案：
- `BinarySearch`（Console Demo，OutputType=Exe）
- `BinarySearch.Core`（ClassLib，被 Demo 與 Tests 引用）
- `BinarySearch.Tests`（xUnit 測試專案）

所有專案 TFM = `net10.0`，啟用 `<Nullable>enable</Nullable>` 與 `<ImplicitUsings>enable</ImplicitUsings>`。

---

## 4. 實作變體清單與規格

每個變體在 `BinarySearch.Core` 中皆需：
- 提供 `public static` 方法（純函式、無副作用）。
- 完整 XML doc 註解（`<summary>`、`<param>`、`<returns>`、`<example>`）。
- 行內註解標註「區間語義」、「為何 mid 這樣取」、「為何邊界這樣更新」。
- 輸入參數做 null / 空陣列 / 邊界檢查，並擲出明確例外（`ArgumentNullException`、`ArgumentException`）。

### 4.1 經典版二分搜尋（ClassicBinarySearch）
- 需求：在已排序陣列中找出 `target` 的索引；找不到回傳 `-1`。
- **同時提供兩種實作**：
  - `SearchClosed(int[] nums, int target)`：閉區間 `[l, r]`。
  - `SearchHalfOpen(int[] nums, int target)`：半開區間 `[l, r)`。
- 兩個方法的註解必須對照說明區間差異。

### 4.2 Lower Bound（LowerBound）
- 回傳第一個 `>= target` 的索引；若不存在則回傳 `nums.Length`（即插入位置）。
- 採半開區間實作（推薦寫法）。

### 4.3 Upper Bound（UpperBound）
- 回傳第一個 `> target` 的索引；若不存在則回傳 `nums.Length`。
- 採半開區間實作。
- 額外提供 `CountEqual(int[] nums, int target)` = `UpperBound - LowerBound`。

### 4.4 First / Last Occurrence（FirstLastOccurrence）
- `FindFirst(int[] nums, int target)`：第一個等於 target 的索引，找不到回傳 `-1`。
- `FindLast(int[] nums, int target)`：最後一個等於 target 的索引，找不到回傳 `-1`。
- 註解需說明「`FindLast` 為何使用偏右 mid（`left + (right - left + 1) / 2`）」。

### 4.5 旋轉排序陣列搜尋（RotatedArraySearch）
- `Search(int[] nums, int target)`：陣列由有序陣列旋轉而成，**元素皆不重複**，找不到回傳 `-1`。
- `SearchWithDuplicates(int[] nums, int target)`：允許重複值（LeetCode 81），回傳 `bool`。
- 註解需說明「如何判斷哪一半是有序的」與「重複值情況下的線性退化」。

### 4.6 峰值元素（FindPeakElement）
- `FindPeak(int[] nums)`：回傳任一峰值索引（`nums[i] > nums[i-1] && nums[i] > nums[i+1]`，邊界視為 `-∞`）。
- 假設相鄰元素不相等（LeetCode 162）。
- 註解需說明「為何在無序陣列也能用二分」（單調性保證）。

### 4.7 答案二分（Binary Search on Answer）
- **KokoEatingBananas**（LeetCode 875）：`MinEatingSpeed(int[] piles, int h)`，求最小吃香蕉速度。
- **SplitArrayLargestSum**（LeetCode 410）：`SplitArray(int[] nums, int k)`，將陣列分成 `k` 個非空子陣列，使「各子陣列和的最大值」最小。
- 兩者皆需在註解中標示：
  - 搜尋空間（`lo`、`hi` 怎麼決定）。
  - 判定函式（`Func<long, bool> canFinish`）的單調性說明。

### 4.8 實數 / 浮點數二分搜尋（FloatingPointBinarySearch）
- `Sqrt(double x, double epsilon = 1e-9)`：以二分法求平方根。
- `FindRoot(Func<double, double> f, double lo, double hi, double epsilon = 1e-9)`：在區間內找單調函數零點。
- 註解需說明：
  - 浮點數無「整數步進」概念，**用 `epsilon` 或固定迭代次數**作為終止條件。
  - 為何**不可**使用 `while (lo < hi)`（會無限迴圈）。

---

## 5. 程式碼風格規範

遵循 `.github/instructions/csharp.instructions.md` 與 `.editorconfig`，重點：

- 使用 C# 14 語法特性（file-scoped namespace、pattern matching、switch expression、collection expressions 等）。
- **所有公開 API** 皆須 XML doc，並在適當處附 `<example><code>...</code></example>`。
- 變數命名：`PascalCase`（公開成員）、`camelCase`（區域變數、private 欄位）。
- null 檢查使用 `is null` / `is not null`。
- 使用 `nameof` 取代字串字面量。
- 註解語言：**繁體中文**；技術名詞可保留英文（如 lower bound、overflow）。
- 每個演算法檔案頂部需有「概念簡介 / 時間複雜度 / 空間複雜度 / 適用情境」區塊註解。

---

## 6. Demo（Console）行為規範

`Program.cs` 提供互動式選單：

```
=== Binary Search 教學 Demo ===
1) 經典二分搜尋（閉區間 vs 半開區間對照）
2) Lower Bound
3) Upper Bound
4) First / Last Occurrence
5) 旋轉排序陣列搜尋
6) 峰值元素
7) 答案二分：Koko 吃香蕉
8) 答案二分：分割陣列最大和
9) 浮點數二分：求平方根
0) 離開
請選擇：
```

- 每個 Demo 內：
  - 顯示輸入資料與目標。
  - 顯示二分過程中的 `(left, right, mid)` 變化（教學用）。
  - 顯示最終結果並對照暴力解（驗證正確性）。
- 使用相依注入或工廠模式管理 Demo 註冊（不強制，但若採用可作為示範）。

---

## 7. 測試規範（xUnit）

每個變體至少需涵蓋以下測試類別：

| 類別 | 範例 |
|------|------|
| **正常情境** | 一般長度陣列、target 存在 / 不存在 |
| **邊界陣列** | 空陣列、單一元素、兩個元素 |
| **邊界 target** | target 為最小值、最大值、不存在但介於兩值之間 |
| **重複值** | 全相同元素、target 出現多次 |
| **大數溢位** | `left + right` 接近 `int.MaxValue` 的情境（驗證不 overflow） |
| **無效輸入** | `null` 陣列應擲 `ArgumentNullException` |
| **答案二分專屬** | `k = 1`、`k = nums.Length`、極大 / 極小搜尋空間 |
| **浮點數專屬** | epsilon 收斂、負輸入應擲例外 |

測試風格：
- 使用 `[Theory]` + `[InlineData]` 參數化測試。
- 命名範例：`Search_TargetExists_ReturnsCorrectIndex`、`LowerBound_AllElementsLessThanTarget_ReturnsLength`。
- **不要**寫 `// Arrange` / `// Act` / `// Assert` 註解。

驗收門檻：所有測試 100% 通過，且關鍵分支皆有測試覆蓋。

---

## 8. README.md 規範

新增 `README.md`（位於專案根目錄），必須包含以下章節，**全部繁體中文**：

1. **專案簡介**
2. **二分搜尋核心概念**
   - 適用前提（已排序 / 單調性）
   - 時間複雜度 O(log n) 推導
3. **區間寫法對照**（閉區間 vs 半開區間表格 + 程式碼片段）
4. **mid 取值與 Overflow 防範**（含錯誤示範與正確示範）
5. **終止條件解析**
   - `left <= right` 結束時 `left == right + 1` 的意義
   - `left < right` 結束時 `left == right` 的意義
6. **變體導覽**：對應第 4 章每個變體的概念說明與適用情境
7. **常見陷阱與除錯技巧**
   - 死迴圈
   - off-by-one
   - 邊界更新方向錯誤
8. **如何執行**：`dotnet build` / `dotnet run --project BinarySearch` / `dotnet test`
9. **LeetCode 推薦題目列表**（**20+ 題，依難度分組**）

### 8.1 LeetCode 推薦題單（規格定稿，實作時依此產出表格）

每題提供：題號、題目（中文）、難度、對應變體、官方連結。

**Easy（5 題以上）**
- 704. Binary Search（二分查詢）
- 35. Search Insert Position（搜尋插入位置）
- 278. First Bad Version（第一個錯誤的版本）
- 374. Guess Number Higher or Lower（猜數字大小）
- 69. Sqrt(x)（x 的平方根）
- 367. Valid Perfect Square（有效的完全平方數）

**Medium（10 題以上）**
- 34. Find First and Last Position of Element in Sorted Array（在排序陣列中查找元素的第一個和最後一個位置）
- 33. Search in Rotated Sorted Array（搜尋旋轉排序陣列）
- 81. Search in Rotated Sorted Array II（搜尋旋轉排序陣列 II）
- 153. Find Minimum in Rotated Sorted Array（尋找旋轉排序陣列中的最小值）
- 162. Find Peak Element（尋找峰值）
- 74. Search a 2D Matrix（搜尋二維矩陣）
- 240. Search a 2D Matrix II（搜尋二維矩陣 II）
- 875. Koko Eating Bananas（愛吃香蕉的 Koko）
- 1011. Capacity To Ship Packages Within D Days（在 D 天內送達包裹的能力）
- 540. Single Element in a Sorted Array（有序陣列中的單一元素）
- 658. Find K Closest Elements（找到 K 個最接近的元素）
- 287. Find the Duplicate Number（尋找重複數）

**Hard（5 題以上）**
- 4. Median of Two Sorted Arrays（兩個排序陣列的中位數）
- 410. Split Array Largest Sum（分割陣列的最大值）
- 668. Kth Smallest Number in Multiplication Table（乘法表中第 K 小的數）
- 719. Find K-th Smallest Pair Distance（找出第 K 小的數對距離）
- 878. Nth Magical Number（第 N 個神奇數字）

> 若 Easy + Medium + Hard 加總未達 20 題，實作時可從以下答案二分經典題補齊：LC 786（第 K 個最小的質數分數）、LC 1283（使結果不超過閾值的最小除數）、LC 1482（製作 m 束花所需的最少天數）、LC 2226（每個小孩可分配的最大糖果數）、LC 2616（最小化數對的最大差值）。

> 註：實作 README 時 LeetCode 題單以**繁中題目名稱為主、附原文與題號連結**，並標註對應第 4 章的哪一個變體。

---

## 9. 驗收標準（Definition of Done）

1. ✅ 解決方案可成功 `dotnet build`（無 warning，啟用 `TreatWarningsAsErrors` 為加分項）。
2. ✅ `dotnet test` 全部通過。
3. ✅ `dotnet run --project BinarySearch` 可進入互動選單並執行所有變體。
4. ✅ 每個 `.cs` 檔皆有完整中文註解，公開 API 有 XML doc。
5. ✅ README.md 涵蓋第 8 章所有章節，LeetCode 題單 ≥ 20 題且依難度分組。
6. ✅ 經典版二分搜尋同時提供閉區間 + 半開區間兩種實作，並在註解對照差異。
7. ✅ 至少一個變體（建議 `FindLast` 或 `FindPeak`）的註解明確說明「為何使用偏右 mid」。
8. ✅ 所有 mid 計算皆使用 `left + (right - left) / 2` 形式，並有註解說明 overflow 防範。

---

## 10. 開發流程建議（實作階段使用）

1. 建立解決方案結構（三個 csproj、slnx 引用）。
2. 實作 `BinarySearch.Core` 各變體 + xUnit 測試（TDD）。
3. 補上 Console Demo 與互動選單。
4. 撰寫 README.md。
5. 跑 `dotnet build` + `dotnet test` 全綠後交付。

---

## 11. 範圍外（Out of Scope）

- 平行 / 並行二分搜尋。
- 持久化資料庫的 B-Tree / B+Tree 索引（屬資料結構範疇）。
- 在無限/未知長度陣列搜尋（LC 702，本次未列入；可作為未來延伸）。
- 二維矩陣的進階變體（LC 74、240 僅在 LeetCode 題單中列出，不在 Core 函式庫實作）。

---

> **備註**：本規格書經審核後即進入實作階段。若實作過程中發現本規格與實際需求不符，請先更新此規格書再修改程式碼，確保文件與程式碼一致。
