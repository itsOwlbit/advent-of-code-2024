using System.Text.RegularExpressions;

string filepath = "day-13/aoc-day13-test.txt";

TheClaw theClaw = new TheClaw(filepath);

theClaw.PrintMachineData();
Console.WriteLine();

class TheClaw
{
    private List<List<(int X, int Y)>> _buttonAList;
    private List<List<(int X, int Y)>> _buttonBList;
    private List<(int X, int Y)> _prizeList;

    public TheClaw(string filepath)
    {
        _buttonAList = new List<List<(int X, int Y)>>();
        _buttonBList = new List<List<(int X, int Y)>>();
        _prizeList = new List<(int X, int Y)>();

        GetMachineDataFromFile(filepath);
    }

    private void GetMachineDataFromFile(string filepath)
    {
        List<(int X, int Y)> currentButtonA = null;
        List<(int X, int Y)> currentButtonB = null;

        using (StreamReader sr = new StreamReader(filepath))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                line = line.Trim();

                if (string.IsNullOrEmpty(line))
                {
                    currentButtonA = null;
                    currentButtonB = null;
                    continue;
                }

                if (line.StartsWith("Button A:"))
                {
                    if (currentButtonA == null)
                    {
                        currentButtonA = new List<(int X, int Y)>();
                        _buttonAList.Add(currentButtonA);
                    }

                    currentButtonA.Add(ParseCoordinates(line));
                }
                else if (line.StartsWith("Button B:"))
                {
                    if (currentButtonB == null)
                    {
                        currentButtonB = new List<(int X, int Y)>();
                        _buttonBList.Add(currentButtonB);
                    }

                    currentButtonB.Add(ParseCoordinates(line));
                }
                else if (line.StartsWith("Prize:"))
                {
                    _prizeList.Add(ParseCoordinates(line));
                }
            }
        }
    }

    private (int X, int Y) ParseCoordinates(string input)
    {
        // Regular expression to extract X and Y values
        var match = Regex.Match(input, @"X[+=](\d+), Y[+=](\d+)");
        if (match.Success)
        {
            int x = int.Parse(match.Groups[1].Value);
            int y = int.Parse(match.Groups[2].Value);
            return (x, y);
        }
        throw new FormatException("Invalid coordinate format in line: " + input);
    }

    public void PrintMachineData()
    {
        for (int i = 0; i < _prizeList.Count; i++)
        {
            Console.WriteLine($"Machine {i + 1}:");
            Console.WriteLine($"  Button A: {string.Join(", ", _buttonAList[i])}");
            Console.WriteLine($"  Button B: {string.Join(", ", _buttonBList[i])}");
            Console.WriteLine($"  Prize: {_prizeList[i]}");
        }
    }
}
