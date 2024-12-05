// string filePath = "day-5/aoc-day5-test.txt";
string filePath = "day-5/aoc-day5-data.txt";


Printer safetyProtocolPrintQueue = new Printer(filePath);
// safetyProtocolPrintQueue.PrintData();
safetyProtocolPrintQueue.PrintQueue();

class Printer
{
    private readonly List<(int pageNumber, int precedesPageNumber)> _pageOrderRules;
    private readonly List<List<int>> _printQueue;
    private int _sumOfMiddlePageValue = 0;

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
                        int.TryParse(parts[1], out int precedesPageNumber))
                    {
                        _pageOrderRules.Add((pageNumber, precedesPageNumber));
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
            Console.WriteLine($"{rule.pageNumber} | {rule.precedesPageNumber}");
        }

        Console.WriteLine("\nPrint Queue:");
        foreach (var queue in _printQueue)
        {
            Console.WriteLine($"{string.Join(", ", queue)}");
        }
    }

    public void PrintQueue()
    {
        foreach (List<int> queue in _printQueue)
        {
            List<int> pages = new List<int>();
            bool hasViolatesRule = false;

            foreach (var page in queue)
            {
                if (isQueueInCorrectRuleOrder(pages, page))
                {
                    pages.Add(page);
                }
                else
                {
                    // Console.WriteLine($"Page order rule error: {page}");
                    hasViolatesRule = true;
                }
            }

            if (!hasViolatesRule)
            {
                // calculate the sum of the middle page number for each queue line
                _sumOfMiddlePageValue += MiddlePageValue(pages);

                PrintPages(pages);
            }
        }

        Console.WriteLine($"\nSum of middle page numbers (from correctly-ordered queues): {_sumOfMiddlePageValue}");
    }

    public bool isQueueInCorrectRuleOrder(List<int> currentQueue, int pageNumber)
    {
        foreach (var (page, precedes) in _pageOrderRules)
        {
            if (pageNumber == page && currentQueue.Contains(precedes))
            {
                return false;
            }
        }

        return true;
    }

    private void PrintPages(List<int> pages)
    {
        Console.WriteLine($"{string.Join(", ", pages)}");
    }

    private int MiddlePageValue(List<int> pages)
    {
        if (pages.Count % 2 != 0)
        {
            return pages[pages.Count / 2];
        }

        return 0;
    }
}