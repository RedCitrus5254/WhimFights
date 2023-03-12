namespace WhimFights.UseCases.Ports
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICharacterMapper
    {
        Task SaveOneAsync(
            Character character);

        Task<List<Character>> GetAllAsync();
    }
}