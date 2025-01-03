﻿// string filePath = "day-6/aoc-day6-test.txt";
string filePath = "day-6/aoc-day6-data.txt";

LabMap myMap = new LabMap(filePath);

// myMap.PrintMap();
// myMap.PrintGuardPosition();
// myMap.PrintObstaclePositions();
Console.WriteLine();
myMap.MoveGuard();
int xCount = myMap.CountXInMap();
Console.WriteLine($"Number of 'X' in map: {xCount}");

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
            bool hasExitedMap = false;
            int nextPosition = position;

            _map[position] = 'X';

            while (!hasExitedMap)
            {
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
                    ChangeGuardsPosition(position, nextPosition, direction);
                    direction = _map[nextPosition];
                }

                if (nextPosition == -1)
                {
                    hasExitedMap = true;
                    Console.WriteLine("Guard has exited map.");
                }

                // PrintMap();
            }
        }
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
}