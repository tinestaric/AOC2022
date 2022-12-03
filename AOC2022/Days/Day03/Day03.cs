using System;

public class Day03: IDay
{
    public void FirstChallenge(string[] lines)
    {
        int totalPriority = 0;
        foreach (string line in lines)
        {
            totalPriority += GetItemPriority(FindDuplicateItem(line));
        }

        Console.WriteLine(totalPriority);
    }

    private static char FindDuplicateItem(string line)
    {
        HashSet<char> duplicates = new HashSet<char>();
        char duplicate = '0';

        string first = line.Substring(0, (int)(line.Length / 2));
        string second = line.Substring((int)(line.Length / 2), (int)(line.Length / 2));

        foreach (char item in first)
        {
            duplicates.Add(item);
        }
        foreach (char item in second)
        {
            if (duplicates.Contains(item))
            {
                duplicate = item;
                break;
            }
        }

        return duplicate;
    }

    private static int GetItemPriority(char duplicate)
    {
        int duplicatePriority = duplicate;
        if (duplicatePriority != 0)
        {
            if ((duplicatePriority - 96) < 0)
                duplicatePriority = duplicatePriority - 38;
            else
                duplicatePriority -= 96;
        }
        else
            throw new Exception("Something went wrong");
        return duplicatePriority;
    }

    public void SecondChallenge(string[] lines)
    {
        int totalPriority = 0, counter = 0;
        HashSet<char> duplicates = new HashSet<char>();

        foreach (string line in lines)
        {
            counter++;
            HashSet<char> subDuplicates = new HashSet<char>();
            
            Boolean firstLine = duplicates.Count == 0;

            foreach(char item in line)
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

            if (counter > 2)
            {
                counter = 0;
                totalPriority += GetItemPriority(duplicates.Single());
                subDuplicates.Clear();
                duplicates.Clear();
            }
        }

        Console.WriteLine(totalPriority);
    }
}
