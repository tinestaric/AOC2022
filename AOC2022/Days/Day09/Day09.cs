using System;

public class Day09 : IDay
{
    public void FirstChallenge(string[] lines)
    {
        HashSet<string> visitedPlaces = new HashSet<string>();
        (int, int) head = (0, 0), tail = (0, 0);

        foreach (string line in lines)
        {
            string[] splitLine = line.Split(' ');
            string direction = splitLine[0];
            int distance = Int32.Parse(splitLine[1]);

            visitedPlaces.Add("0-0");

            for (int i = 0; i < distance; i++)
            {
                head = MoveHead(head, direction);
                tail = MoveTail(head, tail);

                visitedPlaces.Add(tail.Item1 + "-" + tail.Item2);
            }
        }
        Console.WriteLine(visitedPlaces.Count);
    }

    private static (int, int) MoveTail((int, int) head, (int, int) tail)
    {
        if (Math.Abs(head.Item1 - tail.Item1) > 1 || Math.Abs(head.Item2 - tail.Item2) > 1)
        {
            if (head.Item1 != tail.Item1 && head.Item2 != tail.Item2)
            {
                if(Math.Abs(head.Item1 - tail.Item1) > 1)
                {
                    tail.Item2 = head.Item2;
                    tail.Item1 += Sign(head.Item1 - tail.Item1);
                }

                if (Math.Abs(head.Item2 - tail.Item2) > 1)
                {
                    tail.Item1 = head.Item1;
                    tail.Item2 += Sign(head.Item2 - tail.Item2);
                }                
            }
            else if (head.Item1 != tail.Item1)
            {
                if (head.Item1 > tail.Item1)
                    tail.Item1++;
                else
                    tail.Item1--;
            }
            else
            {
                if (head.Item2 > tail.Item2)
                    tail.Item2++;
                else
                    tail.Item2--;
            }
        }

        return tail;
    }

    private static int Sign(int v)
    {
        if (v > 0)
            return 1;
        if (v < 0)
            return -1;

        return 0;
    }

    private static (int, int) MoveHead((int, int) head, string direction)
    {
        switch (direction)
        {
            case "R":
                head.Item2++;
                break;
            case "L":
                head.Item2--;
                break;
            case "U":
                head.Item1++;
                break;
            case "D":
                head.Item1--;
                break;
        }

        return head;
    }

    public void SecondChallenge(string[] lines)
    {
        const int knots = 10;
        (int X, int Y)[] tails = new (int X, int Y)[knots];
        HashSet<(int X, int Y)> visited = new HashSet<(int X, int Y)>();

        foreach(string line in lines)
        {
            var cmd = line.Split(' ');
            int move = int.Parse(cmd[1]);
            for (int j = 0; j < move; j++)
            {
                tails[0] = MoveHead(tails[0], cmd[0]);

                for (int k = 1; k < knots; k++)
                {
                    if (Math.Abs(tails[k - 1].X - tails[k].X) > 1 || Math.Abs(tails[k - 1].Y - tails[k].Y) > 1)
                    {
                        tails[k].X += Math.Min(Math.Max(-1, tails[k - 1].X - tails[k].X), 1);
                        tails[k].Y += Math.Min(Math.Max(-1, tails[k - 1].Y - tails[k].Y), 1);
                    }
                }
                
                visited.Add(tails[knots - 1]);                
            }
        }
        
        Console.WriteLine(visited.Count);
    }
}
