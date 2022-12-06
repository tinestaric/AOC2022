using System;

public class Day06 : IDay
{
    public void FirstChallenge(string[] lines)
    {
        foreach (string line in lines)
        {
            GetMarkerPosition(line, 4);
        }

        Console.WriteLine();
    }

    private static void GetMarkerPosition(string line, int markerLength)
    {
        int markerPosition = 0;
        HashSet<char> markerDuplicate = new HashSet<char>();
        for (int i = 0; i + markerLength -1 < line.Length; i++)
        {
            string marker = line.Substring(i, markerLength);
            foreach (char c in marker)
            {
                if (markerDuplicate.Contains(c))
                {
                    markerDuplicate.Clear();
                    break;
                }
                else
                {
                    markerDuplicate.Add(c);
                }
            }
            if (markerDuplicate.Count != 0)
            {
                markerPosition = i + markerLength;
                break;
            }
        }
        Console.WriteLine(markerPosition);
    }

    public void SecondChallenge(string[] lines)
    {
        foreach (string line in lines)
        {
            GetMarkerPosition(line, 14);
        }

        Console.WriteLine();
    }
}
