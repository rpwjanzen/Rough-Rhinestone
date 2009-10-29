using System;
using System.Collections.Generic;

namespace Particles {
    abstract class ParticleSystem {

        public Particle[] Particles;
        public PVector2 Gravity = PVector2.Down;
        public PVector2 Position;

        public List<Attractor> Attractors;


        protected int NumParticles;
        protected int MaxAge;
        protected int AgeVarience;
        protected int CurNumParticles;
        
        protected Random Random;

        FInt minDistanceSquared = new FInt(5 * 5);
        PVector2 scratchV = PVector2.Zero;
        FInt scratchF = FInt.ZeroF;

        public ParticleSystem(Random r, PVector2 position, int numParticles, int maxAge) {
            Random = r;
            Position = position;
            NumParticles = numParticles;
            MaxAge = maxAge;
            AgeVarience = (int)(MaxAge * 0.4f);

            Attractors = new List<Attractor>();
            CurNumParticles = 0;

            Particles = new Particle[NumParticles];
            for (int i = 0; i < 10; i++) {
                Particles[i] = CreateParticle();
            }
            CurNumParticles = 10;
        }

        static FInt ratio = new FInt(2);
        public void Update() {
            if (CurNumParticles < Particles.Length) {
                Particles[CurNumParticles] = CreateParticle();
                CurNumParticles++;
            }

            for(int i = 0; i < Particles.Length; i++) {
                var p = Particles[i];
                if (p != null) {
                    p.Update();

                    if (p.Alive) {
                        foreach (var a in Attractors) {
                            CalculateForces(p, a);
                        }
                    } else {
                        ResetParticle(p);
                    }
                }
            }
        }

        private void CalculateForces(Particle p, Attractor a) {
            // F = G(m1 * m2) / r^2
            PVector2.Sub(ref a.Position, ref p.Position, out scratchV);
            scratchV.LengthSquared(out scratchF);
            if (scratchF > minDistanceSquared) {
                FInt.Divide(ref FInt.OneF, ref scratchF, out scratchF);

                if (scratchF != FInt.ZeroF) {
                    // force along direction between
                    FInt.Divide(ref scratchF, ref ratio, out scratchF);

                    PVector2.Multiply(ref scratchV, ref scratchF, out scratchV);
                    PVector2.Add(ref p.VelocityPerTick, ref scratchV, out p.VelocityPerTick);
                    //p.VelocityPerTick += scratchV;
                }
            }
        }

        protected abstract Particle CreateParticle();
        protected abstract void ResetParticle(Particle p);
    }
}
