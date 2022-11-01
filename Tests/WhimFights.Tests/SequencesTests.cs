namespace WhimFights.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Xunit;

    public class SequencesTests
    {
        [Fact]
        public void S1()
        {
            var random = new Random();

            var firstCharacter = ObjectsGen.RandomCharacter();
            var secondCharacter = ObjectsGen.RandomCharacter();

            var sut = Sut.Create();

            var countOfFights = random.Next(1, 10);

            sut.AcceptStimuli(
                stimuli: new List<IStimulus>()
                {
                    new GetFightResult(
                        FirstCharacter: firstCharacter,
                        SecondCharacter: secondCharacter,
                        CountOfFights: countOfFights),
                });

            var expectation = new FightStatistics(
                Statistics: new Statistics(
                    countOfFights: countOfFights));

            sut
                .Responces
                .FirstOrDefault()
                .Should()
                .NotBeEquivalentTo(expectation);
        }

        [Fact]
        public void S2()
        {
            var expected = ObjectsGen.RandomCharacter();

            var sut = Sut.Create();

            sut.AcceptStimuli(
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
        public void S3()
        {
            var character = ObjectsGen.RandomCharacter();

            var expected = ObjectsGen.RandomCharacter(
                id: character.Id);

            var sut = Sut.Create();

            sut.AcceptStimuli(
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

        [Fact]
        public void S4()
        {
            var attackingCharacter = ObjectsGen.RandomCharacter(
                prowess: 5,
                slyness: 5,
                overconfidence: 1,
                hp: 1);
            var defendingCharacter = ObjectsGen.RandomCharacter(
                prowess: 6,
                slyness: 6,
                flair: 999,
                hp: 1);

            var sut = Sut.Create();

            sut.AcceptStimuli(
                stimuli: new List<IStimulus>()
                {
                    new GetFightResult(
                        FirstCharacter: attackingCharacter,
                        SecondCharacter: defendingCharacter,
                        CountOfFights: 1),
                });

            sut.Responces
                .FirstOrDefault()
                .Should()
                .NotBeEquivalentTo(
                    unexpected: new FightStatistics(
                        Statistics: new Statistics(
                        countOfFights: 1)));
        }

        [Fact]
        public void S5()
        {
            var firstCharacter = ObjectsGen.RandomCharacter();
            var secondCharacter = ObjectsGen.RandomCharacter();

            var sut = Sut.Create();

            sut.AcceptStimuli(
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