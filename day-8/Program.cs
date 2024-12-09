string filePath = "day-8/aoc-day8-test.txt";
// string filePath = "day-8/aoc-day8-data.txt";

AntennaMap antennaMap = new AntennaMap(filePath);
antennaMap.PrintMap();
Console.WriteLine();

antennaMap.ProcessFrequencies();
// antennaMap.PrintFrequencies();
Console.WriteLine();

antennaMap.LocateAntinodes();
antennaMap.PrintMap();
Console.WriteLine();

int antinodeCount = antennaMap.GetAntinodeCount();
Console.WriteLine($"Number of Antinodes: {antinodeCount}");

class AntennaMap
{
    private List<char> _map;
    private int _rows = 0;
    private int _cols = 0;
    private List<char> _frequencies;
    private List<(char Frequency, int Index)> _signals;
    private int _antinodeCount;

    public AntennaMap(string filePath)
    {
        _map = new List<char>();
        _frequencies = new List<char>();
        _signals = new List<(char Frequency, int Index)>();

        GetDataFromFile(filePath);
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
                Console.Write(_map[(_cols * i) + j]);
            }
            Console.WriteLine();
        }
    }

    public void ProcessFrequencies()
    {
        HashSet<char> seenFrequencies = new HashSet<char>();

        for (int index = 0; index < _map.Count; index++)
        {
            char currentChar = _map[index];

            if (currentChar == '.')
            {
                continue;
            }

            if (!seenFrequencies.Contains(currentChar))
            {
                seenFrequencies.Add(currentChar);
                _frequencies.Add(currentChar);
            }

            _signals.Add((currentChar, index));
        }
    }

    public void PrintFrequencies()
    {
        Console.WriteLine("Frequencies Found:");
        foreach (var frequency in _frequencies)
        {
            Console.WriteLine($"Frequency: {frequency}");
        }

        Console.WriteLine("\nSignals and their positions:");
        foreach (var signal in _signals)
        {
            Console.WriteLine($"Frequency: {signal.Frequency}, Index: {signal.Index}");
        }
    }

    public void LocateAntinodes()
    {
        for (int firstIndex = 0; firstIndex < _map.Count; firstIndex++)
        {
            if (_map[firstIndex] != '.' && _map[firstIndex] != '#')
            {
                char frequency = _map[firstIndex];

                for (int secondIndex = firstIndex + 1; secondIndex < _map.Count; secondIndex++)
                {
                    if (_map[secondIndex] == frequency)
                    {
                        // Console.WriteLine($"Frequency: {frequency}, firstIndex: {firstIndex}, secondIndex: {secondIndex}");
                        // Get row and column of first and second indices
                        int row1 = firstIndex / _cols;
                        int col1 = firstIndex % _cols;

                        int row2 = secondIndex / _cols;
                        int col2 = secondIndex % _cols;

                        int rowDifference = row1 - row2;
                        int colDifference = col1 - col2;

                        int newIndex1row = row1 + rowDifference;
                        int newIndex1col = col1 + colDifference;

                        int newIndex2row = row2 - rowDifference;
                        int newIndex2col = col2 - colDifference;

                        if (newIndex1row >= 0 && newIndex1row < _rows &&
                            newIndex1col >= 0 && newIndex1col < _cols)
                        {
                            int newIndex1 = newIndex1row * _cols + newIndex1col;
                            // Console.WriteLine($"index1: {newIndex1}, ({newIndex1row},{newIndex1col}), {_map[newIndex1]}");
                            if (_map[newIndex1] == '.')
                            {
                                _map[newIndex1] = '#';
                                _antinodeCount++;
                                // Console.WriteLine($"Antinode placed at {newIndex1}, Count: {_antinodeCount}");
                            }
                            else if (_frequencies.Contains(_map[newIndex1]))
                            {
                                _antinodeCount++;
                                // Console.WriteLine($"*Count: {_antinodeCount}");
                            }
                        }

                        if (newIndex2row >= 0 && newIndex2row < _rows &&
                            newIndex2col >= 0 && newIndex2col < _cols)
                        {
                            int newIndex2 = newIndex2row * _cols + newIndex2col;
                            // Console.WriteLine($"index2: {newIndex2}, ({newIndex2row},{newIndex2col}), {_map[newIndex2]}");
                            if (_map[newIndex2] == '.')
                            {
                                _map[newIndex2] = '#';
                                _antinodeCount++;
                                // Console.WriteLine($"Antinode placed at {newIndex2}, Count: {_antinodeCount}");
                            }
                            else if (_frequencies.Contains(_map[newIndex2]))
                            {
                                _antinodeCount++;
                                // Console.WriteLine($"*Count: {_antinodeCount}");
                            }
                        }
                    }
                }
            }
        }
    }

    public int GetAntinodeCount()
    {
        return _antinodeCount;
    }
}
