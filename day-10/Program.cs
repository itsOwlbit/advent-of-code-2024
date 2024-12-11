string filePath = "day-10/aoc-day10-test.txt";

Trails lavaIslandHiking = new Trails(filePath);
lavaIslandHiking.DisplayColumnAndRowCount();
Console.WriteLine();
lavaIslandHiking.FindAllTrails();
lavaIslandHiking.DisplayTrailPaths();

class Trails
{
    private List<List<int>> _topographyMap;
    private List<List<(int row, int col)>> _trailPaths;
    private int _rows = 0;
    private int _cols = 0;
    private List<int> _trailCounts;

    public Trails(string filePath)
    {
        _topographyMap = new List<List<int>>();
        _trailPaths = new List<List<(int row, int col)>>();
        _trailCounts = new List<int>();

        ReadDataFromFile(filePath);
    }

    private void ReadDataFromFile(string filePath)
    {
        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line);

                List<int> row = line.Select(ch => int.Parse(ch.ToString())).ToList();

                _topographyMap.Add(row);

                if (_cols == 0)
                {
                    _cols = row.Count;
                }

                _rows++;
            }
        }
    }

    public void FindAllTrails()
    {
        _trailCounts.Clear();
        Console.WriteLine("Starting trail search...");

        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cols; c++)
            {
                if (_topographyMap[r][c] == 0)
                {
                    Console.WriteLine($"Starting new trail from ({r}, {c})...");
                    int trailsFromThisStart = FindTrail(r, c, 0, new List<(int row, int col)>());
                    Console.WriteLine($"Trails found starting at ({r}, {c}): {trailsFromThisStart}");
                    _trailCounts.Add(trailsFromThisStart);
                }
            }
        }

        Console.WriteLine("All trails completed.");
        Console.WriteLine($"Trail Counts: {string.Join(", ", _trailCounts)}");
    }

    private int FindTrail(int row, int col, int trailHeight, List<(int row, int col)> trailPath)
    {
        // Check bounds
        if (row < 0 || row >= _rows || col < 0 || col >= _cols)
            return 0;

        // If the current cell does not match the expected trail height, stop
        if (_topographyMap[row][col] != trailHeight)
            return 0;

        // Add the current position to the trail path
        trailPath.Add((row, col));
        Console.WriteLine($"Current Trail Path: {string.Join(" -> ", trailPath)}");

        // If we've reached the end of the trail (9), count this trail
        if (trailHeight == 9)
        {
            Console.WriteLine($"Trail complete! Path: {string.Join(" -> ", trailPath)}");
            return 1;
        }

        // Temporarily mark the cell as visited to prevent revisiting
        int originalValue = _topographyMap[row][col];
        _topographyMap[row][col] = -1;

        // Explore all four directions
        int totalTrails = 0;
        int[][] directions = new int[][]
        {
        new int[] { -1, 0 }, // Up
        new int[] { 1, 0 },  // Down
        new int[] { 0, -1 }, // Left
        new int[] { 0, 1 }   // Right
        };

        foreach (var dir in directions)
        {
            int newRow = row + dir[0];
            int newCol = col + dir[1];

            totalTrails += FindTrail(newRow, newCol, trailHeight + 1, new List<(int row, int col)>(trailPath));
        }

        // Restore the original value for backtracking
        _topographyMap[row][col] = originalValue;

        return totalTrails;
    }

    public void DisplayColumnAndRowCount()
    {
        Console.WriteLine($"rows: {_rows}, cols: {_cols}");
    }

    public void DisplayTrailPaths()
    {
        int trailIndex = 1;
        foreach (var trail in _trailPaths)
        {
            Console.WriteLine($"Trail {trailIndex}:");
            foreach (var (row, col) in trail)
            {
                Console.Write($"({row}, {col}) ");
            }
            Console.WriteLine();
            trailIndex++;
        }
        Console.WriteLine($"count: {string.Join(", ", _trailCounts)}");
    }
}