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
    public class SpiralEmitter : IParticleEmitter
    {
        private Texture2D texture;
        private float angle;

        public void Emit(Vector2 position)
        {
            for (int i = 0; i < 50; i++)
            {
                Particle p = ParticleSystem.GetFreeParticle();

                if (p == null) break;

                if (texture == null)
                {
                    texture = Resources.Textures["Particle005"];
                }

                p.Texture = texture;
                p.Position = position;
                p.Direction = MathHelpers.RadianToVector2(MathHelpers.Random.NextAngle());
                p.Speed = 500;
                p.Lifetime = MathHelpers.Random.NextFloat(1.0f, 2.0f);
                p.Angle = MathHelpers.Random.NextAngle();
                p.AutoAngle = true;
                p.RectangleLimit = new Rectangle(0, 0, TestWindow.Width, TestWindow.Height).Offset(5);
                p.RectangleLimitAction = ParticleRectangleLimitAction.Bounce;
                p.Scale = MathHelpers.Random.NextFloat(0.1f, 1f);
                p.Color = MathHelpers.Random.NextColor(Color.DarkBlue, Color.CornflowerBlue).ToVector3();
                p.Modifiers = new List<IParticleModifier> {
                    new DirectionModifier
                    {
                        InitialDirection = MathHelpers.RadianToVector2(angle),
                        FinalDirection = MathHelpers.DegreeToVector2(90, 2)
                    },
                    new OpacityModifier
                    {
                        InitialOpacity = 1,
                        FinalOpacity = 0
                    }
                };

                angle += MathHelpers.DegreeToRadian(0.5f);
            }
        }
    }
}