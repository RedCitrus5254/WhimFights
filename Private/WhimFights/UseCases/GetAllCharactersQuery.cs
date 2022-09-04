namespace WhimFights.UseCases
{
    using System.Collections.Generic;

    public class GetAllCharactersQuery
    {
        public interface IHandler
        {
            List<Character> Handle(
                GetAllCharactersQuery query);
        }
    }
}