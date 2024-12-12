string filepath = "day-11/aoc-day11-test.txt";

StonesLine stoneLine = new StonesLine(filepath);

stoneLine.DisplayStones();
Console.WriteLine();

stoneLine.Blink(25);
Console.WriteLine();

int numberOfStones = stoneLine.GetNumberOfStones();
Console.WriteLine($"Number of Stones: {numberOfStones}");
Console.WriteLine();

class StonesLine
{
    List<int> _stones;

    public StonesLine(string filepath)
    {
        _stones = new List<int>();

        this.GetStonesFromFile(filepath);
    }

    private void GetStonesFromFile(string filepath)
    {
        using (StreamReader sr = new StreamReader(filepath))
        {
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                var parts = line.Split(" ");

                foreach (var part in parts)
                {
                    if (int.TryParse(part, out int number))
                    {
                        _stones.Add(number);
                    }
                }
            }
        }
    }

    public void Blink(int numberOfBlinks)
    {
        for (int i = 1; i <= numberOfBlinks; i++)
        {
            for (int j = 0; j < _stones.Count; j++)
            {
                // int length = _stones[i].ToString().Length;
                // Console.WriteLine($"length: {length}");

                if (_stones[j] == 0)
                {
                    _stones[j] = 1;
                }
                else if (_stones[j].ToString().Length % 2 == 0)
                {
                    int numberOfDigits = _stones[j].ToString().Length;
                    int halfValue = numberOfDigits / 2;
                    int leftStone = _stones[j] / (int)Math.Round(Math.Pow(10, halfValue));
                    int rightStone = _stones[j] - (leftStone * (int)Math.Round(Math.Pow(10, halfValue)));

                    // Console.WriteLine($"left stone: {leftStone}");
                    // Console.WriteLine($"right stone: {rightStone}");

                    _stones[j] = leftStone;
                    _stones.Insert(j + 1, rightStone);
                    j++;
                }
                else
                {
                    _stones[j] *= 2024;
                }
            }

            Console.WriteLine($"After {i} blink(s):");
            DisplayStones();
        }
    }

    public int GetNumberOfStones()
    {
        return _stones.Count;
    }

    public void DisplayStones()
    {
        Console.WriteLine($"Stone line: {string.Join(", ", _stones)}");
    }
}