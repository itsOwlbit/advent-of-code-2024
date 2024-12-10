string filePath = "day-10/aoc-day10-test.txt";

MyClass myClass = new MyClass(filePath);

class MyClass
{
    private List<List<int>> _topographyMap;
    private int _rows = 0;
    private int _cols = 0;

    public MyClass(string filePath)
    {
        _topographyMap = new List<List<int>>();

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

                if (_cols == 0)
                {
                    _cols = line.Length;
                }
            }
        }
    }
}