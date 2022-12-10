using System;

public class Day10 : IDay
{
    public void FirstChallenge(string[] lines)
    {
        int X = 1, totalCycles = 0, totalSignalStrength = 0;
        int maxCycles = 220;
        int cycleToReport = 20;

        foreach (string line in lines)
        {
            int cycleCount = 0, value = 0;
            cycleCount = ParseCommand(line, ref value);

            for (int i = 0; i < cycleCount; i++)
            {
                totalCycles++;
                if (totalCycles == cycleToReport && totalCycles <= maxCycles)
                {
                    totalSignalStrength += (cycleToReport * X);
                    cycleToReport += 40;
                }
            }
            X += value;
        }

        Console.WriteLine(totalSignalStrength);
    }


    public void SecondChallenge(string[] lines)
    {
        int X = 1, totalCycles = 0;
        int crtCounter = 0, crtLineCounter = 0;
        char[][] crt =
        {
            new char[40],
            new char[40],
            new char[40],
            new char[40],
            new char[40],
            new char[40]
        };

        foreach (string line in lines)
        {
            int cycleCount = 0, value = 0;
            cycleCount = ParseCommand(line, ref value);

            for (int i = 0; i < cycleCount; i++)
            {
                if (crtLineCounter >= X - 1 && crtLineCounter <= X + 1)
                    crt[crtCounter][crtLineCounter] = '#';
                else
                    crt[crtCounter][crtLineCounter] = '.';

                totalCycles++;
                crtLineCounter++;
                if (totalCycles % 40 == 0)
                {
                    crtCounter++;
                    crtLineCounter = 0;
                }
            }
            X += value;
        }

        foreach (char[] crtLine in crt)
        {
            Console.WriteLine(crtLine);
        }
    }

    private static int ParseCommand(string line, ref int value)
    {
        int cycleCount;
        if (line == "noop")
        {
            cycleCount = 1;
        }
        else
        {
            string[] cmd = line.Split(' ');
            cycleCount = 2;
            value = Int32.Parse(cmd[1]);
        }

        return cycleCount;
    }
}
