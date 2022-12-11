using System;
using System.Text.RegularExpressions;


public class Day11 : IDay
{
    public void FirstChallenge(string[] lines)
    {
        Dictionary<int, Monkey> monkeys = ParseMonkeys(lines);
        int maxRounds = 20;

        for (int i = 0; i < maxRounds; i++)
        {
            foreach (Monkey monkey in monkeys.Values)
            {
                foreach (long item in monkey.Items)
                {
                    monkey.InspectedItemCount++;

                    long newWorryLevel = RunOperation(item, monkey.Operation, monkey.OperationValue);
                    newWorryLevel = Convert.ToInt64(Math.Round((decimal)newWorryLevel / 3, MidpointRounding.ToZero));
                    if (newWorryLevel % monkey.DivisibilityValue == 0)
                        monkeys[monkey.IfTrue].Items.Add(newWorryLevel);
                    else
                        monkeys[monkey.IfFalse].Items.Add(newWorryLevel);
                }
                monkey.Items.Clear();
            }
        }

        var inspectionCounts = monkeys.Values.Select(m => m.InspectedItemCount).ToList();
        var top2 = inspectionCounts.OrderByDescending(el => el).Take(2).ToList();

        Console.WriteLine(top2[0] * top2[1]);
    }

    private long RunOperation(long item, OperationType operation, int operationValue)
    {
        switch (operation)
        {
            case OperationType.Add:
                return item + operationValue;
            case OperationType.AddSelf:
                return item + item;
            case OperationType.Multiply:
                return item * operationValue;
            case OperationType.MultiplySelf:
                return item * item;
        }

        throw new Exception("Unknown operation");
    }

    private static Dictionary<int, Monkey> ParseMonkeys(string[] lines)
    {
        Dictionary<int, Monkey> monkeys = new Dictionary<int, Monkey>();
        Monkey currMonkey = new Monkey();
        foreach (string line in lines)
        {

            if (line.StartsWith("Monkey"))
            {
                currMonkey = new Monkey();
                monkeys.Add(int.Parse(line.Split(' ')[1][0].ToString()), currMonkey);
            }

            if (line.Contains("Starting items"))
            {
                string[] items = line.Split(':')[1].Split(',');
                foreach (string item in items)
                    currMonkey.Items.Add(int.Parse(item.Trim()));
            }

            if (line.Contains("Operation"))
            {
                SetMonkeyOperation(currMonkey, line);
            }

            if (line.Contains("Test"))
            {
                currMonkey.DivisibilityValue = int.Parse(Regex.Match(line, @"\d+$").Value);
            }

            if (line.Contains("If true"))
            {
                currMonkey.IfTrue = int.Parse(Regex.Match(line, @"\d+$").Value);
            }

            if (line.Contains("If false"))
            {
                currMonkey.IfFalse = int.Parse(Regex.Match(line, @"\d+$").Value);
            }
        }

        return monkeys;
    }

    private static void SetMonkeyOperation(Monkey currMonkey, string line)
    {
        string cmd = line.Split('=')[1].Trim();
        if (cmd.Contains('+'))
        {
            Match regExMatch = Regex.Match(line, @"\d+$");
            if (regExMatch.Success)
            {
                currMonkey.OperationValue = int.Parse(regExMatch.Value);
                currMonkey.Operation = OperationType.Add;
            }
            else
                currMonkey.Operation = OperationType.AddSelf;
        }
        else
        {
            Match regExMatch = Regex.Match(line, @"\d+$");
            if (regExMatch.Success)
            {
                currMonkey.OperationValue = int.Parse(regExMatch.Value);
                currMonkey.Operation = OperationType.Multiply;
            }
            else
                currMonkey.Operation = OperationType.MultiplySelf;
        }
    }

    public void SecondChallenge(string[] lines)
    {
        Dictionary<int, Monkey> monkeys = ParseMonkeys(lines);
        int maxRounds = 10000;

        int superMod = 1;

        foreach (Monkey monkey in monkeys.Values)
            superMod *= monkey.DivisibilityValue;

        for (int i = 0; i < maxRounds; i++)
        {
            foreach (Monkey monkey in monkeys.Values)
            {
                foreach (long item in monkey.Items)
                {
                    monkey.InspectedItemCount++;

                    long newWorryLevel = RunOperation(item, monkey.Operation, monkey.OperationValue);
                    newWorryLevel %= superMod;
                    if (newWorryLevel % monkey.DivisibilityValue == 0)
                        monkeys[monkey.IfTrue].Items.Add(newWorryLevel);
                    else
                        monkeys[monkey.IfFalse].Items.Add(newWorryLevel);
                }
                monkey.Items.Clear();
            }
        }
        long[] busiestMonkeys = new long[2] { 0, 0 };
        foreach (Monkey monkey in monkeys.Values)
        {
            if (monkey.InspectedItemCount > busiestMonkeys[0])
            {
                if (monkey.InspectedItemCount > busiestMonkeys[1])
                {
                    busiestMonkeys[0] = busiestMonkeys[1];
                    busiestMonkeys[1] = monkey.InspectedItemCount;
                }
                else
                    busiestMonkeys[0] = monkey.InspectedItemCount;
            }
        }

        Console.WriteLine(busiestMonkeys[0] * busiestMonkeys[1]);
    }

    class Monkey
    {
        public Monkey()
        {
            Items = new List<long>();
        }
        public List<long> Items { get; set; }
        public OperationType Operation { get; set; }
        public bool MultiplyOperation { get; set; }
        public int DivisibilityValue { get; set; }
        public int OperationValue { get; set; }
        public int IfTrue { get; set; }
        public int IfFalse { get; set; }
        public int InspectedItemCount { get; internal set; }
    }

    enum OperationType
    {
        Add,
        Multiply,
        MultiplySelf,
        AddSelf
    }
}
