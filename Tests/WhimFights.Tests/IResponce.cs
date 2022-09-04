namespace WhimFights.Tests
{
    using System.Collections.Generic;

    public interface IResponce
    {
    }

    public record FightStatistics(
        Statistics Statistics) : IResponce;

    public record ReceivedCharacter(
        WhimFights.Character Character) : IResponce;

    public record ReceivedCharacters(
        List<Character> Characters) : IResponce;
}