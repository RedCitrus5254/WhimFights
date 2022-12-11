namespace WhimFights.UseCases
{
    using System.Threading.Tasks;
    using WhimFights.UseCases.Ports;

    public class GetCharacterQueryHandler : GetCharacterQuery.IHandler
    {
        private readonly ICharacterMapper characterMapper;

        public GetCharacterQueryHandler(
            ICharacterMapper characterMapper)
        {
            this.characterMapper = characterMapper;
        }

        public Task<Character> Handle(
            GetCharacterQuery query)
        {
            return this.characterMapper
                .GetAsync(query.Id);
        }
    }
}
