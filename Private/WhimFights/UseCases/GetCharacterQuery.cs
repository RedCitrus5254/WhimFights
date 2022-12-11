namespace WhimFights.UseCases
{
    using System.Threading.Tasks;

    public class GetCharacterQuery
    {
        public GetCharacterQuery(
            string id)
        {
            this.Id = id;
        }

        public interface IHandler
        {
            Task<Character> Handle(
                GetCharacterQuery query);
        }

        public string Id { get; }
    }
}
