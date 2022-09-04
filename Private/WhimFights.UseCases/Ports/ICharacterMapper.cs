namespace WhimFights.UseCases.Ports
{
    using System.Collections.Generic;

    public interface ICharacterMapper
    {
        void Save(
            Character character);

        Character Get(
            string id);

        List<Character> GetAll();
    }
}