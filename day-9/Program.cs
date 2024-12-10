string filePath = "day-9/aoc-day9-test.txt";
// string filePath = "day-9/aoc-day9-data.txt";

ComputerDisk disk = new ComputerDisk(filePath);

disk.PrintDiskMap();
Console.WriteLine();

disk.GetDiskDrive();
Console.WriteLine();

disk.DefragDiskDrive();
Console.WriteLine("Result after defragmenting:");
disk.DisplayDefragResult();
Console.WriteLine();

class ComputerDisk
{
    private string _diskMap = "";
    private List<char> _diskDrive;
    private int _checkSum = 0;

    public ComputerDisk(string filePath)
    {
        _diskDrive = new List<char>();


        ImportDataFromFile(filePath);
        StoreDataIntoDiskDrive();
    }

    private void ImportDataFromFile(string filePath)
    {
        using (StreamReader sr = new StreamReader(filePath))
        {
            string? line = sr.ReadLine();
            if (line != null)
            {
                _diskMap = line;
            }
        }
    }

    private void StoreDataIntoDiskDrive()
    {
        int idNumber = 0;

        for (int i = 0; i < _diskMap.Length; i++)
        {
            if (char.IsDigit(_diskMap[i]))
            {
                int fileLength = _diskMap[i] - '0';
                if (i % 2 == 0)
                {
                    // Add characters to _diskDrive list
                    _diskDrive.AddRange(CreateBlockFile((char)('0' + idNumber), fileLength));
                    idNumber++;
                }
                else
                {
                    // Add characters to _diskDrive list
                    _diskDrive.AddRange(CreateBlockFile('.', fileLength));
                }
            }
            else
            {
                Console.WriteLine($"Invalid character '{_diskMap[i]}' in disk map.");
            }
        }
    }

    // Helper for StoreDataIntoDiskDrive
    private List<char> CreateBlockFile(char fileID, int fileLength)
    {
        if (fileLength <= 0) return new List<char>();
        return new List<char>(new string(fileID, fileLength).ToCharArray());
    }

    public void PrintDiskMap()
    {
        Console.WriteLine($"Disk Map: {_diskMap}");
    }

    public void GetDiskDrive()
    {
        Console.WriteLine($"Disk Drive: {string.Join("", _diskDrive)}");  // Join List<char> into a single string
    }

    public void DefragDiskDrive()
    {
        List<char> defragmentedDisk = new List<char>();
        int numberOfBlocks = _diskDrive.Count;

        for (int i = 0; i < numberOfBlocks; i++)
        {
            if (_diskDrive[i] == '.')
            {
                for (int j = numberOfBlocks - 1; j > i; j--)
                {
                    if (_diskDrive[j] != '.')
                    {
                        // Console.WriteLine($"Found {_diskDrive[i]} so swapping in {_diskDrive[j]}");
                        defragmentedDisk.Add(_diskDrive[j]);
                        numberOfBlocks = j;
                        break;
                    }
                }
            }
            else
            {
                defragmentedDisk.Add(_diskDrive[i]);
            }
        }

        _diskDrive = defragmentedDisk;
    }

    private void CalculateCheckSum()
    {
        _checkSum = 0;

        for (int i = 0; i < _diskDrive.Count; i++)
        {
            int checksum = i * (_diskDrive[i] - '0');
            // Console.WriteLine($"{checksum} = {i} * {_diskDrive[i]}");
            _checkSum += checksum;
        }

        Console.WriteLine($"Checksum: {_checkSum}");
    }

    public void DisplayDefragResult()
    {
        this.GetDiskDrive();
        this.CalculateCheckSum();
    }
}