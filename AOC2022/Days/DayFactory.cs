class DayFactory
{
    public IDay GetDaillyChallenge(int CustomDay)
    {
        int day = DateTime.Now.Day;

        if (CustomDay != 0)
            day = CustomDay;

        switch (day)
        {
            case 1:
                return new Day01();
            case 2:
                return new Day02();
            case 3:
                return new Day03();
        }

        throw new Exception("Cannnot parse correct daily challenge.");
    }
}