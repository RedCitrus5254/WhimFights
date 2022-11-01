namespace WhimFights.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WhimFights.UseCases;
    using WhimFights.UseCases.Infrastructure;

    public class Sut
    {
        private readonly SaveCharacterCommand.IHandler saveCharacterCommandHandler;
        private readonly GetCharacterQuery.IHandler getCharacterQueryHandler;
        private readonly GetAllCharactersQuery.IHandler getAllCharactersQueryHandler;
        private readonly StatisticsQuery.IHandler statisticsQueryHandler;

        private Sut(
            SaveCharacterCommand.IHandler saveCharacterCommandHandler,
            GetCharacterQuery.IHandler getCharacterQueryHandler,
            GetAllCharactersQuery.IHandler getAllCharactersQueryHandler,
            StatisticsQuery.IHandler statisticsQueryHandler)
        {
            this.saveCharacterCommandHandler = saveCharacterCommandHandler;
            this.getCharacterQueryHandler = getCharacterQueryHandler;
            this.getAllCharactersQueryHandler = getAllCharactersQueryHandler;
            this.statisticsQueryHandler = statisticsQueryHandler;
        }

        public List<IResponce> Responces { get; set; } = new List<IResponce>();

        public static Sut Create()
        {
            var characterMapper = new CharacterMapper();
            return new Sut(
                saveCharacterCommandHandler: new SaveCharacterCommandHandler(
                    characterMapper: characterMapper),
                getCharacterQueryHandler: new GetCharacterQueryHandler(
                    characterMapper: characterMapper),
                getAllCharactersQueryHandler: new GetAllCharactersQueryHandler(
                    characterMapper: characterMapper),
                statisticsQueryHandler: new StatisticsQueryHandler());
        }

        public async Task AcceptStimuliAsync(
            List<IStimulus> stimuli)
        {
            foreach (var stimulus in stimuli)
            {
                await this.AcceptStimulusAsync(
                    stimulus: stimulus)
                    .ConfigureAwait(false);
            }
        }

        private async Task AcceptStimulusAsync(
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

                case GetFightResult getFightResult:
                    var statistics = await this.statisticsQueryHandler
                        .HandleAsync(
                            query: new StatisticsQuery(
                                attaker: getFightResult.Attacker,
                                defender: getFightResult.Defender,
                                fightsCount: getFightResult.FightsCount))
                        .ConfigureAwait(false);

                    this.Responces.Add(new FightStatistics(
                        Statistics: statistics));
                    break;

                case GetAllCharacters:
                    var characters = this.getAllCharactersQueryHandler
                        .Handle(
                            query: new GetAllCharactersQuery());

                    this.Responces.Add(
                        item: new ReceivedCharacters(
                            Characters: characters));
                    break;
            }
        }
    }
}