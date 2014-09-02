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

namespace GameHelpersLib
{
    public static class ParticleSystem
    {
        public const int ParticleLimit = 10000;

        public static bool Enabled { get; set; }

        public static int ParticleCount
        {
            get
            {
                return ParticleLimit - FreeParticleCount;
            }
        }

        public static int FreeParticleCount
        {
            get
            {
                return freeParticles.Count;
            }
        }

        private static Particle[] particles;
        private static Queue<Particle> freeParticles;

        static ParticleSystem()
        {
            particles = new Particle[ParticleLimit];
            freeParticles = new Queue<Particle>(ParticleLimit);

            for (int i = 0; i < particles.Length; i++)
            {
                particles[i] = new Particle();
                freeParticles.Enqueue(particles[i]);
            }

            Enabled = true;
        }

        public static Particle GetFreeParticle()
        {
            if (FreeParticleCount > 0)
            {
                Particle p = freeParticles.Dequeue();
                p.Initialize(); // Resets particle properties
                return p;
            }

            return null;
        }

        public static IEnumerable<Particle> GetFreeParticles(int count)
        {
            List<Particle> particles = new List<Particle>(count);

            for (int i = 0; i < count; i++)
            {
                Particle particle = GetFreeParticle();

                if (particle == null) break;

                particles.Add(particle);
            }

            return particles;
        }

        public static void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                foreach (Particle p in particles)
                {
                    if (p.IsActive)
                    {
                        p.Update(deltaTime);

                        if (!p.IsActive)
                        {
                            freeParticles.Enqueue(p);
                        }
                    }
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (Enabled)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive); // For transparent particles

                foreach (Particle p in particles)
                {
                    if (p.IsActive)
                    {
                        p.Draw(spriteBatch);
                    }
                }

                spriteBatch.End();
            }
        }
    }
}