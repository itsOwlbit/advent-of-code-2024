using System.Text.RegularExpressions;

// string filepath = "day-13/aoc-day13-test.txt";
string filepath = "day-13/aoc-day13-data.txt";

TheClaw theClaw = new TheClaw(filepath);

// theClaw.PrintMachineData();
// Console.WriteLine();

theClaw.PrintMinimalTokenCosts();
Console.WriteLine();

class TheClaw
{
    private List<(int X, int Y)> _buttonAList;
    private List<(int X, int Y)> _buttonBList;
    private List<(int X, int Y)> _prizeList;

    public TheClaw(string filepath)
    {
        _buttonAList = new List<(int X, int Y)>();
        _buttonBList = new List<(int X, int Y)>();
        _prizeList = new List<(int X, int Y)>();

        GetMachineDataFromFile(filepath);
    }

    private void GetMachineDataFromFile(string filepath)
    {
        using (StreamReader sr = new StreamReader(filepath))
        {
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                line = line.Trim(); // Remove leading and trailing whitespace

                if (string.IsNullOrEmpty(line))
                {
                    // Skip blank lines
                    continue;
                }

                if (line.StartsWith("Button A:"))
                {
                    _buttonAList.Add(ParseCoordinates(line));
                }
                else if (line.StartsWith("Button B:"))
                {
                    _buttonBList.Add(ParseCoordinates(line));
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

    private int CalculateDistance((int X, int Y) point1, (int X, int Y) point2)
    {
        return Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y);
    }

    public int CalculateMinimalTokenCostForMachine(int machineIndex)
    {
        if (machineIndex < 0 || machineIndex >= _prizeList.Count)
        {
            throw new ArgumentOutOfRangeException("Invalid machine index.");
        }

        var startPoint = (X: 0, Y: 0);
        var buttonA = _buttonAList[machineIndex];
        var buttonB = _buttonBList[machineIndex];
        var prize = _prizeList[machineIndex];

        int minCost = int.MaxValue;
        int bestAClicks = 0, bestBClicks = 0;

        // Try a range of A clicks (assume a reasonable limit to avoid infinite loops)
        for (int aClicks = 0; aClicks <= 1000; aClicks++)
        {
            // Calculate position after A clicks
            int currentX = startPoint.X + (aClicks * buttonA.X);
            int currentY = startPoint.Y + (aClicks * buttonA.Y);

            // Check if current position already matches the prize
            if (currentX == prize.X && currentY == prize.Y)
            {
                int cost = (aClicks * 4); // Only Button A clicks are used
                if (cost < minCost)
                {
                    minCost = cost;
                    bestAClicks = aClicks;
                    bestBClicks = 0;
                }
                continue;
            }

            // Try B clicks from the current position
            for (int bClicks = 0; bClicks <= 1000; bClicks++)
            {
                int nextX = currentX + (bClicks * buttonB.X);
                int nextY = currentY + (bClicks * buttonB.Y);

                if (nextX == prize.X && nextY == prize.Y)
                {
                    int cost = (aClicks * 3) + (bClicks * 1);
                    if (cost < minCost)
                    {
                        minCost = cost;
                        bestAClicks = aClicks;
                        bestBClicks = bClicks;
                    }
                    break; // Found a solution with this combination of A clicks
                }
            }
        }

        if (minCost == int.MaxValue)
        {
            Console.WriteLine($"Machine {machineIndex + 1} Debug: Unreachable prize");
            return 0;
        }

        Console.WriteLine($"Machine {machineIndex + 1} Debug:");
        Console.WriteLine($"  Button A Clicks: {bestAClicks}");
        Console.WriteLine($"  Button B Clicks: {bestBClicks}");
        return minCost;
    }

    public void PrintMinimalTokenCosts()
    {
        int totalTokens = 0;

        for (int i = 0; i < _prizeList.Count; i++)
        {
            int cost = CalculateMinimalTokenCostForMachine(i);
            Console.WriteLine($"Machine {i + 1}: Minimal Token Cost = {cost}");
            Console.WriteLine();
            totalTokens += cost;
        }

        Console.WriteLine($"Total Tokens Used: {totalTokens}");
    }

    public void PrintMachineData()
    {
        for (int i = 0; i < _prizeList.Count; i++)
        {
            Console.WriteLine($"Machine {i + 1}:");
            Console.WriteLine($"  Button A: {_buttonAList[i]}");
            Console.WriteLine($"  Button B: {_buttonBList[i]}");
            Console.WriteLine($"  Prize: {_prizeList[i]}");
        }
    }
}
