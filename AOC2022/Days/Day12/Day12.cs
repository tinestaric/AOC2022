using AOC2022.Days.Day12;
using System;
using System.Globalization;

public class Day12 : IDay
{
    private static int maxX { get; set; }
    private static int maxY { get; set; }
    private static int[,] map { get; set; }
    public void FirstChallenge(string[] lines)
    {
        int startX, startY, endX, endY;
        CreateMap(lines, out startX, out startY, out endX, out endY);

        var x = AStar<Node>.PerformSearch(
                new List<Node> { new Node { X = startX, Y = startY } },
                (node) => $"{node.X}|{node.Y}",
                (node) => node.X != endX || node.Y != endY,
                (node) => GetNeighbors(node),
                (_, _) => 1,
                (node) => (ulong)Math.Abs(endX - node.X) + (ulong)Math.Abs(endY - node.Y),
                out var distance
            );

        Console.WriteLine(distance);
    }

    private static void CreateMap(string[] lines, out int startX, out int startY, out int endX, out int endY)
    {
        map = new int[lines[0].Length, lines.Length];
        maxX = lines[0].Length;
        maxY = lines.Length;
        (startX, startY) = (0, 0);
        (endX, endY) = (0, 0);
        for (var j = 0; j < lines.Length; j++)
        {
            for (var i = 0; i < lines[0].Length; ++i)
            {
                if (lines[j][i] == 'S')
                {
                    map[i, j] = 0;
                    (startX, startY) = (i, j);
                    continue;
                }
                if (lines[j][i] == 'E')
                {
                    map[i, j] = 'z' - 'a';
                    (endX, endY) = (i, j);
                    continue;
                }
                map[i, j] = lines[j][i] - 'a';
            }
        }
    }

    private static List<Node> GetNeighbors(Node node)
    {
        var neighbors = new List<Node>();
        if (node.X > 0 && map[node.X - 1, node.Y] <= map[node.X, node.Y] + 1)
        {
            neighbors.Add(new Node { X = node.X - 1, Y = node.Y });
        }
        if (node.X < maxX - 1 && map[node.X + 1, node.Y] <= map[node.X, node.Y] + 1)
        {
            neighbors.Add(new Node { X = node.X + 1, Y = node.Y });
        }
        if (node.Y > 0 && map[node.X, node.Y - 1] <= map[node.X, node.Y] + 1)
        {
            neighbors.Add(new Node { X = node.X, Y = node.Y - 1 });
        }
        if (node.Y < maxY - 1 && map[node.X, node.Y + 1] <= map[node.X, node.Y] + 1)
        {
            neighbors.Add(new Node { X = node.X, Y = node.Y + 1 });
        }

        return neighbors;
    }

    private class Node : PriorityQueueNode
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public void SecondChallenge(string[] lines)
    {
        int endX, endY;
        List<(int X, int Y)> startPositions;
        CreateMap(lines, out endX, out endY, out startPositions);

        var minDistance = ulong.MaxValue;

        foreach (var (startX, startY) in startPositions)
        {
            var x = AStar<Node>.PerformSearch(
                    new List<Node> { new Node { X = startX, Y = startY } },
                    (node) => $"{node.X}|{node.Y}",
                    (node) => node.X != endX || node.Y != endY,
                    (node) => GetNeighbors(node),
                    (_, _) => 1,
                    (node) => (ulong)Math.Abs(endX - node.X) + (ulong)Math.Abs(endY - node.Y),
                    out var distance
                );
            minDistance = Math.Min(minDistance, distance);
        }

        Console.WriteLine(minDistance);
    }

    private static void CreateMap(string[] lines, out int endX, out int endY, out List<(int X, int Y)> startPositions)
    {
        map = new int[lines[0].Length, lines.Length];
        maxX = lines[0].Length;
        maxY = lines.Length;
        (endX, endY) = (0, 0);
        startPositions = new List<(int X, int Y)>();
        for (var j = 0; j < lines.Length; j++)
        {
            for (var i = 0; i < lines[0].Length; ++i)
            {
                if (lines[j][i] == 'S')
                {
                    map[i, j] = 0;
                    startPositions.Add((i, j));
                    continue;
                }
                if (lines[j][i] == 'E')
                {
                    map[i, j] = 'z' - 'a';
                    (endX, endY) = (i, j);
                    continue;
                }
                if (lines[j][i] == 'a')
                {
                    startPositions.Add((i, j));
                }
                map[i, j] = lines[j][i] - 'a';
            }
        }
    }
}
