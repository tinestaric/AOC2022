using System;
using System.Text.Json.Nodes;

public class Day13 : IDay
{
    public void FirstChallenge(string[] lines)
    {
        string[] pair = new string[2];
        int pairIndex = 1, lineIndex = 0;
        int correctPairs = 0;

        foreach (string line in lines)
        {
            if (line == "")
            {
                var jsonLeft = JsonNode.Parse(pair[0]);
                var jsonRight = JsonNode.Parse(pair[1]);
                var isCorrect = Compare(jsonLeft, jsonRight);
                if (isCorrect == true) correctPairs += pairIndex;

                lineIndex = 0;
                pairIndex++;
            }
            else
            {
                pair[lineIndex] = line;
                lineIndex++;
            }
        }

        Console.WriteLine(correctPairs);
    }

    public void SecondChallenge(string[] lines)
    {
        var allPackets = lines.Where(l => !string.IsNullOrEmpty(l)).Select(l => JsonNode.Parse(l)).ToList();
        var x = JsonNode.Parse("[[2]]");
        var y = JsonNode.Parse("[[6]]");

        allPackets.Add(x);
        allPackets.Add(y);

        allPackets.Sort((left, right) => Compare(left, right) == true ? -1 : 1);

        Console.WriteLine($"Part 2: {(allPackets.IndexOf(x) + 1) * (allPackets.IndexOf(y) + 1)}");
    }

    bool? Compare(JsonNode left, JsonNode right)
    {
        if (left is JsonValue leftVal && right is JsonValue rightVal)
        {
            var leftInt = leftVal.GetValue<int>();
            var rightInt = rightVal.GetValue<int>();
            return leftInt == rightInt ? null : leftInt < rightInt;
        }

        if (left is not JsonArray leftArray) leftArray = new JsonArray(left.GetValue<int>());
        if (right is not JsonArray rightArray) rightArray = new JsonArray(right.GetValue<int>());

        for (var i = 0; i < Math.Min(leftArray.Count, rightArray.Count); i++)
        {
            var res = Compare(leftArray[i], rightArray[i]);
            if (res.HasValue) { return res.Value; }
        }

        if (leftArray.Count < rightArray.Count) { return true; }
        if (leftArray.Count > rightArray.Count) { return false; }

        return null;
    }
}
