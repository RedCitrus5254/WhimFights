namespace WhimFights.Tests
{
    using System;

    public static class ObjectsGen
    {
        public static Character RandomCharacter(
            string? id = default,
            int? prowess = default,
            int? slyness = default,
            int? overconfidence = default,
            int? flair = default,
            int? hp = default)
        {
            var random = new Random();

            var characterId = id ?? Guid.NewGuid().ToString();

            var characterProwess = prowess ?? random.Next(1, 20);
            var characterSlyness = slyness ?? random.Next(1, 20);
            var characterOverconfidence = overconfidence ?? random.Next(1, 20);
            var characterFlair = flair ?? random.Next(1, 20);
            var characterHp = hp ?? random.Next(1, 20);

            return new PlayerCharacter(
                id: characterId,
                prowess: characterProwess,
                slyness: characterSlyness,
                overconfidence: characterOverconfidence,
                flair: characterFlair,
                hp: characterHp);
        }
    }
}