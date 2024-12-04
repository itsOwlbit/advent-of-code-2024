string filePath = "day-4/aoc-day4-test.txt";
// string filePath = "/day-4/aoc-day4-data.txt"

List<char> puzzle = new List<char>();
int rows = 0;
int cols = 0;

ReadPuzzleFromFile(filePath, puzzle, ref rows, ref cols);
DisplayPuzzle(puzzle, rows, cols);

string word = "XMAS";

static void ReadPuzzleFromFile(string filePath, List<char> puzzle, ref int rows, ref int cols)
{
    using (StreamReader sr = new StreamReader(filePath))
    {
        string line;

        while ((line = sr.ReadLine()) != null)
        {
            if (cols == 0)
            {
                cols = line.Length;
            }

            foreach (char c in line)
            {
                puzzle.Add(c);
            }

            rows++;
        }
    }
}

static void DisplayPuzzle(List<char> puzzle, int rows, int cols)
{
    for (int row = 0; row < rows; row++)
    {
        for (int col = 0; col < cols; col++)
        {
            Console.Write(puzzle[row * cols + col] + " ");
        }
        Console.WriteLine();
    }
}