// part 1: count the number of times the word is found in the puzzle (8 directions)

// string filePath = "day-4/aoc-day4-test.txt";
string filePath = "day-4/aoc-day4-puzzle.txt";

List<char> puzzle = new List<char>();
int rows = 0;
int cols = 0;

ReadPuzzleFromFile(filePath, puzzle, ref rows, ref cols);
// DisplayPuzzle(puzzle, rows, cols);

string word = "XMAS";
int totalCount = CountWord(puzzle, word, rows, cols);

Console.WriteLine($"Part 1: {word} is found {totalCount} times in the puzzle.");

// part 2: count the number of times the word is found as an X pattern

// DisplayPuzzle(puzzle, rows, cols);

word = "MAS";
totalCount = CountWordInXPattern(puzzle, word, rows, cols);

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

static int CountWord(List<char> puzzle, string word, int rows, int cols)
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

static int CountWordInXPattern(List<char> puzzle, string word, int rows, int cols)
{
    if (word.Length != 3)
    {
        throw new ArgumentException("The word must be exactly three characters long");
    }

    int count = 0;

    // avoid out-of-bounds error from the edges
    for (int row = 1; row < rows - 1; row++)
    {
        for (int col = 1; col < cols - 1; col++)
        {
            if (IsXPattern(puzzle, word, row, col, rows, cols))
            {
                count++;
            }
        }
    }

    return count;
}

static bool IsXPattern(List<char> puzzle, string word, int centerRow, int centerCol, int rows, int cols)
{
    char center = puzzle[centerRow * cols + centerCol];

    if (center != word[1])
    {
        return false;
    }

    // 4 possible diagonal directions for the X pattern
    int[] rowOffsets = { -1, 1, -1, 1 };
    int[] colOffsets = { -1, 1, 1, -1 };

    // Check both diagonals to form an X pattern
    bool firstDiagonal = (IsInBounds(centerRow + rowOffsets[0], centerCol + colOffsets[0], rows, cols) &&
                         puzzle[(centerRow + rowOffsets[0]) * cols + (centerCol + colOffsets[0])] == word[0] &&
                         IsInBounds(centerRow + rowOffsets[1], centerCol + colOffsets[1], rows, cols) &&
                         puzzle[(centerRow + rowOffsets[1]) * cols + (centerCol + colOffsets[1])] == word[2]) ||
                         (IsInBounds(centerRow + rowOffsets[0], centerCol + colOffsets[0], rows, cols) &&
                         puzzle[(centerRow + rowOffsets[0]) * cols + (centerCol + colOffsets[0])] == word[2] &&
                         IsInBounds(centerRow + rowOffsets[1], centerCol + colOffsets[1], rows, cols) &&
                         puzzle[(centerRow + rowOffsets[1]) * cols + (centerCol + colOffsets[1])] == word[0]);

    bool secondDiagonal = (IsInBounds(centerRow + rowOffsets[2], centerCol + colOffsets[2], rows, cols) &&
                          puzzle[(centerRow + rowOffsets[2]) * cols + (centerCol + colOffsets[2])] == word[0] &&
                          IsInBounds(centerRow + rowOffsets[3], centerCol + colOffsets[3], rows, cols) &&
                          puzzle[(centerRow + rowOffsets[3]) * cols + (centerCol + colOffsets[3])] == word[2]) ||
                          (IsInBounds(centerRow + rowOffsets[2], centerCol + colOffsets[2], rows, cols) &&
                          puzzle[(centerRow + rowOffsets[2]) * cols + (centerCol + colOffsets[2])] == word[2] &&
                          IsInBounds(centerRow + rowOffsets[3], centerCol + colOffsets[3], rows, cols) &&
                          puzzle[(centerRow + rowOffsets[3]) * cols + (centerCol + colOffsets[3])] == word[0]);

    return firstDiagonal && secondDiagonal;
}

static bool IsInBounds(int row, int col, int rows, int cols)
{
    return row >= 0 && row < rows && col >= 0 && col < cols;
}