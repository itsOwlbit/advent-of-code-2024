using System.Numerics;
// string filepath = "day-11/aoc-day11-test.txt";
string filepath = "day-11/aoc-day11-data.txt";

StonesLine stoneLine = new StonesLine(filepath);

stoneLine.DisplayStones();
Console.WriteLine();

stoneLine.Blink(25);
Console.WriteLine();

BigInteger numberOfStones = stoneLine.GetNumberOfStones();
Console.WriteLine($"Number of Stones: {numberOfStones}");
Console.WriteLine();

class StonesLine
{
    List<BigInteger> _stones;

    public StonesLine(string filepath)
    {
        _stones = new List<BigInteger>();

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
                    if (BigInteger.TryParse(part, out BigInteger number))
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
            List<BigInteger> newStones = new List<BigInteger>();

            foreach (var stone in _stones)
            {
                if (stone == 0)
                {
                    newStones.Add(1);
                }
                else if (stone.ToString().Length % 2 == 0)
                {
                    int numberOfDigits = stone.ToString().Length;
                    int halfDigit = numberOfDigits / 2;
                    BigInteger divisor = BigInteger.Pow(10, halfDigit);
                    BigInteger leftStone = stone / divisor;
                    BigInteger rightStone = stone % divisor;

                    newStones.Add(leftStone);
                    newStones.Add(rightStone);
                }
                else
                {
                    newStones.Add(stone * 2024);
                }
            }

            _stones = newStones; // Replace old stones with the new list

            Console.WriteLine($"Blink {i}: {_stones.Count}");
            Console.WriteLine("");
        }
    }

    public BigInteger GetNumberOfStones()
    {
        return _stones.Count;
    }

    public void DisplayStones()
    {
        Console.WriteLine($"Stone line: {string.Join(", ", _stones)}");
    }
}