using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// Test file: day-3/aoc-day3-test.txt
// Data file: day-3/aoc-day3-data.txt
string data = ReadDataFromFile("day-3/aoc-day3-data.txt");

// used for troubleshooting
// Console.WriteLine(data);

string pattern = @"mul\(\d{1,3},\d{1,3}\)";
List<string> instructions = FindStringMatches(data, pattern);

// used for troubleshooting
// PrintInstructions(instructions);

int result = ProcessInstructions(instructions);

Console.WriteLine($"Result: {result}");

static string ReadDataFromFile(string filePath)
{
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

static List<string> FindStringMatches(string data, string pattern)
{
    var matches = new List<string>();
    Regex regex = new Regex(pattern);

    foreach (Match match in regex.Matches(data))
    {
        matches.Add(match.Value);
    }

    return matches;
}

static void PrintInstructions(List<string> instructions)
{
    Console.WriteLine("List of Instructions");
    foreach (var instruction in instructions)
    {
        Console.WriteLine($"Instruction: {instruction}");
    }
}

static int ProcessInstructions(List<string> instructions)
{
    Regex regex = new Regex(@"\d+");
    int result = 0;

    foreach (string instruction in instructions)
    {
        MatchCollection numbers = regex.Matches(instruction);

        if (numbers.Count == 2)
        {
            int a = int.Parse(numbers[0].Value);
            int b = int.Parse(numbers[1].Value);

            result += a * b;
        }
    }

    return result;
}