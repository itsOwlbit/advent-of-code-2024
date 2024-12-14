using System.Text.RegularExpressions;

string filepath = "day-14/aoc-day14-test.txt";
int width = 101;
int height = 103;

RobotGrid robotGrid = new RobotGrid(filepath, width, height);
robotGrid.PrintRobotData();
Console.WriteLine();

class RobotGrid
{
    List<(int X, int Y)> _robotPositions;
    List<(int XVelocity, int YVelocity)> _robotVelocities;
    int _gridWidth;
    int _gridHeight;

    public RobotGrid(string filepath, int width, int height)
    {
        _robotPositions = new List<(int X, int Y)>();
        _robotVelocities = new List<(int XVelocity, int YVelocity)>();
        _gridWidth = width;
        _gridHeight = height;

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

    public void PrintRobotData()
    {
        Console.WriteLine("Robot Positions and Velocities:");
        for (int i = 0; i < _robotPositions.Count; i++)
        {
            Console.WriteLine($"Robot {i}: Position={_robotPositions[i]}, Velocity={_robotVelocities[i]}");
        }
    }
}
