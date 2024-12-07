﻿try
{
    string filePath = "day-7/aoc-day7-test.txt";

    var calibrator = new Calibrator(filePath);
    var testResults = calibrator.ReadDataFromFile();

    int sumOfValidResults = 0;

    foreach (var testResult in testResults)
    {
        Console.WriteLine($"Processing: {testResult}");

        if (BFSValidator.ValidateEquation(testResult.TestValues, testResult.TestResultValue))
        {
            Console.WriteLine($"Valid equation found for {testResult.TestResultValue}");
            sumOfValidResults += testResult.TestResultValue;
        }
        else
        {
            Console.WriteLine($"No valid equation found for {testResult.TestResultValue}");
        }
    }

    Console.WriteLine($"\nSum of all valid TestResultValues: {sumOfValidResults}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

struct TestResult
{
    public int TestResultValue { get; set; }
    public List<int> TestValues { get; set; }

    public TestResult(int testResultValue, List<int> testValues)
    {
        TestResultValue = testResultValue;
        TestValues = testValues;
    }

    public override string ToString()
    {
        return $"Test Result: {TestResultValue}, TestValues: [{string.Join(", ", TestValues)}]";
    }
}

class Calibrator
{
    private readonly string _filePath;

    public Calibrator(string filePath)
    {
        _filePath = filePath;
    }

    public IEnumerable<TestResult> ReadDataFromFile()
    {
        var testResults = new List<TestResult>();

        using (StreamReader sr = new StreamReader(_filePath))
        {
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(':');
                if (parts.Length != 2)
                {
                    throw new FormatException($"Invalid line format: {line}");
                }

                if (!int.TryParse(parts[0].Trim(), out int testResultValue))
                {
                    throw new FormatException($"Invalid TestResult value: {parts[0].Trim()}");
                }

                var testValues = new List<int>();
                var valueParts = parts[1].Trim().Split(' ');

                foreach (var value in valueParts)
                {
                    if (int.TryParse(value.Trim(), out int testValue))
                    {
                        testValues.Add(testValue);
                    }
                    else
                    {
                        throw new FormatException($"Invalid TestValue: {value.Trim()}");
                    }
                }

                testResults.Add(new TestResult(testResultValue, testValues));
            }
        }

        return testResults;
    }
}

class BFSValidator
{
    public static bool ValidateEquation(List<int> testValues, int target)
    {
        // Create queue
        var queue = new Queue<(int result, int index)>();
        // Start with the first value as the initial result
        queue.Enqueue((testValues[0], 0));

        // While not empty
        while (queue.Count > 0)
        {
            var (currentResult, currentIndex) = queue.Dequeue();

            // Base case: if we used all testValues
            if (currentIndex == testValues.Count - 1)
            {
                if (currentResult == target)
                {
                    return true;
                }
                continue;
            }

            // Get next test in list
            int nextValue = testValues[currentIndex + 1];

            // Add the next value
            queue.Enqueue((currentResult + nextValue, currentIndex + 1));

            // Multiply the next value
            queue.Enqueue((currentResult * nextValue, currentIndex + 1));
        }

        return false; // No valid equation found
    }
}