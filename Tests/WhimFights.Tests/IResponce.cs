namespace WhimFights.Tests
{
    public interface IResponce
    {
    }

    public record FightStatistics(
        Statistics Statistics) : IResponce;

    public record ReceivedCharacter(
        WhimFights.Character Character) : IResponce;
}