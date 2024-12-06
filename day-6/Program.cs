string filePath = "day-6/aoc-day6-test.txt";

LabMap myMap = new LabMap(filePath);

myMap.PrintMap();
myMap.PrintGuardPosition();
myMap.PrintObstaclePositions();
Console.WriteLine();
myMap.MoveGuard();

class LabMap
{
    private List<char> _map;
    private int _rows = 0;
    private int _cols = 0;
    private List<int> _guardsPositions;
    private List<int> _obstaclePositions;
    private static readonly char[] positionalChars = { '^', '>', 'v', '<' };

    public LabMap(string filePath)
    {
        _map = new List<char>();

        GetDataFromFile(filePath);

        _guardsPositions = _map
            .Select((c, index) => new { Character = c, Index = index })
            .Where(x => positionalChars.Contains(x.Character))
            .Select(x => x.Index)
            .ToList(); ;

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
        Console.WriteLine($"\nGuard Location(s): {string.Join(", ", _guardsPositions)}");
    }

    public void PrintObstaclePositions()
    {
        Console.WriteLine($"\nObstacles are at: {string.Join(", ", _obstaclePositions)}");
    }

    public void MoveGuard()
    {
        foreach (var position in _guardsPositions)
        {
            char direction = _map[position];

            switch (direction)
            {
                case var _ when direction == positionalChars[0]:
                    GuardFacingUp();
                    break;
                case var _ when direction == positionalChars[1]:
                    GuardFacingRight();
                    break;
                case var _ when direction == positionalChars[2]:
                    GuardFacingDown();
                    break;
                case var _ when direction == positionalChars[3]:
                    GuardFacingLeft();
                    break;
                default:
                    Console.WriteLine($"Invalid direction at: {position}: {direction}");
                    break;
            }
        }
    }

    private void GuardFacingUp()
    {
        Console.WriteLine("Guard is facing up.");
    }
    private void GuardFacingRight()
    {
        Console.WriteLine("Guard is facing right.");
    }
    private void GuardFacingDown()
    {
        Console.WriteLine("Guard is facing down.");
    }
    private void GuardFacingLeft()
    {
        Console.WriteLine("Guard is facing left.");
    }
}