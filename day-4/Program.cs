// part 1: count the number of times the word is found in the puzzle (8 directions)

string filePath = "day-4/aoc-day4-test.txt";
// string filePath = "day-4/aoc-day4-puzzle.txt";

List<char> puzzle = new List<char>();
int rows = 0;
int cols = 0;

ReadPuzzleFromFile(filePath, puzzle, ref rows, ref cols);
// DisplayPuzzle(puzzle, rows, cols);

string word = "XMAS";
int totalCount = CountWordOccurrences(puzzle, word, rows, cols);

Console.WriteLine($"Part 1: {word} is found {totalCount} times in the puzzle.");

// part 2: count the number of times the word is found as an X pattern

rows = 0;
cols = 0;

ReadPuzzleFromFile(filePath, puzzle, ref rows, ref cols);
DisplayPuzzle(puzzle, rows, cols);

word = "MAS";
totalCount = 0;

Console.WriteLine($"Part 2: {word} is found in an X pattern {totalCount} times in the puzzle.");

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

static int CountWordOccurrences(List<char> puzzle, string word, int rows, int cols)
{
    int count = 0;

    for (int row = 0; row < rows; row++)
    {
        for (int col = 0; col < cols; col++)
        {
            count += CountWordInAllDirections(puzzle, word, row, col, rows, cols);
        }
    }

    return count;
}

static int CountWordInAllDirections(List<char> puzzle, string word, int startRow, int startCol, int rows, int cols)
{
    int count = 0;

    // 8 possible directions in which the word can be searched
    int[] rowDirs = { -1, -1, -1, 0, 0, 1, 1, 1 };
    int[] colDirs = { -1, 0, 1, -1, 1, -1, 0, 1 };

    for (int dir = 0; dir < 8; dir++)
    {
        int currentRow = startRow;
        int currentCol = startCol;
        int matchCount = 0;

        for (int i = 0; i < word.Length; i++)
        {
            if (currentRow >= 0 && currentRow < rows && currentCol >= 0 && currentCol < cols && puzzle[currentRow * cols + currentCol] == word[i])
            {
                matchCount++;
                currentRow += rowDirs[dir];
                currentCol += colDirs[dir];
            }
            else
            {
                break;
            }
        }

        if (matchCount == word.Length)
        {
            count++;
        }
    }

    return count;
}