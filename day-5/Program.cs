// string filePath = "day-5/aoc-day5-test.txt";
string filePath = "day-5/aoc-day5-data.txt";


Printer safetyProtocolPrintQueue = new Printer(filePath);
// safetyProtocolPrintQueue.PrintData();
safetyProtocolPrintQueue.PrintAllJobs();

class Printer
{
    private readonly List<(int pageNumber, int precedesPageNumber)> _pageOrderRules;
    private readonly List<List<int>> _printJobs;
    private List<List<int>> _printQueue;
    private List<List<int>> _printQueueErrors;
    private int _sumOfMiddlePageValue = 0;
    private int _sumOfMiddlePageValueFromErrors = 0;

    public Printer(string filePath)
    {
        _pageOrderRules = new List<(int, int)>();
        _printJobs = new List<List<int>>();
        _printQueue = new List<List<int>>();
        _printQueueErrors = new List<List<int>>();


        GetRulesAndPrintJobs(filePath);
    }

    private void GetRulesAndPrintJobs(string filePath)
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
                        // Console.WriteLine($"Rule Added: ({pageNumber}, {precedesPageNumber})");
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

                    _printJobs.Add(pages);
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
        foreach (var queue in _printJobs)
        {
            Console.WriteLine($"{string.Join(", ", queue)}");
        }
    }

    public void PrintAllJobs()
    {
        this.ValidatePrintJobs();
        int sum = 0;

        // Console.WriteLine("\nCorrectly ordered printing:");
        foreach (List<int> queue in _printQueue)
        {
            // Console.WriteLine($"{string.Join(", ", queue)}");
            sum = GetSumOfMiddleValues(_printQueue);
        }

        Console.WriteLine($"\nSum of middle page numbers (from correctly-ordered queues): {sum}");

        // Console.WriteLine("\nFixed ordered printing:");
        foreach (List<int> queue in _printQueueErrors)
        {
            // Console.WriteLine($"{string.Join(", ", queue)}");
            sum = GetSumOfMiddleValues(_printQueueErrors);
        }

        Console.WriteLine($"\nSum of middle page numbers (from fixed-ordered queues): {sum}\n");
    }

    private void ValidatePrintJobs()
    {
        // loop through the print jobs (the queue)
        foreach (List<int> queue in _printJobs)
        {
            List<int> pages = new List<int>();
            bool hasViolatesRule = false;

            // loop through the pages for a print job
            foreach (var page in queue)
            {
                pages.Add(page);

                if (!isQueueInCorrectRuleOrder(pages, page))
                {
                    // Console.WriteLine($"Page order rule error: {page}");
                    hasViolatesRule = true;
                }
            }

            if (!hasViolatesRule)
            {
                _printQueue.Add(pages);
            }
            else
            {
                _printQueueErrors.Add(pages);
            }
        }
    }

    public bool isQueueInCorrectRuleOrder(List<int> currentQueue, int pageNumber)
    {
        bool isModified = false;


        while (true)
        {
            bool isSwapped = false;

            foreach (var (page, precedes) in _pageOrderRules)
            {
                if (pageNumber == page && currentQueue.Contains(precedes))
                {
                    int pageIndex = currentQueue.IndexOf(pageNumber);
                    int precedesIndex = currentQueue.IndexOf(precedes);

                    if (pageIndex > precedesIndex)
                    {
                        // Remove and insert the page at the correct position
                        currentQueue.RemoveAt(pageIndex);
                        currentQueue.Insert(precedesIndex, pageNumber);

                        isSwapped = true;
                        isModified = true;
                    }
                }
            }

            // Exit the loop if no swaps were made during this iteration
            if (!isSwapped)
            {
                break;
            }
        }

        return !isModified;
    }

    private int GetSumOfMiddleValues(List<List<int>> queryList)
    {
        int sum = 0;

        foreach (var item in queryList)
        {
            sum += GetMiddleValue(item);
        }

        return sum;
    }

    private int GetMiddleValue(List<int> numbers)
    {
        if (numbers == null || numbers.Count == 0)
        {
            throw new ArgumentException("List cannot be null or empty.");
        }

        int middleIndex = numbers.Count / 2;

        if (numbers.Count % 2 != 0)
        {
            return numbers[middleIndex];
        }

        return numbers[middleIndex - 1];
    }
}