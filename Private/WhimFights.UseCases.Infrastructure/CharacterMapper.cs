namespace WhimFights.UseCases.Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;
    using WhimFights.UseCases.Ports;

    public class CharacterMapper : ICharacterMapper
    {
        private readonly List<Character> characters = new List<Character>();

        public Character Get(
            string id)
        {
            return this.characters.First(character => character.Id == id);
        }

        public void Save(
            Character character)
        {
            this.characters.Add(character);
        }
    }
}