namespace WhimFights.Tests.Fakes
{
    using WhimFights.UseCases.Ports;

    public class FakeDice : IDice
    {
        private readonly int predefinedValue;

        public FakeDice(
            int predefinedValue)
        {
            this.predefinedValue = predefinedValue;
        }

        public int Throw1To6()
        {
            return this.predefinedValue;
        }
    }
}