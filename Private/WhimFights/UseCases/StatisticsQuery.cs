namespace WhimFights.UseCases
{
    using System.Threading.Tasks;

    public class StatisticsQuery
    {
        public StatisticsQuery(
            Character attaker,
            Character defender,
            int fightsCount)
        {
            this.Defender = defender;
            this.FightsCount = fightsCount;
            this.Attaker = attaker;
        }

        public interface IHandler
        {
            Task<Statistics> HandleAsync(
                StatisticsQuery query);
        }

        public Character Defender { get; }

        public int FightsCount { get; }

        public Character Attaker { get; }
    }
}