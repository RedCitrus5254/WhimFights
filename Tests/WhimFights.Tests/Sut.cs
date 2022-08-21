namespace WhimFights.Tests
{
    using System.Collections.Generic;
    using WhimFights.UseCases;
    using WhimFights.UseCases.Infrastructure;

    public class Sut
    {
        private readonly SaveCharacterCommand.IHandler saveCharacterCommandHandler;

        private Sut(
            SaveCharacterCommand.IHandler saveCharacterCommandHandler)
        {
            this.saveCharacterCommandHandler = saveCharacterCommandHandler;
        }

        public List<IResponce> Responces { get; set; } = new List<IResponce>();

        public static Sut Create()
        {
            var characterMapper = new CharacterMapper();
            return new Sut(
                saveCharacterCommandHandler: new SaveCharacterCommandHandler(
                    characterMapper: characterMapper));
        }

        public void AcceptStimuli(
            List<IStimulus> stimuli)
        {
            foreach (var stimulus in stimuli)
            {
                this.AcceptStimulus(
                    stimulus: stimulus);
            }
        }

        private void AcceptStimulus(
            IStimulus stimulus)
        {
            switch (stimulus)
            {
                case SaveCharacter saveCharacter:
                    this.saveCharacterCommandHandler
                        .Handle(
                            command: new SaveCharacterCommand(
                                character: saveCharacter.Character));
                    break;
                case ChangeCharacter:
                    break;
                case GetCharacter:
                    break;
                case GetFightResult:
                    break;
                case GetAllCharacters:
                    break;
            }
        }
    }
}