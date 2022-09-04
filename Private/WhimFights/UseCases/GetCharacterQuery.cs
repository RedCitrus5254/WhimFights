namespace WhimFights.UseCases
{
    public class GetCharacterQuery
    {
        public GetCharacterQuery(
            string id)
        {
            this.Id = id;
        }

        public interface IHandler
        {
            Character Handle(
                GetCharacterQuery query);
        }

        public string Id { get; }
    }
}
