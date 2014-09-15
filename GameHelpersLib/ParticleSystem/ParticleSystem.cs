#region License Information (GPL v3)

/*
    Copyright (C) Jaex

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v3)

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameHelpersLib
{
    public static class ParticleSystem
    {
        public const int ParticleLimit = 30000;

        public static bool Enabled { get; set; }

        public static int ParticleCount { get; private set; }

        private static Particle[] particles;

        static ParticleSystem()
        {
            particles = new Particle[ParticleLimit];

            Enabled = true;
        }

        public static Particle GetFreeParticle()
        {
            if (ParticleCount < ParticleLimit)
            {
                Particle particle = new Particle();
                particles[ParticleCount] = particle;
                ParticleCount++;
                return particle;
            }

            return null;
        }

        public static IEnumerable<Particle> GetFreeParticles(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Particle particle = GetFreeParticle();

                if (particle == null)
                {
                    break;
                }

                yield return particle;
            }
        }

        public static void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                Parallel.For(0, ParticleCount, i =>
                {
                    particles[i].Update(deltaTime);
                });

                Cleanup();
            }
        }

        private static void Cleanup()
        {
            int i = 0;

            while (i < ParticleCount)
            {
                if (particles[i].IsActive)
                {
                    i++;
                }
                else
                {
                    particles[i] = particles[ParticleCount - 1];
                    ParticleCount--;
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (Enabled)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);

                for (int i = 0; i < ParticleCount; i++)
                {
                    particles[i].Draw(spriteBatch);
                }

                spriteBatch.End();
            }
        }
    }
}