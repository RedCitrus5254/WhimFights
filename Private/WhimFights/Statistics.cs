namespace WhimFights
{
    public class Statistics
    {
        public Statistics(
            int countOfFights,
            FighterStatistics firstFighterStatistics,
            FighterStatistics secondFighterStatistics)
        {
            this.CountOfFights = countOfFights;
            this.FirstFighterStatistics = firstFighterStatistics;
            this.SecondFighterStatistics = secondFighterStatistics;
        }

        public int CountOfFights { get; }

        public FighterStatistics FirstFighterStatistics { get; }

        public FighterStatistics SecondFighterStatistics { get; }
    }
}