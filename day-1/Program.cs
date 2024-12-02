// lists to store input data
List<int> listA = new List<int>();
List<int> listB = new List<int>();

// populate lists using data imported from text file
try
{
    foreach (string line in File.ReadLines("aoc-day1-data.txt"))
    {
        // help solve the problem of invisible characters in my text file
        string[] parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 2)
        {
            int numberA = int.Parse(parts[0]);
            int numberB = int.Parse(parts[1]);

            listA.Add(numberA);
            listB.Add(numberB);
        }
        else
        {
            Console.WriteLine($"Invalid line from: {line}");
        }
    }

    // trouble shooting small test text files
    // Console.WriteLine("List A: " + string.Join(". ", listA));
    // Console.WriteLine("List B: " + string.Join(". ", listB));
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}

// sort a copied list for evaluation
List<int> sortedlistA = new List<int>(listA);
List<int> sortedlistB = new List<int>(listB);
sortedlistA.Sort();
sortedlistB.Sort();

int sum = 0;

// part 1: sum = total difference for each row of data
for (int i = 0; i < sortedlistA.Count; i++)
{
    sum += Math.Abs(sortedlistA[i] - sortedlistB[i]);
}

int similarityScore = 0;

// part 2: similarity score = sum of listA value * number of times listA value is seen in listB
for (int i = 0; i < listA.Count; i++)
{
    int leftNumber = listA[i];
    int count = 0;

    for (int j = 0; j < listB.Count; j++)
    {
        if (leftNumber == listB[j])
        {
            count++;
        }
    }

    similarityScore += leftNumber * count;
}

Console.WriteLine($"Answer to part 1 is: {sum}");
Console.WriteLine($"Answer to part 2 is: {similarityScore}");