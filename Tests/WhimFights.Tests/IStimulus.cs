namespace WhimFights.Tests
{
    public interface IStimulus
    {
    }

    public record SaveCharacter(
        Character Character) : IStimulus;

    public record ChangeCharacter(
        Character Character) : IStimulus;

    public record GetCharacter(
        string CharacterId) : IStimulus;

    public record GetFightResult(
        Character Attacker,
        Character Defender,
        int FightsCount) : IStimulus;

    public record GetAllCharacters() : IStimulus;
}