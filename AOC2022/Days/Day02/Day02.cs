using System;

public class Day02 : IDay
{
    public void FirstChallenge(string[] lines)
    {
        int totalScore = 0;

        totalScore = 0;
        foreach (string line in lines)
        {
            if (line.Contains("X"))
                totalScore += 1;
            else if (line.Contains("Y"))
                totalScore += 2;
            else if (line.Contains("Z"))
                totalScore += 3;

            if (line.Contains("A X") || line.Contains("B Y") || line.Contains("C Z"))
                totalScore += 3;
            if (line.Contains("A Y") || line.Contains("B Z") || line.Contains("C X"))
                totalScore += 6;
        }

        Console.WriteLine(totalScore);
    }

    public void SecondChallenge(string[] lines)
    {
        int elfSelection, totalScore = 0;

        foreach (string line in lines)
        {
            elfSelection = GetElfSelection(line);

            if (line.Contains("Y"))
                totalScore += 3;
            else if (line.Contains("Z"))
            {
                totalScore += 6;
                elfSelection++;
            }
            else
                elfSelection--;

            if (elfSelection == 0)
                elfSelection = 3;
            else if (elfSelection == 4)
                elfSelection = 1;

            totalScore += elfSelection;            
        }

        Console.WriteLine(totalScore);
    }

    private static int GetElfSelection(string line)
    {
        switch (line[0])
        {
            case 'A':
                return 1;
            case 'B':
                return 2;
            case 'C':
                return 3;
        }
        throw new Exception("Weird Line");
    }
}
