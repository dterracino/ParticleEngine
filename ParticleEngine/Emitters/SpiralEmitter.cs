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
using System.Collections.Generic;

namespace ParticleEngine
{
    public class SpiralEmitter : ParticleEmitter
    {
        private float angle;

        public SpiralEmitter()
        {
            TextureName = "Particle005";
        }

        public override void Emit(GameTime gameTime, Vector2 position)
        {
            foreach (Particle p in ParticleSystem.GetFreeParticles(50))
            {
                p.Texture = Texture;
                p.Position = position;
                p.Direction = MathHelpers.RadianToVector2(MathHelpers.Random.NextAngle());
                p.Speed = 500f;
                p.Lifetime = MathHelpers.Random.NextFloat(1f, 2f);
                p.Angle = MathHelpers.Random.NextAngle();
                p.AutoAngle = true;
                p.RectangleLimit = TestWindow.Bounds.Offset(10);
                p.RectangleLimitAction = ParticleRectangleLimitAction.Bounce;
                p.Scale = MathHelpers.Random.NextFloat(0.1f, 1f);
                p.Color = MathHelpers.Random.NextColor(Color.DarkBlue, Color.CornflowerBlue);
                p.Modifiers = new List<IParticleModifier> {
                    new DirectionModifier
                    {
                        InitialDirection = MathHelpers.RadianToVector2(angle),
                        FinalDirection = MathHelpers.DegreeToVector2(90f, 2f)
                    },
                    new OpacityModifier
                    {
                        InitialOpacity = 1f,
                        FinalOpacity = 0f
                    }
                };

                angle += MathHelpers.DegreeToRadian(0.5f);
            }
        }
    }
}