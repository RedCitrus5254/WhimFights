namespace WhimFights.UseCases.Infrastructure.Tests;

using FluentAssertions;
using WhimFights.Testing;
using Xunit;

public class CharacterMapperTests
{
    private const string TestFilesDir = "testFiles";

    private static CharacterMapper CreateSut(
        string filePath)
    {
        return new CharacterMapper(
            filePath: filePath);
    }

    public class SaveOneAsyncTests : IDisposable
    {
        public SaveOneAsyncTests()
        {
            CopyDirectory(
                sourceDir: $"../../../{TestFilesDir}",
                destinationDir: $"./{TestFilesDir}");
        }

        public void Dispose()
        {
            if (Directory.Exists($"./{TestFilesDir}"))
            {
                Directory.Delete($"./{TestFilesDir}", true);
            }
        }

        [Fact]
        public async Task SavesNewCharacterAsync()
        {
            var filePath = $"./{TestFilesDir}/SavesNewCharacter";

            var sut = CreateSut(filePath);

            var character = ObjectsGen.RandomPlayerCharacter();

            await sut.SaveOneAsync(
                character: character);

            var expectedCharacter = $"{character.Id},{character.Prowess},{character.Flair}," +
                $"{character.Slyness},{character.Overconfidence},{character.Hp},False";

            var file = await File.ReadAllTextAsync(
                path: filePath);

            file
                .Should()
                .Contain(expectedCharacter);
        }

        [Fact]
        public async Task SavesExistingCharacterAsync()
        {
            var filePath = $"./{TestFilesDir}/SavesExistingCharacter";

            var sut = CreateSut(filePath);

            var character = ObjectsGen.RandomPlayerCharacter(
                id: "Фармацевт");

            await sut.SaveOneAsync(
                character: character);

            var expectedCharacter = $"{character.Id},{character.Prowess},{character.Flair}," +
                $"{character.Slyness},{character.Overconfidence},{character.Hp},False";

            var file = await File.ReadAllTextAsync(
                path: filePath);

            file
                .Should()
                .Contain(expectedCharacter);
        }

        private static void CopyDirectory(
            string sourceDir,
            string destinationDir)
        {
            var dir = new DirectoryInfo(sourceDir);

            DirectoryInfo[] dirs = dir.GetDirectories();

            Directory.CreateDirectory(destinationDir);

            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath);
            }

            foreach (DirectoryInfo subDir in dirs)
            {
                string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                CopyDirectory(subDir.FullName, newDestinationDir);
            }
        }
    }

    public class GetAllAsyncTests : IDisposable
    {
        public GetAllAsyncTests()
        {
            Directory.CreateDirectory(
                path: $"./{TestFilesDir}");
        }

        public void Dispose()
        {
            if (Directory.Exists($"./{TestFilesDir}"))
            {
                Directory.Delete($"./{TestFilesDir}", true);
            }
        }

        [Fact]
        public async Task GetAllCorrectlyAsync()
        {
            var filePath = $"./{TestFilesDir}/GetAllCorrectly";
            var textCharacters = $"Фармацевт,11,14,14,11,15,False{Environment.NewLine}Председатель,12,13,12,13,15,True";
            var sut = CreateSut(
                filePath: filePath);

            File.Create(filePath).Dispose();
            File.WriteAllText(filePath, textCharacters);

            var expected = new List<Character>()
            {
                new PlayerCharacter(
                    id: "Фармацевт",
                    prowess: 11,
                    flair: 14,
                    slyness: 14,
                    overconfidence: 11,
                    hp: 15,
                    hasSupport: false),
                new NPC(
                    id: "Председатель",
                    prowess: 12,
                    flair: 13,
                    slyness: 12,
                    overconfidence: 13,
                    hp: 15),
            };

            var actual = await sut.GetAllAsync();

            actual
                .Should()
                .BeEquivalentTo(expected);
        }
    }
}