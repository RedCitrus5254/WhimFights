namespace WhimFights
{
    public abstract class Character
    {
        protected Character(
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

        public int Hp { get; }

        public string Id { get; }

        public int Prowess { get; }

        public int Slyness { get; }

        public int Overconfidence { get; }

        public int Flair { get; }
    }
}