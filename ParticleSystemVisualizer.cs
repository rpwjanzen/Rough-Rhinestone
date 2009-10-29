using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Particles {
    class ParticleSystemVisualizer {
        public Vector2 Position;
        public Color Color;

        ParticleSystem particleSystem;
        Texture2D texture;
        float scale = 0.001f;
        Vector2 origin;

        public ParticleSystemVisualizer(ParticleSystem ps, Vector2 position, float scale) {
            particleSystem = ps;
            Position = position;
            this.scale = scale;
        }

        public void LoadContent(ContentManager content) {
            texture = content.Load<Texture2D>(@"Particle");
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public void Draw(SpriteBatch sb) {
            Draw(sb, particleSystem);
        }

        public void Draw(SpriteBatch sb, ParticleSystem ps) {
            foreach (var p in ps.Particles) {
                if (p != null) {
                    var c = Color;
                    c.A = (byte)MathHelper.Lerp(255, 0, (p.Age / (float)p.MaxAge));
                    var p0 = p.Position.ToVector2() * scale + Position;
                    sb.Draw(texture, p0, null, c, 0, origin, 1f, SpriteEffects.None, 0f);
                }
            }

            foreach (var a in ps.Attractors) {
                var pos = a.Position.ToVector2() * scale + Position;
                sb.Draw(texture, pos, null, Color, 0, origin, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}
