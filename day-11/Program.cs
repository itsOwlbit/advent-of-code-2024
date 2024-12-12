string filepath = "day-11/aoc-day11-test.txt";

StonesLine stoneLine = new StonesLine(filepath);

stoneLine.DisplayStones();
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

    public void DisplayStones()
    {
        Console.WriteLine($"Stone line: {string.Join(", ", _stones)}");
    }
}