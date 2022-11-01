namespace WhimFights.UseCases.Infrastructure
{
    using System;
    using WhimFights.UseCases.Ports;

    public class Dice : IDice
    {
        private readonly Random rnd = new Random();

        public int Throw1To6()
        {
            return this.rnd.Next(
                minValue: 1,
                maxValue: 6);
        }
    }
}