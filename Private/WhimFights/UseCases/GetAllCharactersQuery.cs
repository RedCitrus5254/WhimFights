namespace WhimFights.UseCases
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class GetAllCharactersQuery
    {
        public interface IHandler
        {
            Task<List<Character>> Handle(
                GetAllCharactersQuery query);
        }
    }
}