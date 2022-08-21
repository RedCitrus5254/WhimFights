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
            int hp,
            bool hasSupport)
            : base(
                id: id,
                prowess: prowess,
                slyness: slyness,
                overconfidence: overconfidence,
                flair: flair,
                hp: hp)
        {
            this.HasSupport = hasSupport;
        }

        public bool HasSupport { get; }
    }
}