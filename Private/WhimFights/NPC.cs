namespace WhimFights
{
 #pragma warning disable S101
    public class NPC
 #pragma warning restore S101
        : Character
    {
        public NPC(
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