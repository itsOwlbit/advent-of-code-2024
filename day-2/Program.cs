// data has rows of reports, which are list of levels
List<List<int>> data = new List<List<int>>();

int safeCounter = 0;
bool isSafe = true;

// populate data list from a file
try
{
    // Test file: day-2/aoc-day2-test.txt
    // Data file: day-2/aoc-day2-data.txt
    foreach (string line in File.ReadLines("day-2/aoc-day2-data.txt"))
    {
        // help solve the problem of invisible characters in my text file
        string[] parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

        // storage for a report row, which has items called levels
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

// count number of safe reports
// safe reports have continuous increasing or decreasing values
// adjacent values must not equal and have a difference of no more than 3
// NOTE: assumes each row has at least 3 values
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

        // ensures difference between values is at most 3
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

Console.WriteLine($"{safeCounter} reports are safe.");