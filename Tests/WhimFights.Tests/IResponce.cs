namespace WhimFights.Tests
{
    public interface IResponce
    {
    }

    public record FightStatistics(
        Statistics Statistics) : IResponce;
}