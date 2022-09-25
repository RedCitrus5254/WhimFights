namespace WhimFights.UseCases
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class StatisticsQueryHandler : StatisticsQuery.IHandler
    {
        private readonly Random random = new Random();

        public async Task<Statistics> HandleAsync(
            StatisticsQuery query)
        {
            var tasks = Enumerable.Range(0, query.FightsCount).Select(
                _ => Task.Run(() =>
                    this.GetFighterStatistics(
                        first: this.CreateFighter(query.Attaker),
                        second: this.CreateFighter(query.Defender))));

            var fights = (await Task.WhenAll(tasks)
                    .ConfigureAwait(false))
                .ToList();

            var attackerFights = fights
                .Where(fighter => fighter.WinnerId == query.Attaker.Id)
                .ToList();
            var attackerFightStatistics = new FighterStatistics(
                fighterId: query.Attaker.Id,
                victories: attackerFights.Count,
                averageHealth: attackerFights.Sum(fighter => fighter.WinnerHp) / attackerFights.Count);

            var defenderFights = fights
                .Where(fighter => fighter.WinnerId == query.Defender.Id)
                .ToList();
            var defenderFightStatistics = new FighterStatistics(
                fighterId: query.Defender.Id,
                victories: defenderFights.Count,
                averageHealth: defenderFights.Sum(fighter => fighter.WinnerHp) / defenderFights.Count);

            return new Statistics(
                countOfFights: query.FightsCount,
                firstFighterStatistics: attackerFightStatistics,
                secondFighterStatistics: defenderFightStatistics);
        }

        private Fighter CreateFighter(
            Character character)
        {
            if (character is PlayerCharacter { HasSupport: true })
            {
                return new Fighter(
                    id: character.Id,
                    prowess: character.Prowess + 2 + this.random.Next(1, 6),
                    slyness: character.Slyness + 2 + this.random.Next(1, 6),
                    overconfidence: character.Overconfidence,
                    flair: character.Flair,
                    hp: character.Hp + 10);
            }

            return new Fighter(
                id: character.Id,
                prowess: character.Prowess,
                slyness: character.Slyness,
                overconfidence: character.Overconfidence,
                flair: character.Flair,
                hp: character.Hp);
        }

        private OneFightStatistics GetFighterStatistics(
            Fighter first,
            Fighter second)
        {
            var firstFighterOverconfidence = first.Overconfidence + (2 * this.random.Next(1, 6));
            var secondFighterSlyness = second.Flair + (2 * this.random.Next(1, 6));

            Fighter hittingFirst;
            Fighter hittingSecond;

            if (firstFighterOverconfidence >= secondFighterSlyness)
            {
                hittingFirst = first;
                hittingSecond = second;
            }
            else
            {
                hittingFirst = second;
                hittingSecond = first;
            }

            while (hittingFirst.IsAlive() && hittingSecond.IsAlive())
            {
                this.Punch(
                    hitting: hittingFirst,
                    blocking: hittingSecond);

                if (!hittingSecond.IsAlive())
                {
                    break;
                }

                this.Punch(
                    hitting: hittingSecond,
                    blocking: hittingFirst);
            }

            var isFirstWon = first.IsAlive();

            return isFirstWon
                ? new OneFightStatistics(
                    winnerId: first.Id,
                    winnerHp: first.Hp)
                : new OneFightStatistics(
                    winnerId: second.Id,
                    winnerHp: second.Hp);
        }

        private void Punch(
            Fighter hitting,
            Fighter blocking)
        {
            var attack = hitting.Prowess + (2 * this.random.Next(1, 6));
            var defence = blocking.Slyness + (2 * this.random.Next(1, 6));

            var damage = attack - defence;

            if (damage > 0)
            {
                blocking.DecreaseHp(damage);
            }
        }

        private class Fighter
        {
            public Fighter(
                string id,
                int prowess,
                int slyness,
                int overconfidence,
                int flair,
                int hp)
            {
                this.Id = id;
                this.Prowess = prowess;
                this.Slyness = slyness;
                this.Overconfidence = overconfidence;
                this.Flair = flair;
                this.Hp = hp;
            }

            public int Hp { get; set; }

            public string Id { get; }

            public int Prowess { get; }

            public int Slyness { get; }

            public int Overconfidence { get; }

            public int Flair { get; }

            public bool IsAlive()
            {
                return this.Hp > 0;
            }

            public void DecreaseHp(
                int value)
            {
                this.Hp -= value;
            }
        }

        private class OneFightStatistics
        {
            public OneFightStatistics(
                string winnerId,
                int winnerHp)
            {
                this.WinnerId = winnerId;
                this.WinnerHp = winnerHp;
            }

            public string WinnerId { get; }

            public int WinnerHp { get; }
        }
    }
}