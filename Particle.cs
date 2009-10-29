namespace Particles {
    class Particle {

        public PVector2 Position;
        public PVector2 VelocityPerTick;
        public int Age;
        public int MaxAge;
        public bool Alive { get { return Age <= MaxAge; } }
        public PVector2 Gravity;

        public Particle() { }

        public Particle(PVector2 position, PVector2 velocityPerTick) {
            this.Position = position;
            this.VelocityPerTick = velocityPerTick;
            this.Age = 0;
        }

        public void Update() {
            Age += 1;
            PVector2.Add(ref VelocityPerTick, ref Gravity, out VelocityPerTick);
            PVector2.Add(ref Position, ref VelocityPerTick, out Position);
        }
    }
}
