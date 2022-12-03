using System;

public class Day01 : IDay
{
    public void FirstChallenge(string[] lines)
    {
        long maxCalories = 0, elfCalories = 0;
        foreach (string line in lines)
        {
            if (line == "")
            {
                if (elfCalories > maxCalories)
                    maxCalories = elfCalories;

                elfCalories = 0;
                continue;
            }

            long calorieLine = Int64.Parse(line);
            elfCalories += calorieLine;
        }

        Console.WriteLine(maxCalories);
    }

    public void SecondChallenge(string[] lines)
    {
        long[] maxCalories = new long[3] { 0, 0, 0 };
        long elfCalories = 0;
        foreach (string line in lines)
        {
            if (line == "")
            {
                StoreMaxCalories(maxCalories, elfCalories);

                elfCalories = 0;
                continue;
            }

            long calorieLine = Int64.Parse(line);
            elfCalories += calorieLine;
        }
        if (elfCalories != 0)
            StoreMaxCalories(maxCalories, elfCalories);

        Console.WriteLine(maxCalories.Sum(x => x));
    }

    private static void StoreMaxCalories(long[] maxCalories, long elfCalories)
    {
        if (elfCalories > maxCalories[0])
        {
            if (elfCalories > maxCalories[1])
            {
                if (elfCalories > maxCalories[2])
                {
                    maxCalories[0] = maxCalories[1];
                    maxCalories[1] = maxCalories[2];
                    maxCalories[2] = elfCalories;
                }
                else
                {
                    maxCalories[0] = maxCalories[1];
                    maxCalories[1] = elfCalories;
                }
            }
            else
            {
                maxCalories[0] = elfCalories;
            }
        }
    }
}
