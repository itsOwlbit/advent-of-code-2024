string filepath = "day-12/aoc-day12-test.txt";

Plots garden = new Plots(filepath);

garden.printGarden();
Console.WriteLine();

class Plots
{
    List<List<char>> _plots;
    int _rows = 0;
    int _cols = 0;
    int _totalPrice = 0;

    public Plots(string filepath)
    {
        _plots = new List<List<char>>();

        GetDataFromFile(filepath);
    }

    private void GetDataFromFile(string filepath)
    {
        using (StreamReader sr = new StreamReader(filepath))
        {
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                var row = new List<char>(line);

                _plots.Add(row);

                if (_cols == 0)
                {
                    _cols = line.Length;
                }

                _rows++;
            }
        }
    }

    public void printGarden()
    {
        foreach (var plot in _plots)
        {
            Console.WriteLine($"{string.Join(" ", plot)}");
        }
    }
}