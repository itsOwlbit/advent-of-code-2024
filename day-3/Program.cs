// Test file: day-3/aoc-day3-test.txt
// Data file: day-3/aoc-day3-data.txt
DayThreeAoC dayThreeAoc = new DayThreeAoC("day-3/aoc-day3-test.txt");

// to check what was stored from file
Console.WriteLine(dayThreeAoc.Data);

class DayThreeAoC
{
    private readonly string pattern = @"mul\(\d{1,3},\d{1,3}\)";
    private readonly string data;
    public string Data => (data);
    private List<string> dataParts;

    public DayThreeAoC(string filePath)
    {
        data = ReadDataFromFile(filePath);
    }

    private string ReadDataFromFile(string filePath)
    {
        string dataFromFile = "";

        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                dataFromFile = sr.ReadToEnd();
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }

        return dataFromFile;
    }

}