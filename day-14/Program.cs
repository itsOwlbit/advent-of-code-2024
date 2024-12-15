using System.Text.RegularExpressions;

string filepath = "day-14/aoc-day14-test.txt";
int row = 103;
int col = 101;

RobotGrid robotGrid = new RobotGrid(filepath, row, col);
robotGrid.PrintRobotData();
Console.WriteLine();

robotGrid.DisplayRobotLocationGraph();
Console.WriteLine();

int secondsPassed = 1;

class RobotGrid
{
    List<(int X, int Y)> _robotPositions;
    List<(int XVelocity, int YVelocity)> _robotVelocities;
    List<List<char>> _robotLocationGraph;
    int _gridRow;
    int _gridCol;

    public RobotGrid(string filepath, int row, int col)
    {
        _robotPositions = new List<(int X, int Y)>();
        _robotVelocities = new List<(int XVelocity, int YVelocity)>();
        _robotLocationGraph = new List<List<char>>();
        _gridRow = row;
        _gridCol = col;

        CreateRobotLocationGraph();
        GetRobotDataFromFile(filepath);
    }

    private void GetRobotDataFromFile(string filepath)
    {
        using (StreamReader sr = new StreamReader(filepath))
        {
            string line;
            var regex = new Regex(@"p=(\d+),(\d+)\s+v=([\-\d]+),([\-\d]+)");

            while ((line = sr.ReadLine()) != null)
            {
                var match = regex.Match(line);
                if (match.Success)
                {
                    // Parse positions
                    int posX = int.Parse(match.Groups[1].Value);
                    int posY = int.Parse(match.Groups[2].Value);

                    // Parse velocities
                    int velX = int.Parse(match.Groups[3].Value);
                    int velY = int.Parse(match.Groups[4].Value);

                    // Add to lists
                    _robotPositions.Add((posX, posY));
                    _robotVelocities.Add((velX, velY));
                }
            }
        }
    }

    private void CreateRobotLocationGraph()
    {
        _robotLocationGraph = new List<List<char>>();

        for (int r = 0; r < _gridRow; r++)
        {
            List<char> row = new List<char>();

            for (int c = 0; c < _gridCol; c++)
            {
                row.Add('.');
            }

            _robotLocationGraph.Add(row);
        }
    }

    private void UpdateRobotLocationGraph()
    {
        // Reset the graph with '.' for empty spaces
        CreateRobotLocationGraph();

        // Update the graph with robot positions
        foreach (var (x, y) in _robotPositions)
        {
            if (_robotLocationGraph[y][x] == '.')
            {
                // If the cell is empty, set it to '1'
                _robotLocationGraph[y][x] = '1';
            }
            else
            {
                // If the cell already contains a number, increment it
                int currentValue = _robotLocationGraph[y][x] - '0';
                _robotLocationGraph[y][x] = (char)(currentValue + 1 + '0');
            }
        }
    }

    public void DisplayRobotLocationGraph()
    {
        UpdateRobotLocationGraph();

        foreach (var row in _robotLocationGraph)
        {
            Console.WriteLine(string.Join("", row));
        }
    }

    public void PrintRobotData()
    {
        Console.WriteLine("Robot Positions and Velocities:");
        for (int i = 0; i < _robotPositions.Count; i++)
        {
            Console.WriteLine($"Robot {i}: Position={_robotPositions[i]}, Velocity={_robotVelocities[i]}");
        }
    }
}
