namespace WhimFights.UseCases
{
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

        public void Handle(
            SaveCharacterCommand command)
        {
            this.characterMapper
                .Save(
                    character: command.Character);
        }
    }
}