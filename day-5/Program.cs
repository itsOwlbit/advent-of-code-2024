string filePath = "day-5/aoc-day5-test.txt";

Printer safetyProtocolPrintJob = new Printer(filePath);
safetyProtocolPrintJob.PrintData();

class Printer
{
    private readonly List<(int pageNumber, int precedingPageNumber)> _pageOrderRules;
    private readonly List<List<int>> _printQueue;

    public Printer(string filePath)
    {
        _pageOrderRules = new List<(int, int)>();
        _printQueue = new List<List<int>>();

        GetRulesAndQueue(filePath);
    }

    private void GetRulesAndQueue(string filePath)
    {
        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("|"))
                {
                    var parts = line.Split('|');

                    if (parts.Length == 2 &&
                        int.TryParse(parts[0], out int pageNumber) &&
                        int.TryParse(parts[1], out int precedingPageNumber))
                    {
                        _pageOrderRules.Add((pageNumber, precedingPageNumber));
                        // Console.WriteLine($"Rule Added: ({pageNumber}, {precedingPageNumber})");
                    }
                }
                else if (line.Contains(","))
                {
                    var pages = new List<int>();
                    var parts = line.Split(',');

                    foreach (var part in parts)
                    {
                        if (int.TryParse(part, out int page))
                        {
                            pages.Add(page);
                        }
                    }

                    _printQueue.Add(pages);
                    // Console.WriteLine($"Queue added: [{string.Join(", ", pages)}]");
                }
            }
        }
    }

    public void PrintData()
    {
        Console.WriteLine("\nPage Order Rules:");
        foreach (var rule in _pageOrderRules)
        {
            Console.WriteLine($"{rule.pageNumber} | {rule.precedingPageNumber}");
        }

        Console.WriteLine("\nPrint Queue:");
        foreach (var queue in _printQueue)
        {
            Console.WriteLine($"{string.Join(", ", queue)}");
        }
    }
}