namespace WhimFights.UseCases.Ports
{
    public interface ICharacterMapper
    {
        void Save(
            Character character);

        Character Get(
            string id);
    }
}