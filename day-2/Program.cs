using Microsoft.Win32.SafeHandles;

DayTwoAoC dayTwoAoc = new DayTwoAoC();

// Test file: day-2/aoc-day2-test.txt
// Data file: day-2/aoc-day2-data.txt
dayTwoAoc.PopulateDataFromFile("day-2/aoc-day2-data.txt");

// Part One Solution
var safeReports = dayTwoAoc.CountSafeReports();
Console.WriteLine($"{safeReports} reports with default safety rule are safe.");

// Part Two Solution
safeReports = dayTwoAoc.CountSafeReportsWithSafety();
Console.WriteLine($"{safeReports} reports with safety rules are safe.");

class DayTwoAoC
{
    private List<List<int>> data;
    bool isSafe = true;

    public DayTwoAoC()
    {
        data = new List<List<int>>();
    }
    public void PopulateDataFromFile(string filePath)
    {
        try
        {
            foreach (string line in File.ReadLines(filePath))
            {
                string[] parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                List<int> report = new List<int>();

                if (parts.Length > 1)
                {
                    foreach (string number in parts)
                    {
                        if (int.TryParse(number, out int level))
                        {
                            report.Add(level);

                        }
                        else
                        {
                            Console.WriteLine($"Invalid Number: {number}");
                        }
                    }

                    // trouble shooting small test text files
                    // Console.WriteLine("Report Line: " + string.Join(". ", report));

                    data.Add(report);
                }
                else
                {
                    Console.WriteLine($"Invalid line from: {line}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    public int CountSafeReports()
    {
        int safeCounter = 0;

        for (int i = 0; i < data.Count; i++)
        {
            List<int> currentReport = data[i];

            bool isIncreasing = false;
            bool isDecreasing = false;
            isSafe = true;

            int difference;

            int currentValue = currentReport[0];
            int indexCounter = 1;

            do
            {
                // for testing values
                // Console.WriteLine($"Current: {currentValue}, Test against: {currentReport[indexCounter]}");

                if (currentValue < currentReport[indexCounter])
                {
                    if (isDecreasing)
                    {
                        isIncreasing = false;
                        // Console.WriteLine("Unsafe.");
                        isSafe = false;
                    }
                    isIncreasing = true;
                    // Console.WriteLine("Increasing.");
                }
                else if (currentValue > currentReport[indexCounter])
                {
                    if (isIncreasing)
                    {
                        isDecreasing = false;
                        // Console.WriteLine("Unsafe.");
                        isSafe = false;
                    }

                    isDecreasing = true;
                    // Console.WriteLine("Decreasing.");
                }
                else
                {
                    isSafe = false;
                }

                difference = Math.Abs(currentValue - currentReport[indexCounter]);

                if (difference > 3)
                {
                    isSafe = false;
                }

                currentValue = currentReport[indexCounter];
                indexCounter++;

            } while (isSafe && indexCounter < currentReport.Count);

            if ((isIncreasing || isDecreasing) && isSafe)
            {
                safeCounter++;
            }
        }

        return safeCounter;
    }

    public int CountSafeReportsWithSafety()
    {
        int safeCounter = 0;


        for (int i = 0; i < data.Count; i++)
        {
            List<int> currentReport = data[i];

            int increasingCounter = 0;
            int decreasingCounter = 0;
            int equalCounter = 0;
            isSafe = true;

            int difference;
            int safetyViolationCounter = 0;

            int currentValue = currentReport[0];
            int indexCounter = 1;

            do
            {
                // for testing values
                // Console.WriteLine($"Current: {currentValue}, Test against: {currentReport[indexCounter]}");

                if (currentValue < currentReport[indexCounter])
                {
                    increasingCounter++;
                }
                else if (currentValue > currentReport[indexCounter])
                {
                    decreasingCounter++;
                }
                else
                {
                    equalCounter++;
                    safetyViolationCounter++;
                }

                difference = Math.Abs(currentValue - currentReport[indexCounter]);

                if (difference > 3 || safetyViolationCounter > 1)
                {
                    isSafe = false;
                }

                currentValue = currentReport[indexCounter];
                indexCounter++;

            } while (isSafe && indexCounter < currentReport.Count);

            if ((increasingCounter > 0 && decreasingCounter > 1) || (increasingCounter > 1 && decreasingCounter > 0))
            {
                safetyViolationCounter++;
            }

            // for debugging purposes
            // Console.WriteLine($"Increase: {increasingCounter}, Decrease: {decreasingCounter}, Equal: {equalCounter}");

            if (isSafe && safetyViolationCounter <= 1)
            {
                safeCounter++;
            }
        }

        return safeCounter;
    }
}