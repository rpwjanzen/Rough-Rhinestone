using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Particles {
    class Fountain : ParticleSystem {

        public Fountain(Random r, PVector2 position)
            : base(r, position, 2048, 1800) {
        }

        protected override Particle CreateParticle() {
            var p = new Particle();
            p.Gravity = PVector2.Zero;
            ResetParticle(p);
            return p;
        }

        protected override void ResetParticle(Particle p) {
            p.Position = Position;
            var direction = MathHelper.Lerp(0, MathHelper.TwoPi, (float)Random.NextDouble());
            var speed = Random.NextDouble() * 1.0;
            p.VelocityPerTick.X = new FInt((Math.Cos(direction) * speed));
            p.VelocityPerTick.Y = new FInt((Math.Sin(direction) * speed));
            p.MaxAge = Random.Next(-AgeVarience, AgeVarience) + MaxAge;
            p.Age = 0;
        }

        //protected override void ResetParticle(Particle p) {
        //    p.Position = Position;
        //    p.VelocityPerTick = PVector2.Zero;
        //    p.MaxAge = Random.Next(-AgeVarience, AgeVarience) + MaxAge;
        //    p.Age = 0;
        //}
    }
}
