namespace WhimFights.UseCases
{
    using System.Threading.Tasks;
    using WhimFights.UseCases.Ports;

    public class SaveCharacterCommandHandler
        : SaveCharacterCommand.IHandler
    {
        private readonly ICharacterMapper characterMapper;

        public SaveCharacterCommandHandler(
            ICharacterMapper characterMapper)
        {
            this.characterMapper = characterMapper;
        }

        public Task Handle(
            SaveCharacterCommand command)
        {
            return this.characterMapper
                .SaveOneAsync(
                    character: command.Character);
        }
    }
}