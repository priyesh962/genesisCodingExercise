internal class Program
{
    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("weather.dat");
        decimal smallRange = int.MaxValue;
        int smallday = 0;
        foreach (string line in lines)
        {
            List<string> test = GetRowValues(line);

            if (test.Any() && int.TryParse(test[0], out int day))
            {
                decimal range = decimal.Parse(test[1].Replace("*", "")) - decimal.Parse(test[2].Replace("*", ""));
                if (range < smallRange)
                {
                    smallRange = range;
                    smallday = day;
                }
            }

        }
        Console.WriteLine("day number with the smallest temperature spread: " + smallday);

        string[] footballLines = File.ReadAllLines("football.dat");
        decimal smallGoalGap = int.MaxValue;
        string smallgapTeam = string.Empty;
        foreach (string line in footballLines)
        {
            List<string> test = GetRowValues(line);

            if (test.Any() && test.Count > 1 && int.TryParse(test[6], out int forGoal) && test[7] == "-")
            {
                int againstGoal = int.Parse(test[8]);
                int range;
                if (forGoal > againstGoal) range = forGoal - againstGoal;
                else if (againstGoal > forGoal) range = againstGoal - forGoal;
                else range = 0;

                if (range < smallGoalGap)
                {
                    smallGoalGap = range;
                    smallgapTeam = test[1];
                }
            }
        }

        Console.WriteLine(" name of the team with the smallest difference in ‘for’ and ‘against’ goals: " + smallgapTeam);
    }

    private static List<string> GetRowValues(string line)
    {
        var test = line.Split(" ").ToList();
        test.RemoveAll(x => string.IsNullOrWhiteSpace(x));
        return test;
    }
}