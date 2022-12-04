using System;

public class Day04 : IDay
{
    public void FirstChallenge(string[] lines)
    {
        int redundantAssignments = 0;
        foreach (string line in lines)
        {
            string[] assignments = line.Split(',');
            string[] firstAssignment = assignments[0].Split('-');
            string[] secondAssignment = assignments[1].Split('-');

            if (((Int32.Parse(firstAssignment[0]) >= Int32.Parse(secondAssignment[0])) && (Int32.Parse(firstAssignment[0]) <= Int32.Parse(secondAssignment[1]))) 
                || 
                (Int32.Parse(secondAssignment[0]) >= Int32.Parse(firstAssignment[0]) && Int32.Parse(secondAssignment[0]) <= Int32.Parse(firstAssignment[1])))
                redundantAssignments++;
        }

        Console.WriteLine(redundantAssignments);
    }

    public void SecondChallenge(string[] lines)
    {
        return;
        int totalPriority = 0, counter = 0;
        HashSet<char> duplicates = new HashSet<char>();

        foreach (string line in lines)
        {
            counter++;
            HashSet<char> subDuplicates = new HashSet<char>();

            Boolean firstLine = duplicates.Count == 0;

            foreach (char item in line)
            {
                if (firstLine)
                    subDuplicates.Add(item);
                else
                {
                    if (duplicates.Contains(item))
                        subDuplicates.Add(item);
                }
            }

            duplicates = new HashSet<char>(subDuplicates);

        }

        Console.WriteLine(totalPriority);
    }
}
