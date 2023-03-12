namespace WhimFights.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using WhimFights.UseCases;
    using WhimFights.UseCases.Infrastructure;
    using WhimFights.UseCases.Ports;

    public class Sut
    {
        private readonly SaveCharacterCommand.IHandler saveCharacterCommandHandler;
        private readonly GetAllCharactersQuery.IHandler getAllCharactersQueryHandler;
        private readonly StatisticsQuery.IHandler statisticsQueryHandler;

        private Sut(
            SaveCharacterCommand.IHandler saveCharacterCommandHandler,
            GetAllCharactersQuery.IHandler getAllCharactersQueryHandler,
            StatisticsQuery.IHandler statisticsQueryHandler)
        {
            this.saveCharacterCommandHandler = saveCharacterCommandHandler;
            this.getAllCharactersQueryHandler = getAllCharactersQueryHandler;
            this.statisticsQueryHandler = statisticsQueryHandler;
        }

        public List<IResponce> Responces { get; set; } = new List<IResponce>();

        public static Sut Create(
            IDice? dice = null)
        {
            var filename = Guid.NewGuid().ToString();
            using var fs = File.Create(filename);
            var characterMapper = new CharacterMapper(
                filePath: filename);
            return new Sut(
                saveCharacterCommandHandler: new SaveCharacterCommandHandler(
                    characterMapper: characterMapper),
                getAllCharactersQueryHandler: new GetAllCharactersQueryHandler(
                    characterMapper: characterMapper),
                statisticsQueryHandler: new StatisticsQueryHandler(
                    dice: dice ?? new Dice()));
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
                    await this.saveCharacterCommandHandler
                        .Handle(
                            command: new SaveCharacterCommand(
                                character: saveCharacter.Character))
                        .ConfigureAwait(false);
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
                    var characters = await this.getAllCharactersQueryHandler
                        .Handle(
                            query: new GetAllCharactersQuery())
                        .ConfigureAwait(false);

                    this.Responces.Add(
                        item: new ReceivedCharacters(
                            Characters: characters));
                    break;
            }
        }
    }
}