using BinarySearch.Demos;

namespace BinarySearch;

/// <summary>
/// 主程式：互動式選單，依序展示 9 個 Binary Search 變體。
/// </summary>
internal static class Program
{
    private static readonly (string Key, IDemo Demo)[] Menu =
    [
        ("1", new ClassicSearchDemo()),
        ("2", new LowerBoundDemo()),
        ("3", new UpperBoundDemo()),
        ("4", new FirstLastOccurrenceDemo()),
        ("5", new RotatedArrayDemo()),
        ("6", new PeakElementDemo()),
        ("7", new BinarySearchOnAnswerDemo()),
        ("8", new SplitArrayDemo()),
        ("9", new FloatingPointBinarySearchDemo()),
    ];

    private static void Main(string[] args)
    {
        // 支援以參數一鍵跑完所有 Demo（便於 CI 驗證）
        if (args.Length > 0 && args[0] is "--all" or "-a")
        {
            RunAll();
            return;
        }

        while (true)
        {
            PrintMenu();
            Console.Write("請選擇：");
            string? input = Console.ReadLine()?.Trim();

            if (input is null or "0")
            {
                Console.WriteLine("再見！");
                return;
            }

            (string Key, IDemo Demo)? entry = Menu.FirstOrDefault(m => m.Key == input);
            if (entry is null || entry.Value.Demo is null)
            {
                Console.WriteLine($"未知選項：{input}");
                continue;
            }

            Console.WriteLine();
            Console.WriteLine($"=== {entry.Value.Demo.Title} ===");
            try
            {
                entry.Value.Demo.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"執行錯誤：{ex.Message}");
            }
            Console.WriteLine();
            Console.WriteLine("(按 Enter 回到主選單)");
            Console.ReadLine();
        }
    }

    private static void PrintMenu()
    {
        Console.WriteLine("=== Binary Search 教學 Demo ===");
        foreach ((string key, IDemo demo) in Menu)
        {
            Console.WriteLine($"{key}) {demo.Title}");
        }
        Console.WriteLine("0) 離開");
    }

    private static void RunAll()
    {
        foreach ((_, IDemo demo) in Menu)
        {
            Console.WriteLine($"=== {demo.Title} ===");
            demo.Run();
            Console.WriteLine();
        }
    }
}
