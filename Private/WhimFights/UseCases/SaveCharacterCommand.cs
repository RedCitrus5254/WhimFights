namespace WhimFights.UseCases
{
    public class SaveCharacterCommand
    {
        public SaveCharacterCommand(
            Character character)
        {
            this.Character = character;
        }

        public interface IHandler
        {
            void Handle(
                SaveCharacterCommand command);
        }

        public Character Character { get; }
    }
}