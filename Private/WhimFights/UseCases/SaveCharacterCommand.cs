namespace WhimFights.UseCases
{
    using System.Threading.Tasks;

    public class SaveCharacterCommand
    {
        public SaveCharacterCommand(
            Character character)
        {
            this.Character = character;
        }

        public interface IHandler
        {
            Task Handle(
                SaveCharacterCommand command);
        }

        public Character Character { get; }
    }
}