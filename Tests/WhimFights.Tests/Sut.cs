namespace WhimFights.Tests
{
    using System.Collections.Generic;

    public class Sut
    {
        public List<IResponce> Responces { get; set; } = new List<IResponce>();

        public void AcceptStimuli(
            List<IStimulus> stimuli)
        {
            foreach (var stimulus in stimuli)
            {
                this.AcceptStimulus(
                    stimulus: stimulus);
            }
        }

        private void AcceptStimulus(
            IStimulus stimulus)
        {
            switch (stimulus)
            {
                case SaveCharacter:
                    break;
                case ChangeCharacter:
                    break;
                case GetCharacter:
                    break;
                case GetFightResult:
                    break;
                case GetAllCharacters:
                    break;
            }
        }
    }
}