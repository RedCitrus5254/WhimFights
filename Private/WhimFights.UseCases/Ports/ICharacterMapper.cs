namespace WhimFights.UseCases.Ports
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICharacterMapper
    {
        Task SaveAsync(
            Character character);

        Task<Character> GetAsync(
            string id);

        Task<List<Character>> GetAllAsync();
    }
}