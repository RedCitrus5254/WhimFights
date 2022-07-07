namespace WhimFights.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using WhimFights.UseCases;
    using Xunit;

    public class SequencesTests
    {
        [Fact]
        public void S1()
        {
            var random = new Random();

            var firstCharacter = ObjectsGen.RandomCharacter();
            var secondCharacter = ObjectsGen.RandomCharacter();

            var sut = new Sut();

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
        }

        [Fact]
        public void S3()
        {
        }

        [Fact]
        public void S4()
        {
        }
    }
}