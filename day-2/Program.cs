// data has rows of reports, which are list of levels
List<List<int>> data = new List<List<int>>();

// populate data from a file
try
{
    foreach (string line in File.ReadLines("day-2/aoc-day2-test.txt"))
    {
        // help solve the problem of invisible characters in my text file
        string[] parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

        // storage for a report row, which has items called levels
        List<int> report = new List<int>();

        if (parts.Length == 5)
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
            Console.WriteLine("Report Line: " + string.Join(". ", report));

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


