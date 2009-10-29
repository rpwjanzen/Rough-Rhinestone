using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Particles {
    class ZeroFountain : Fountain {

        public ZeroFountain(Random r, PVector2 p) : base(r, p) { }

        protected override void ResetParticle(Particle p) {
            p.Position = Position;
            p.VelocityPerTick = PVector2.Zero;
            p.MaxAge = Random.Next(-AgeVarience, AgeVarience) + MaxAge;
            p.Age = 0;
        }
    }
}
