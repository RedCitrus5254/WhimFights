namespace WhimFights.UseCases.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using WhimFights.UseCases.Ports;

    public class CharacterMapper : ICharacterMapper
    {
        private readonly string filePath;

        public CharacterMapper(
            string filePath)
        {
            this.filePath = filePath;
        }

        public Task SaveAsync(
            Character character)
        {
            var isNpc = character is NPC;
            var stringCharacter = $"{character.Id}, {character.Prowess}, {character.Flair}," +
                                  $" {character.Slyness}, {character.Overconfidence}, {character.Hp}, {isNpc}";

            var file = File.ReadAllText(this.filePath);

            var isCharacterAlreadySaved = Regex.Match(
                input: file,
                pattern: $"{character.Id}",
                options: RegexOptions.Singleline);

            if (isCharacterAlreadySaved.Success)
            {
                var newText = Regex.Replace(
                    pattern: $"{character.Id}",
                    input: file,
                    replacement: stringCharacter,
                    options: RegexOptions.Singleline);

                File.WriteAllText(
                    path: this.filePath,
                    contents: newText);
            }
            else
            {
                var newText = $"{file}{Environment.NewLine}{stringCharacter}";

                File.WriteAllText(
                    path: this.filePath,
                    contents: newText);
            }

            return Task.CompletedTask;
        }

        public async Task<Character> GetAsync(
            string id)
        {
            using var streamReader = new StreamReader(
                path: this.filePath);

            while (!streamReader.EndOfStream)
            {
                var line = await streamReader
                    .ReadLineAsync()
                    .ConfigureAwait(false);

                var row = line.Split(',');

                if (row[0] == id)
                {
                    if (row.Length > 6 && bool.TryParse(row[6], out var isNpc) && isNpc)
                    {
                        return new NPC(
                            id: row[0],
                            prowess: int.Parse(row[1]),
                            flair: int.Parse(row[2]),
                            slyness: int.Parse(row[3]),
                            overconfidence: int.Parse(row[4]),
                            hp: int.Parse(row[5]));
                    }
                    else
                    {
                        return new PlayerCharacter(
                            id: row[0],
                            prowess: int.Parse(row[1]),
                            flair: int.Parse(row[2]),
                            slyness: int.Parse(row[3]),
                            overconfidence: int.Parse(row[4]),
                            hp: int.Parse(row[5]),
                            hasSupport: false);
                    }
                }
            }

            throw new InvalidDataException();
        }

        public async Task<List<Character>> GetAllAsync()
        {
            var characters = new List<Character>();
            using var streamReader = new StreamReader(
                path: this.filePath);

            while (!streamReader.EndOfStream)
            {
                var line = await streamReader
                    .ReadLineAsync()
                    .ConfigureAwait(false);

                var row = line.Split(',');

                if (!string.IsNullOrWhiteSpace(row[0]))
                {
                    if (row.Length > 6 && bool.TryParse(row[6], out var isNpc) && isNpc)
                    {
                        characters.Add(new NPC(
                            id: row[0],
                            prowess: int.Parse(row[1]),
                            flair: int.Parse(row[2]),
                            slyness: int.Parse(row[3]),
                            overconfidence: int.Parse(row[4]),
                            hp: int.Parse(row[5])));
                    }
                    else
                    {
                        characters.Add(new PlayerCharacter(
                            id: row[0],
                            prowess: int.Parse(row[1]),
                            flair: int.Parse(row[2]),
                            slyness: int.Parse(row[3]),
                            overconfidence: int.Parse(row[4]),
                            hp: int.Parse(row[5]),
                            hasSupport: false));
                    }
                }
            }

            return characters;
        }
    }
}