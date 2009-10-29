using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ParticlesScreenSaver {
    class Timer {
        public int Interval;
        int elapsedTime;

        public event EventHandler TimeElapsed;

        public void Update(GameTime gameTime) {
            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsedTime >= Interval) {
                elapsedTime = 0;
                if (TimeElapsed != null) {
                    TimeElapsed(this, EventArgs.Empty);
                }
            }
        }
    }
}
