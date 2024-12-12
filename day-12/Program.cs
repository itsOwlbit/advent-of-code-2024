// string filepath = "day-12/aoc-day12-test-mini.txt";
// string filepath = "day-12/aoc-day12-test.txt";
string filepath = "day-12/aoc-day12-data.txt";

Plots garden = new Plots(filepath);

garden.printGarden();
Console.WriteLine();

int totalPrice = garden.CalculateTotalPrice();
Console.WriteLine($"Total Price: {totalPrice}");
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

    // region: connected character that are next to each other (above, below, left, or right)
    // area: number of nodes in a region
    // perimeter: number of exposed sides for the region
    // node: a representation of a square with 4 sides
    // price of a region = area * perimeter
    // total price = sum of all region's price

    public int CalculateTotalPrice()
    {
        var visited = new HashSet<(int, int)>();

        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                if (!visited.Contains((row, col)) && _plots[row][col] != ' ')
                {
                    int area = 0;
                    int perimeter = 0;

                    DFS(_plots, row, col, _plots[row][col], visited, ref area, ref perimeter);

                    int price = area * perimeter;
                    _totalPrice += price;

                    Console.WriteLine($"Region found: Character = {_plots[row][col]}, Area = {area}, Perimeter = {perimeter}, Price = {price}");
                }
            }
        }

        return _totalPrice;
    }

    static void DFS(List<List<char>> grid, int row, int col, char target, HashSet<(int, int)> visited, ref int area, ref int perimeter)
    {
        // Check for out-of-bounds or invalid cells
        if (row < 0 || row >= grid.Count || col < 0 || col >= grid[0].Count || visited.Contains((row, col)) || grid[row][col] != target)
        {
            return;
        }

        // Mark the current cell as visited
        visited.Add((row, col));
        area++; // Increment the area for this cell

        // Start with the assumption that this node has 4 sides
        int localPerimeter = 4;

        // Check each direction to reduce the perimeter for shared sides
        if (row > 0 && grid[row - 1][col] == target) localPerimeter--; // Up
        if (row < grid.Count - 1 && grid[row + 1][col] == target) localPerimeter--; // Down
        if (col > 0 && grid[row][col - 1] == target) localPerimeter--; // Left
        if (col < grid[0].Count - 1 && grid[row][col + 1] == target) localPerimeter--; // Right

        // Add the local perimeter of this node to the total perimeter
        perimeter += localPerimeter;

        // Continue exploring neighbors
        DFS(grid, row - 1, col, target, visited, ref area, ref perimeter); // Up
        DFS(grid, row + 1, col, target, visited, ref area, ref perimeter); // Down
        DFS(grid, row, col - 1, target, visited, ref area, ref perimeter); // Left
        DFS(grid, row, col + 1, target, visited, ref area, ref perimeter); // Right
    }
}