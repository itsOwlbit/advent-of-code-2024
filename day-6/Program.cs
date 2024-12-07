// string filePath = "day-6/aoc-day6-test.txt";
string filePath = "day-6/aoc-day6-data.txt";

LabMap labMap = new LabMap(filePath);

// labMap.PrintMap();
// labMap.PrintGuardPosition();
// labMap.PrintObstaclePositions();
Console.WriteLine();
labMap.MoveGuard();
int xCount = labMap.CountXInMap();
Console.WriteLine($"Number of 'X' in map: {xCount}");

int loopCount = labMap.CountPossibleLoops();
Console.WriteLine($"Number of possible loops: {loopCount}");

class LabMap
{
    private List<char> _map;
    private int _rows = 0;
    private int _cols = 0;
    private List<int> _guardsPositions;
    private List<int> _obstaclePositions;
    private static readonly char[] positionalChars = { '^', '>', 'v', '<' };
    private List<(int row, int col)> _guardStopPositions;

    public LabMap(string filePath)
    {
        _map = new List<char>();
        _guardStopPositions = new List<(int row, int col)>();
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
            bool hasExitedMap = false;
            int nextPosition = position;

            _map[position] = 'X';

            while (!hasExitedMap)
            {
                int lastValidPosition = nextPosition;

                switch (direction)
                {
                    case var _ when direction == positionalChars[0]:
                        nextPosition = GuardFacingUp(nextPosition);
                        break;
                    case var _ when direction == positionalChars[1]:
                        nextPosition = GuardFacingRight(nextPosition);
                        break;
                    case var _ when direction == positionalChars[2]:
                        nextPosition = GuardFacingDown(nextPosition);
                        break;
                    case var _ when direction == positionalChars[3]:
                        nextPosition = GuardFacingLeft(nextPosition);
                        break;
                    default:
                        Console.WriteLine($"Invalid direction at: {position}: {direction}");
                        break;
                }

                // Change guard's position and facing direction
                if (nextPosition >= 0 && nextPosition < _map.Count)
                {
                    int row = nextPosition / _cols;
                    int col = nextPosition % _cols;
                    _guardStopPositions.Add((row, col));

                    ChangeGuardsPosition(position, nextPosition, direction);
                    direction = _map[nextPosition];
                }
                else
                {
                    int row = lastValidPosition / _cols;
                    int col = lastValidPosition % _cols;

                    if (direction == positionalChars[0]) // Exiting top
                    {
                        _guardStopPositions.Add((0, col));
                        Console.WriteLine($"Guard exited at the top: (0, {col})");
                    }
                    else if (direction == positionalChars[1]) // Exiting right
                    {
                        _guardStopPositions.Add((row, _cols - 1));
                        Console.WriteLine($"Guard exited at the right: ({row}, {_cols - 1})");
                    }
                    else if (direction == positionalChars[2]) // Exiting bottom
                    {
                        _guardStopPositions.Add((_rows - 1, col));
                        Console.WriteLine($"Guard exited at the bottom: ({_rows - 1}, {col})");
                    }
                    else if (direction == positionalChars[3]) // Exiting left
                    {
                        _guardStopPositions.Add((row, 0));
                        Console.WriteLine($"Guard exited at the left: ({row}, 0)");
                    }

                    hasExitedMap = true;
                    Console.WriteLine("Guard has exited map.");
                }

                // PrintMap();
            }
        }
        Console.WriteLine($"\nGuard visited: {string.Join(", ", _guardStopPositions)}");
    }

    private int GuardFacingUp(int position)
    {
        // Console.WriteLine("Guard is facing up.");
        _map[position] = 'X';

        // Move up until obstacle
        int nextStep = position - _cols;

        while (nextStep >= 0)
        {
            if (_map[nextStep] == '#')
            {
                // Console.WriteLine($"Obstacle ahead at: {nextStep}");
                return nextStep + _cols;
            }

            _map[nextStep] = 'X';
            nextStep -= _cols;
        }

        Console.WriteLine($"Reached top of map from ({nextStep + _cols})");
        return -1;
    }

    private int GuardFacingRight(int position)
    {
        // Console.WriteLine("Guard is facing right.");
        _map[position] = 'X';

        // Move right until obstacle
        int nextStep = position + 1;

        while (nextStep % _cols != 0)
        {
            if (_map[nextStep] == '#')
            {
                // Console.WriteLine($"Obstacle ahead at: {nextStep}");
                return nextStep - 1;
            }

            _map[nextStep] = 'X';
            nextStep++;
        }

        Console.WriteLine($"Reached far right of map from ({nextStep - 1})");
        return -1;
    }

    private int GuardFacingDown(int position)
    {
        // Console.WriteLine("Guard is facing down.");
        _map[position] = 'X';

        // Move down until obstacle
        int nextStep = position + _cols;

        while (nextStep < _map.Count)
        {
            if (_map[nextStep] == '#')
            {
                // Console.WriteLine($"Obstacle ahead at: {nextStep}");
                return nextStep - _cols;
            }

            _map[nextStep] = 'X';
            nextStep += _cols;
        }

        Console.WriteLine($"Reached bottom of map from ({nextStep - _cols})");
        return -1;
    }

    private int GuardFacingLeft(int position)
    {
        // Console.WriteLine("Guard is facing left.");
        _map[position] = 'X';

        // Move left until obstacle
        int nextStep = position - 1;

        while (nextStep >= 0 && nextStep % _cols != _cols - 1)
        {
            if (_map[nextStep] == '#')
            {
                // Console.WriteLine($"Obstacle ahead: {nextStep}");
                return nextStep + 1;
            }

            _map[nextStep] = 'X';
            nextStep--;
        }

        Console.WriteLine($"Reached far left of map from ({nextStep + 1})");
        return -1;
    }

    private void ChangeGuardsPosition(int currentPosition, int newPosition, char currentDirection)
    {
        // Find the index of the current direction
        int directionIndex = Array.IndexOf(positionalChars, currentDirection);
        if (directionIndex == -1)
        {
            Console.WriteLine($"Invalid direction: {currentDirection}");
            return;
        }

        // Update the new position with the correct character
        _map[newPosition] = positionalChars[(directionIndex + 1) % positionalChars.Length];
    }

    public int CountXInMap()
    {
        int count = _map.Count(c => c == 'X');

        return count;
    }

    public int CountPossibleLoops()
    {
        int loopCount = 0;

        // Use a HashSet to store previously visited positions for faster lookup
        HashSet<(int row, int col)> visitedPositions = new HashSet<(int, int)>();

        // Iterate over each position the guard has visited in _guardStopPositions
        for (int i = 0; i < _guardStopPositions.Count - 1; i++)  // Ensure we don't go out of bounds
        {
            var (currentRow, currentCol) = _guardStopPositions[i];
            var (nextRow, nextCol) = _guardStopPositions[i + 1];

            Console.WriteLine($"\nChecking from ({currentRow}, {currentCol}) to ({nextRow}, {nextCol})");

            bool isGoingUp = currentCol == nextCol && nextRow < currentRow;
            bool isGoingRight = currentRow == nextRow && nextCol > currentCol;
            bool isGoingDown = currentCol == nextCol && nextRow > currentRow;
            bool isGoingLeft = currentRow == nextRow && nextCol < currentCol;

            if (isGoingUp)
            {
                Console.WriteLine($"Moving up from ({currentRow}, {currentCol}), checking rows from {currentRow - 1} to {nextRow + 1} and columns from {currentCol} to 0");

                for (int rowCheck = currentRow - 1; rowCheck > nextRow; rowCheck--)
                {
                    for (int colCheck = currentCol + 1; colCheck < _cols; colCheck++)
                    {
                        int linearPos = rowCheck * _cols + colCheck;
                        int obstaclePos = (rowCheck * _cols + colCheck) + 1;

                        // Check if this position is in visitedPositions and not in obstacle positions
                        if (visitedPositions.Contains((rowCheck, colCheck)) &&
                            !_obstaclePositions.Contains((linearPos)) &&
                            _obstaclePositions.Contains((obstaclePos)))
                        {
                            Console.WriteLine($"Loop detected at ({rowCheck}, {colCheck})");
                            loopCount++; // Increment loop count if loop is found
                        }
                        else if (_obstaclePositions.Contains((linearPos)))
                        {
                            break;
                        }
                    }
                }
            }
            else if (isGoingRight)
            {
                // Check if there is any previously visited position in the same column, and rows below the current row
                Console.WriteLine($"Moving right from ({currentRow}, {currentCol}), checking rows from {currentRow + 1} to {_rows - 1} and columns from {currentCol + 1} to {nextCol - 1}");

                // Loop over columns between currentCol to nextCol - 1
                for (int colCheck = currentCol + 1; colCheck < nextCol; colCheck++)
                {
                    // Loop over rows between currentRow + 1 to _rows - 1
                    for (int rowCheck = currentRow + 1; rowCheck < _rows; rowCheck++)
                    {
                        int linearPos = rowCheck * _cols + colCheck;
                        int obstaclePos = (rowCheck + 1) * _cols + colCheck;

                        // Check if this position is in visitedPositions and not in obstacle positions
                        if (visitedPositions.Contains((rowCheck, colCheck)) &&
                            !_obstaclePositions.Contains((linearPos)) &&
                            _obstaclePositions.Contains((obstaclePos)))
                        {
                            Console.WriteLine($"Loop detected at ({rowCheck}, {colCheck})");
                            loopCount++; // Increment loop count if loop is found
                        }
                        else if (_obstaclePositions.Contains((linearPos)))
                        {
                            break;
                        }
                    }
                }
            }
            else if (isGoingDown)
            {
                Console.WriteLine($"Moving down from ({currentRow}, {currentCol}), checking rows from {currentRow + 1} to {nextRow - 1} and columns from {currentCol - 1} to 0");

                for (int rowCheck = currentRow + 1; rowCheck < nextRow; rowCheck++)
                {
                    for (int colCheck = currentCol - 1; colCheck >= 0; colCheck--)
                    {
                        int linearPos = rowCheck * _cols + colCheck;
                        int obstaclePos = (rowCheck * _cols + colCheck) - 1;

                        // Check if this position is in visitedPositions and not in obstacle positions
                        if (visitedPositions.Contains((rowCheck, colCheck)) &&
                            !_obstaclePositions.Contains((linearPos)) &&
                            _obstaclePositions.Contains((obstaclePos)))
                        {
                            Console.WriteLine($"Loop detected at ({rowCheck}, {colCheck})");
                            loopCount++; // Increment loop count if loop is found
                        }
                        else if (_obstaclePositions.Contains((linearPos)))
                        {
                            break;
                        }
                    }
                }
            }
            else if (isGoingLeft)
            {
                // Check if there is any previously visited position in the same column, and rows below the current row
                Console.WriteLine($"Moving left from ({currentRow}, {currentCol}), checking rows from {currentRow - 1} to 0 and columns from {currentCol - 1} to {nextCol + 1}");

                // Loop over columns between currentCol to nextCol - 1
                for (int colCheck = currentCol - 1; colCheck > nextCol; colCheck--)
                {
                    // Loop over rows between currentRow + 1 to _rows - 1
                    for (int rowCheck = currentRow - 1; rowCheck >= 0; rowCheck--)
                    {
                        int linearPos = rowCheck * _cols + colCheck;
                        int obstaclePos = (rowCheck - 1) * _cols + colCheck;

                        // Check if this position is in visitedPositions and not in obstacle positions
                        if (visitedPositions.Contains((rowCheck, colCheck)) &&
                            !_obstaclePositions.Contains((linearPos)) &&
                            _obstaclePositions.Contains((obstaclePos)))
                        {
                            Console.WriteLine($"Loop detected at ({rowCheck}, {colCheck})");
                            loopCount++; // Increment loop count if loop is found
                        }
                        else if (_obstaclePositions.Contains((linearPos)))
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Something wrong with directions.");
            }

            // Add the current position to the visited positions set
            visitedPositions.Add((currentRow, currentCol));
        }

        return loopCount;
    }
}