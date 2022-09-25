namespace WhimFights
{
    public class FighterStatistics
    {
        public FighterStatistics(
            string fighterId,
            int victories,
            int averageHealth)
        {
            this.FighterId = fighterId;
            this.Victories = victories;
            this.AverageHealth = averageHealth;
        }

        public string FighterId { get; }

        public int Victories { get; }

        public int AverageHealth { get; }
    }
}