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

using GameHelpersLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ParticleEngine
{
    public class PlasmaParticleSystem : ParticleSystem
    {
        public PlasmaParticleSystem(Game game)
            : base(game)
        {
            AddTextureNames("Gradient");
        }

        public override void Emit(Vector2 emitterLocation)
        {
            for (int i = 0; i < 5; i++)
            {
                Particle p = GetFreeParticle();

                if (p != null)
                {
                    p.Position = emitterLocation;
                    p.Lifetime = MathHelpers.Random.NextFloat(1, 3);
                    p.Color = MathHelpers.Random.NextColor(Color.White, Color.CornflowerBlue).ToVector3();
                    p.Speed = MathHelpers.Random.Next(1, 300);
                    p.Scale = MathHelpers.Random.NextFloat(0.5f, 2f);
                    p.Direction = MathHelpers.RadianToVector2(MathHelpers.Random.NextAngle());
                    p.RectangleLimit = GraphicsDevice.Viewport.Bounds;
                    p.RectangleLimitAction = ParticleRectangleLimitAction.Bounce;
                    p.Modifiers = new List<IModifier> {
                        new OpacityModifier
                        {
                            InitialOpacity = 1,
                            FinalOpacity = 0
                        }
                    };
                }
                else
                {
                    break;
                }
            }
        }
    }
}