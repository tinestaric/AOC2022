using System;
using System.Text.RegularExpressions;

public class Day05 : IDay
{
    public void FirstChallenge(string[] lines)
    {
        Dictionary<int, List<char>> containerSchema = new Dictionary<int, List<char>>()
        {
            {1, new List<char>() },
            {2, new List<char>() },
            {3, new List<char>() },
            {4, new List<char>() },
            {5, new List<char>() },
            {6, new List<char>() },
            {7, new List<char>() },
            {8, new List<char>() },
            {9, new List<char>() }
        };

        foreach (string line in lines)
        {
            if (line.StartsWith("move"))
                MoveContainers(line, containerSchema);
            else
                InitContainerSchema(line, containerSchema);
        }

        foreach (List<char> container in containerSchema.Values)
            Console.Write(container.FirstOrDefault());

        Console.WriteLine(" ");
    }

    private void MoveContainers(string line, Dictionary<int, List<char>> containerSchema)
    {

        int noOfCrates = GetNumberFromString(line, line.IndexOf("move ") + 5);
        int from = GetNumberFromString(line, line.IndexOf("from ") + 5);
        int to = GetNumberFromString(line, line.IndexOf("to ") + 3);

        if (noOfCrates == 0)
            return;

        containerSchema.TryGetValue(from, out var fromContainerRow);
        containerSchema.TryGetValue(to, out var toContainerRow);

        for (int i = 0; i < noOfCrates; i++)
        {            
            toContainerRow.Insert(0, fromContainerRow.First());
            fromContainerRow.RemoveAt(0);
        }
    }

    private static int GetNumberFromString(string line, int startPos)
    {
        int endPos = startPos;

        while (char.IsDigit(line[endPos]))
        {
            endPos++;
            if (endPos == line.Length)
                break;
        }
        
        endPos--;

        return Int32.Parse(line.Substring(startPos, endPos - startPos + 1));
    }

    private void InitContainerSchema(string line, Dictionary<int, List<char>> containerSchema)
    {
        if (line.Length == 0 || line[1] == '1')
            return;

        for (int i = 0; i < (line.Length + 1) / 4; i++)
        {
            char container = line[i * 4 + 1];
            if (container == ' ')
                continue;

            if (containerSchema.TryGetValue(i + 1, out List<char> containerColumn))
                containerColumn.Add(container);
        }
    }

    public void SecondChallenge(string[] lines)
    {
        Dictionary<int, List<char>> containerSchema = new Dictionary<int, List<char>>()
        {
            {1, new List<char>() },
            {2, new List<char>() },
            {3, new List<char>() },
            {4, new List<char>() },
            {5, new List<char>() },
            {6, new List<char>() },
            {7, new List<char>() },
            {8, new List<char>() },
            {9, new List<char>() }
        };

        foreach (string line in lines)
        {
            if (line.StartsWith("move"))
                MoveContainers9001(line, containerSchema);
            else
                InitContainerSchema(line, containerSchema);
        }

        foreach (List<char> container in containerSchema.Values)
            Console.Write(container.FirstOrDefault());

        Console.WriteLine(" ");
    }

    private void MoveContainers9001(string line, Dictionary<int, List<char>> containerSchema)
    {

        int noOfCrates = GetNumberFromString(line, line.IndexOf("move ") + 5);
        int from = GetNumberFromString(line, line.IndexOf("from ") + 5);
        int to = GetNumberFromString(line, line.IndexOf("to ") + 3);

        if (noOfCrates == 0)
            return;

        containerSchema.TryGetValue(from, out var fromContainerRow);
        containerSchema.TryGetValue(to, out var toContainerRow);

        for (int i = 0; i < noOfCrates; i++)
        {
            toContainerRow.Insert(i, fromContainerRow.First());
            fromContainerRow.RemoveAt(0);
        }
    }
}
