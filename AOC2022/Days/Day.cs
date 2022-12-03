class DaillyChallenge
{
    private string[]? testLines, lines;

    public void ImportData(string day)
    {
        string example = $"../../..//Days/{day}/Example.txt";
        string input = $"../../..//Days/{day}/input.txt";

        testLines = File.ReadAllLines(example);
        lines = File.ReadAllLines(input);
    }

    public void Solve()
    {
        IDay day = new DayFactory().GetDaillyChallenge(0);

        Console.WriteLine($"Solving challenge for: {day.GetType().Name}");
        Console.WriteLine("");
        
        ImportData(day.GetType().Name);

        if ((testLines == null) || (lines == null))
            throw new Exception("Import data missing");
                
        Console.WriteLine("Example data");
        day.FirstChallenge(testLines);
        day.SecondChallenge(testLines);

        Console.WriteLine("");
        Console.WriteLine("Input data");
        day.FirstChallenge(lines);
        day.SecondChallenge(lines);
    }
}