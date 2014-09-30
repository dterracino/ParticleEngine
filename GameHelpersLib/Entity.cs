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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GameHelpersLib
{
    public class Entity
    {
        public Vector2 Position { get; set; }
        public Size Size { get; set; }
        public Vector2 Velocity { get; set; }
        public float Speed { get; set; }
        public ParticleEmitter Emitter { get; set; }

        public virtual void Update(GameTime gameTime)
        {
            if (Speed > 0)
            {
                Position += Velocity * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (Emitter != null)
            {
                Emitter.Emit(gameTime, Position);
            }
        }
    }
}