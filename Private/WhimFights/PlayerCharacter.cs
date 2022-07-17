namespace WhimFights
{
    public class PlayerCharacter
        : Character
    {
        public PlayerCharacter(
            string id,
            int prowess,
            int slyness,
            int overconfidence,
            int flair,
            int hp)
            : base(
                id: id,
                prowess: prowess,
                slyness: slyness,
                overconfidence: overconfidence,
                flair: flair,
                hp: hp)
        {
        }
    }
}