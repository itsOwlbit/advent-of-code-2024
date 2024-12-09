string filePath = "day-8/aoc-day8-test.txt";
// string filePath = "day-8/aoc-day8-data.txt";

AntennaMap antennaMap = new AntennaMap(filePath);
antennaMap.PrintMap();
Console.WriteLine();



class AntennaMap
{
    private List<char> _map;
    private int _rows = 0;
    private int _cols = 0;

    public AntennaMap(string filePath)
    {
        _map = new List<char>();

        GetDataFromFile(filePath);
    }

    private void GetDataFromFile(string filePath)
    {
        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                if (_cols == 0)
                {
                    _cols = line.Length;
                }

                foreach (char c in line)
                {
                    _map.Add(c);
                }

                _rows++;
            }
        }
    }

    public void PrintMap()
    {
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _cols; j++)
            {
                Console.Write(_map[(_rows * i) + j]);
            }
            Console.WriteLine();
        }
    }
}