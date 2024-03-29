﻿namespace WhimFights.UseCases
{
    using System.Linq;
    using System.Threading.Tasks;
    using WhimFights.UseCases.Ports;

    public class StatisticsQueryHandler : StatisticsQuery.IHandler
    {
        private readonly IDice dice;

        private const int DiceMaxValue = 6;

        private const int DiceMinValue = 1;

        public StatisticsQueryHandler(
            IDice dice)
        {
            this.dice = dice;
        }

        public async Task<Statistics> HandleAsync(
            StatisticsQuery query)
        {
            if (query.Attaker.Prowess + DiceMaxValue <= query.Defender.Slyness + DiceMinValue &&
                query.Defender.Prowess + DiceMaxValue > query.Attaker.Slyness + DiceMinValue)
            {
                return new Statistics(
                    countOfFights: 0,
                    attackerStatistics: new FighterStatistics(
                        fighterId: query.Attaker.Id,
                        victories: 0,
                        averageHealth: -1),
                    defenderStatistics: new FighterStatistics(
                        fighterId: query.Defender.Id,
                        victories: 0,
                        averageHealth: -1));
            }

            var tasks = Enumerable.Range(0, query.FightsCount).Select(
                _ => Task.Run(() =>
                    this.GetFighterStatistics(
                        attacker: this.CreateFighter(query.Attaker),
                        defender: this.CreateFighter(query.Defender))));

            var fights = (await Task.WhenAll(tasks)
                    .ConfigureAwait(false))
                .ToList();

            var attackerFightsWins = fights
                .Where(fighter => fighter.WinnerId == query.Attaker.Id)
                .ToList();
            var attackerWinsAverageHealth = attackerFightsWins.Count > 0
                ? attackerFightsWins.Sum(fighter => fighter.WinnerHp) / attackerFightsWins.Count
                : -1;
            var attackerFightStatistics = new FighterStatistics(
                fighterId: query.Attaker.Id,
                victories: attackerFightsWins.Count,
                averageHealth: attackerWinsAverageHealth);

            var defenderFightWins = fights
                .Where(fighter => fighter.WinnerId == query.Defender.Id)
                .ToList();
            var defenderWinsAverageHealth = defenderFightWins.Count > 0
                ? defenderFightWins.Sum(fighter => fighter.WinnerHp) / defenderFightWins.Count
                : -1;
            var defenderFightStatistics = new FighterStatistics(
                fighterId: query.Defender.Id,
                victories: defenderFightWins.Count,
                averageHealth: defenderWinsAverageHealth);

            return new Statistics(
                countOfFights: query.FightsCount,
                attackerStatistics: attackerFightStatistics,
                defenderStatistics: defenderFightStatistics);
        }

        private Fighter CreateFighter(
            Character character)
        {
            if (character is PlayerCharacter { HasSupport: true })
            {
                return new Fighter(
                    id: character.Id,
                    prowess: character.Prowess + 2 + this.dice.Throw1To6(),
                    slyness: character.Slyness + 2 + this.dice.Throw1To6(),
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
            Fighter attacker,
            Fighter defender)
        {
            var attackerOverconfidence = attacker.Overconfidence + (2 * this.dice.Throw1To6());
            var defenderSlyness = defender.Flair + (2 * this.dice.Throw1To6());

            Fighter hittingFirst;
            Fighter hittingSecond;

            if (attackerOverconfidence >= defenderSlyness)
            {
                hittingFirst = attacker;
                hittingSecond = defender;
            }
            else
            {
                hittingFirst = defender;
                hittingSecond = attacker;
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

            var isAttackerWon = attacker.IsAlive();

            return isAttackerWon
                ? new OneFightStatistics(
                    winnerId: attacker.Id,
                    winnerHp: attacker.Hp)
                : new OneFightStatistics(
                    winnerId: defender.Id,
                    winnerHp: defender.Hp);
        }

        private void Punch(
            Fighter hitting,
            Fighter blocking)
        {
            var attack = hitting.Prowess + (2 * this.dice.Throw1To6());
            var defence = blocking.Slyness + (2 * this.dice.Throw1To6());

            var damage = attack - defence;

            if (damage > 0)
            {
                blocking.DecreaseHp(damage);
            }
        }

        private sealed class Fighter
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
                if (this.Hp - value < 0)
                {
                    this.Hp = 0;
                }
                else
                {
                    this.Hp -= value;
                }
            }
        }

        private sealed class OneFightStatistics
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