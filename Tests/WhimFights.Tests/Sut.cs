namespace WhimFights.Tests
{
    using System.Collections.Generic;
    using WhimFights.UseCases;
    using WhimFights.UseCases.Infrastructure;

    public class Sut
    {
        private readonly SaveCharacterCommand.IHandler saveCharacterCommandHandler;

        private readonly GetCharacterQuery.IHandler getCharacterQueryHandler;

        private Sut(
            SaveCharacterCommand.IHandler saveCharacterCommandHandler,
            GetCharacterQuery.IHandler getCharacterQueryHandler)
        {
            this.saveCharacterCommandHandler = saveCharacterCommandHandler;
            this.getCharacterQueryHandler = getCharacterQueryHandler;
        }

        public List<IResponce> Responces { get; set; } = new List<IResponce>();

        public static Sut Create()
        {
            var characterMapper = new CharacterMapper();
            return new Sut(
                saveCharacterCommandHandler: new SaveCharacterCommandHandler(
                    characterMapper: characterMapper),
                getCharacterQueryHandler: new GetCharacterQueryHandler(
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

                case GetCharacter getCharacter:
                    var character = this.getCharacterQueryHandler
                                        .Handle(
                                            query: new GetCharacterQuery(
                                                id: getCharacter.CharacterId));
                    this.Responces.Add(new ReceivedCharacter(character));
                    break;

                case GetFightResult:
                    break;
                case GetAllCharacters:
                    break;
            }
        }
    }
}