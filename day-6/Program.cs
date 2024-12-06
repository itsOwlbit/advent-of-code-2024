string filePath = "day-6/aoc-day6-test.txt";

LabMap myMap = new LabMap(filePath);

myMap.PrintMap();
myMap.PrintGuardPosition();
myMap.PrintObstaclePositions();
Console.WriteLine();

class LabMap
{
    private List<char> _map;
    private int _rows = 0;
    private int _cols = 0;
    private int _guardPosition;
    private List<int> _obstaclePositions;

    public LabMap(string filePath)
    {
        _map = new List<char>();

        GetDataFromFile(filePath);

        _guardPosition = _map.IndexOf('^');

        _obstaclePositions = _map
            .Select((c, index) => new { Character = c, Index = index })
            .Where(x => x.Character == '#')
            .Select(x => x.Index)
            .ToList();
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

    public void PrintGuardPosition()
    {
        Console.WriteLine($"\nGuard is at: {_guardPosition}");
    }

    public void PrintObstaclePositions()
    {
        Console.WriteLine($"\nObstacles are at: {string.Join(", ", _obstaclePositions)}");
    }
}