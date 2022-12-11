namespace WhimFights.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;
    using WhimFights.Tests.Fakes;
    using Xunit;

    public class SequencesTests
    {
        [Fact]
        public async Task S1()
        {
            var random = new Random();

            var attacker = ObjectsGen.RandomCharacter(
                prowess: random.Next(5, 10),
                slyness: random.Next(1, 4));
            var defender = ObjectsGen.RandomCharacter(
                prowess: random.Next(5, 10),
                slyness: random.Next(1, 4));

            var sut = Sut.Create(
                dice: new FakeDice(
                    predefinedValue: random.Next(1, 6)));

            var countOfFights = random.Next(1, 2);

            await sut.AcceptStimuliAsync(
                stimuli: new List<IStimulus>()
                {
                    new GetFightResult(
                        Attacker: attacker,
                        Defender: defender,
                        FightsCount: countOfFights),
                });

            var statistics = (FightStatistics)sut.Responces.First();

            var actualVictoriesCount = statistics.Statistics.FirstFighterStatistics.Victories + statistics.Statistics.SecondFighterStatistics.Victories;

            actualVictoriesCount
                .Should()
                .Be(countOfFights);
        }

        [Fact]
        public async Task S2()
        {
            var expected = ObjectsGen.RandomCharacter();

            var sut = Sut.Create();

            await sut.AcceptStimuliAsync(
                stimuli: new List<IStimulus>()
                {
                    new SaveCharacter(
                        Character: expected),
                    new GetCharacter(
                        CharacterId: expected.Id),
                });

            sut.Responces
                .FirstOrDefault()
                .Should()
                .BeEquivalentTo(
                    expectation: new ReceivedCharacter(
                        Character: expected));
        }

        [Fact]
        public async Task S3()
        {
            var character = ObjectsGen.RandomCharacter();

            var expected = ObjectsGen.RandomCharacter(
                id: character.Id);

            var sut = Sut.Create();

            await sut.AcceptStimuliAsync(
                stimuli: new List<IStimulus>()
                {
                    new SaveCharacter(
                        Character: character),
                    new SaveCharacter(
                        Character: expected),
                    new GetCharacter(
                        CharacterId: expected.Id),
                });

            sut.Responces
                .FirstOrDefault()
                .Should()
                .BeEquivalentTo(
                    expectation: new ReceivedCharacter(
                    Character: expected));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task S4Async(
            bool attackerHasSupport)
        {
            var random = new Random();

            var attacker = ObjectsGen.RandomCharacter(
                prowess: 5,
                slyness: 5,
                overconfidence: 1,
                hp: 1,
                hasSupport: attackerHasSupport);
            var defender = ObjectsGen.RandomCharacter(
                prowess: 6,
                slyness: 6,
                flair: 999,
                hp: 1);

            var sut = Sut.Create(
                dice: new FakeDice(
                    predefinedValue: random.Next(1, 6)));

            await sut.AcceptStimuliAsync(
                stimuli: new List<IStimulus>()
                {
                    new GetFightResult(
                        Attacker: attacker,
                        Defender: defender,
                        FightsCount: 1),
                });

            var expectedAttackerWins = attackerHasSupport
                ? 1
                : 0;

            var statistics = (FightStatistics)sut.Responces
                .First();

            statistics
                .Statistics
                .FirstFighterStatistics
                .Victories
                .Should()
                .Be(expectedAttackerWins);
        }

        [Fact]
        public async Task S5()
        {
            var firstCharacter = ObjectsGen.RandomCharacter();
            var secondCharacter = ObjectsGen.RandomCharacter();

            var sut = Sut.Create();

            await sut.AcceptStimuliAsync(
                stimuli: new List<IStimulus>()
                {
                    new SaveCharacter(
                        Character: firstCharacter),
                    new SaveCharacter(
                        Character: secondCharacter),
                    new GetAllCharacters(),
                });

            var expected = new ReceivedCharacters(
                new List<Character>()
                {
                    firstCharacter,
                    secondCharacter,
                });

            sut
                .Responces
                .First()
                .Should()
                .BeEquivalentTo(
                    expectation: expected);
        }
    }
}