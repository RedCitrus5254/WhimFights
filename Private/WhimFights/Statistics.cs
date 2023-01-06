namespace WhimFights
{
    public class Statistics
    {
        public Statistics(
            int countOfFights,
            FighterStatistics attackerStatistics,
            FighterStatistics defenderStatistics)
        {
            this.CountOfFights = countOfFights;
            this.AttackerStatistics = attackerStatistics;
            this.DefenderStatistics = defenderStatistics;
        }

        public int CountOfFights { get; }

        public FighterStatistics AttackerStatistics { get; }

        public FighterStatistics DefenderStatistics { get; }
    }
}