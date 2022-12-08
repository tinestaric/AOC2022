using System;

public class Day08 : IDay
{
    public void FirstChallenge(string[] lines)
    {
        int[,] forrest = new int[lines.Length, lines[0].Length];

        int totalVisibleTrees = lines.Length * 2 + lines[0].Length * 2 - 4;
        PopulateForrest(lines, forrest);

        for (int i = 1; i < lines.Length - 1; i++)
        {
            for (int j = 1; j < lines[i].Length - 1; j++)
            {
                if (IsTreeVisible(forrest, i, j))
                    totalVisibleTrees++;
            }
        }

        Console.WriteLine(totalVisibleTrees);
    }

    private static void PopulateForrest(string[] lines, int[,] forrest)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                forrest[i, j] = Int32.Parse(lines[i][j].ToString());
            }
        }
    }

    private bool IsTreeVisible(int[,] forrest, int i, int j)
    {
        int treeHeight = forrest[i, j];
        bool treeVisible = true;


        for (int k = i + 1; k < forrest.GetLength(0); k++)
        {
            if (forrest[k, j] >= treeHeight)
            {
                treeVisible = false;
                break;
            }
        }

        if (treeVisible)
            return true;

        treeVisible = true;

        for (int k = i - 1; k >= 0; k--)
        {
            if (forrest[k, j] >= treeHeight)
            {
                treeVisible = false;
                break;
            }
        }

        if (treeVisible)
            return true;

        treeVisible = true;

        for (int k = j + 1; k < forrest.GetLength(1); k++)
        {
            if (forrest[i, k] >= treeHeight)
            {
                treeVisible = false;
                break;
            }
        }

        if (treeVisible)
            return true;

        treeVisible = true;

        for (int k = j - 1; k >= 0; k--)
        {
            if (forrest[i, k] >= treeHeight)
            {
                treeVisible = false;
                break;
            }
        }

        return treeVisible;
    }

    public void SecondChallenge(string[] lines)
    {
        int[,] forrest = new int[lines.Length, lines[0].Length];

        int scenicScore = 0;

        PopulateForrest(lines, forrest);

        for (int i = 1; i < lines.Length - 1; i++)
        {
            for (int j = 1; j < lines[i].Length - 1; j++)
            {
                int treeScenicScore = GetScenicScore(forrest, i, j);

                if (treeScenicScore > scenicScore)
                    scenicScore = treeScenicScore;
            }
        }

        Console.WriteLine(scenicScore);
    }

    private int GetScenicScore(int[,] forrest, int i, int j)
    {
        int treeHeight = forrest[i, j];
        int scenicScore = 1, visibleTrees = 1;


        for (int k = i + 1; k < forrest.GetLength(0); k++)
        {
            if (forrest[k, j] >= treeHeight)
                break;

            if (k + 1 < forrest.GetLength(0))
                visibleTrees++;
        }

        scenicScore *= visibleTrees;
        visibleTrees = 1;

        for (int k = i - 1; k >= 0; k--)
        {
            if (forrest[k, j] >= treeHeight)
                break;

            if (k - 1 >= 0)
                visibleTrees++;
        }

        scenicScore *= visibleTrees;
        visibleTrees = 1;

        for (int k = j + 1; k < forrest.GetLength(1); k++)
        {
            if (forrest[i, k] >= treeHeight)
                break;

            if (k + 1 < forrest.GetLength(1))
                visibleTrees++;
        }

        scenicScore *= visibleTrees;
        visibleTrees = 1;

        for (int k = j - 1; k >= 0; k--)
        {
            if (forrest[i, k] >= treeHeight)
                break;

            if (k - 1 >= 0)
                visibleTrees++;
        }

        scenicScore *= visibleTrees;

        return scenicScore;
    }
}
