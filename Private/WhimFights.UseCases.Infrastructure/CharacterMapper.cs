namespace WhimFights.UseCases.Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;
    using WhimFights.UseCases.Ports;

    public class CharacterMapper : ICharacterMapper
    {
        private readonly Dictionary<string, Character> characters = new Dictionary<string, Character>();

        public Character Get(
            string id)
        {
            return this.characters[id];
        }

        public List<Character> GetAll()
        {
            return this.characters.Values.ToList();
        }

        public void Save(
            Character character)
        {
            this.characters[character.Id] = character;
        }
    }
}