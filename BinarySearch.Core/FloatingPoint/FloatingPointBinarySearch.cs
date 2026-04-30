// 概念簡介：實數（浮點數）二分搜尋 — 用於「連續搜尋空間」上的單調函數求根、求值問題。
// 為何不可使用 while (lo < hi)：
//   浮點數無「整數步進」，lo 與 hi 通常不會精確相等；
//   隨著 mid 不斷縮小區間，lo / hi 的差會逼近 0 但永遠不為 0（且受浮點精度限制），
//   因此會無限迴圈。
// 終止條件：以「絕對誤差 |hi - lo| < epsilon」或「固定迭代次數」（建議 100 ~ 200 次）取代。
// 時間複雜度：固定迭代 O(log((hi - lo) / eps))；以固定次數實作則為 O(1)（次數）。
// 空間複雜度：O(1)

namespace BinarySearch.Core.FloatingPoint;

/// <summary>
/// 浮點數二分：示範平方根計算與一般單調函數求根。
/// </summary>
public static class FloatingPointBinarySearch
{
    // 固定最大迭代次數作為保險：每輪區間長度減半，100 輪足以將 [0, 1e18] 收斂到 << 1e-9
    private const int MaxIterations = 200;

    /// <summary>
    /// 以二分法計算 <paramref name="x"/> 的非負平方根。
    /// </summary>
    /// <param name="x">非負實數。</param>
    /// <param name="epsilon">收斂容忍度，必須 &gt; 0。</param>
    /// <returns>近似平方根，誤差在 <paramref name="epsilon"/> 內。</returns>
    /// <exception cref="ArgumentException">當 <paramref name="x"/> 為負或 <paramref name="epsilon"/> &lt;= 0。</exception>
    public static double Sqrt(double x, double epsilon = 1e-9)
    {
        if (x < 0)
        {
            throw new ArgumentException("x 必須為非負數。", nameof(x));
        }
        if (epsilon <= 0)
        {
            throw new ArgumentException("epsilon 必須為正數。", nameof(epsilon));
        }

        if (x == 0d || x == 1d)
        {
            return x;
        }

        // x 介於 (0, 1) 時，平方根 > x 自身，hi 必須取 1；x >= 1 時 hi = x 即可
        double lo = 0d;
        double hi = x < 1d ? 1d : x;

        // 採「epsilon 收斂 + 最大迭代次數」雙保險，避免浮點精度造成的死迴圈
        for (int i = 0; i < MaxIterations && hi - lo > epsilon; i++)
        {
            double mid = lo + ((hi - lo) / 2.0);
            if (mid * mid > x)
            {
                hi = mid;
            }
            else
            {
                lo = mid;
            }
        }

        return lo + ((hi - lo) / 2.0);
    }

    /// <summary>
    /// 在 <c>[lo, hi]</c> 區間內以二分法尋找單調函數 <paramref name="f"/> 的零點（<c>f(root) ≈ 0</c>）。
    /// </summary>
    /// <remarks>
    /// 前提：<paramref name="f"/> 在 <c>[lo, hi]</c> 上連續且單調，且端點函數值「異號或其中一端為 0」。
    /// 演算法自動偵測函數方向（遞增 / 遞減），不論哪種皆可正確收斂。
    /// </remarks>
    /// <param name="f">單調連續函數。</param>
    /// <param name="lo">區間左端。</param>
    /// <param name="hi">區間右端，必須 &gt; <paramref name="lo"/>。</param>
    /// <param name="epsilon">收斂容忍度，必須 &gt; 0。</param>
    /// <returns>近似零點。</returns>
    /// <exception cref="ArgumentNullException">當 <paramref name="f"/> 為 <see langword="null"/>。</exception>
    /// <exception cref="ArgumentException">當參數不合法或端點同號。</exception>
    public static double FindRoot(Func<double, double> f, double lo, double hi, double epsilon = 1e-9)
    {
        ArgumentNullException.ThrowIfNull(f);

        if (hi <= lo)
        {
            throw new ArgumentException("hi 必須大於 lo。", nameof(hi));
        }
        if (epsilon <= 0)
        {
            throw new ArgumentException("epsilon 必須為正數。", nameof(epsilon));
        }

        double fLo = f(lo);
        double fHi = f(hi);

        if (fLo == 0d)
        {
            return lo;
        }
        if (fHi == 0d)
        {
            return hi;
        }
        if (fLo * fHi > 0)
        {
            throw new ArgumentException("f(lo) 與 f(hi) 必須異號才能保證區間內存在零點。", nameof(f));
        }

        // 標準化方向：恆假設「左端 < 0 < 右端」，否則交換 lo / hi 的角色由 increasing 旗標處理
        bool increasing = fLo < fHi; // 遞增則右大左小

        for (int i = 0; i < MaxIterations && hi - lo > epsilon; i++)
        {
            double mid = lo + ((hi - lo) / 2.0);
            double fMid = f(mid);

            // 若 f 為遞增：fMid > 0 表示根在左側 ⇒ hi = mid；反之 lo = mid
            // 若 f 為遞減：方向相反
            if ((increasing && fMid > 0) || (!increasing && fMid < 0))
            {
                hi = mid;
            }
            else
            {
                lo = mid;
            }
        }

        return lo + ((hi - lo) / 2.0);
    }
}
