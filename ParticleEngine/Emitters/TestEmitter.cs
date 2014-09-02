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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParticleEngine
{
    public class TestEmitter : ParticleEmitter
    {
        public TestEmitter()
        {
            TextureName = "Square";
        }

        public override void Emit(GameTime gameTime, Vector2 position)
        {
            Particle p = ParticleSystem.GetFreeParticle();

            if (p != null)
            {
                p.Texture = Texture;
                p.Position = position;
                p.Lifetime = 0.1f;
                p.Color = MathHelpers.Random.NextColor().ToVector3();
                p.RectangleLimit = TestWindow.Bounds;
                p.RectangleLimitAction = ParticleRectangleLimitAction.Kill;
            }
        }
    }
}