namespace BinarySearch.Demos;

/// <summary>
/// 所有 Demo 的共同介面。<see cref="Program"/> 以選單方式呼叫。
/// </summary>
public interface IDemo
{
    /// <summary>顯示在主選單的標題。</summary>
    string Title { get; }

    /// <summary>執行 Demo（讀取輸入、印出搜尋過程與結果）。</summary>
    void Run();
}
