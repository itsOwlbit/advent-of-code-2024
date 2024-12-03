using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// Test file: day-3/aoc-day3-test.txt
// Data file: day-3/aoc-day3-data.txt
string data = ReadDataFromFile("day-3/aoc-day3-data.txt");

// used for troubleshooting
// Console.WriteLine(data);

string pattern = @"mul\(\d{1,3},\d{1,3}\)";
List<string> instructions = FindInstructions(data, pattern);

// used for troubleshooting
// PrintInstructions(instructions);

int result = ProcessInstructions(instructions);

Console.WriteLine($"Part 1 Result: {result}");

// Part 2: Process instructions with conditionals

// Test file: day-3/aoc-day3-test2.txt
// Test file: day-3/aoc-day3-data.txt
string data2 = ReadDataFromFile("day-3/aoc-day3-data.txt");
string pattern2 = @"mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\)";

List<(int Index, string Instruction)> instructions2 = FindInstructionsWithIndex(data2, pattern2);

int result2 = ProcessInstructionsWithIndex(instructions2);

// PrintInstructionsWithIndex(instructions2);

Console.WriteLine($"Part 2 Result: {result2}");

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

static List<string> FindInstructions(string data, string pattern)
{
    var matches = new List<string>();
    Regex regex = new Regex(pattern);

    foreach (Match match in regex.Matches(data))
    {
        matches.Add(match.Value);
    }

    return matches;
}

static List<(int Index, string Instruction)> FindInstructionsWithIndex(string data, string pattern)
{
    var matches = new List<(int Index, string Instruction)>();
    Regex regex = new Regex(pattern);

    foreach (Match match in regex.Matches(data))
    {
        matches.Add((match.Index, match.Value));
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

static void PrintInstructionsWithIndex(List<(int Index, string Instruction)> instructions)
{
    Console.WriteLine("List of Instructions");
    foreach (var (index, instruction) in instructions)
    {
        Console.WriteLine($"Index: {index} Instruction: {instruction}");
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

static int ProcessInstructionsWithIndex(List<(int Index, string instruction)> instructions)
{
    Regex regex = new Regex(@"\d+");
    bool isDo = true;
    int result = 0;

    foreach (var (index, instruction) in instructions)
    {
        // Console.WriteLine($"Instruction: {instruction}");

        if (isDo && instruction.StartsWith("mul"))
        {
            MatchCollection numbers = regex.Matches(instruction);

            int a = int.Parse(numbers[0].Value);
            int b = int.Parse(numbers[1].Value);

            result += a * b;

            // Console.WriteLine($"Updated result: {result}, isDo: {isDo}");
        }
        else if (instruction == "do()")
        {
            isDo = true;
            // Console.WriteLine("DO");
        }
        else if (instruction == "don't()")
        {
            isDo = false;
            // Console.WriteLine("DON'T");
        }
    }

    return result;
}