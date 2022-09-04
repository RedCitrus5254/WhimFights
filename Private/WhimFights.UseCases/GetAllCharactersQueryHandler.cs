namespace WhimFights.UseCases
{
    using System.Collections.Generic;
    using WhimFights.UseCases.Ports;

    public class GetAllCharactersQueryHandler : GetAllCharactersQuery.IHandler
    {
        private readonly ICharacterMapper characterMapper;

        public GetAllCharactersQueryHandler(
            ICharacterMapper characterMapper)
        {
            this.characterMapper = characterMapper;
        }

        public List<Character> Handle(
            GetAllCharactersQuery query)
        {
            return this.characterMapper
                .GetAll();
        }
    }
}