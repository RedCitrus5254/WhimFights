namespace WhimFights.UseCases
{
    using WhimFights.UseCases.Ports;

    public class GetCharacterQueryHandler : GetCharacterQuery.IHandler
    {
        private readonly ICharacterMapper characterMapper;

        public GetCharacterQueryHandler(
            ICharacterMapper characterMapper)
        {
            this.characterMapper = characterMapper;
        }

        public Character Handle(
            GetCharacterQuery query)
        {
            return this.characterMapper.Get(query.Id);
        }
    }
}
