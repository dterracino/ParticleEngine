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

namespace GameHelpersLib
{
    public abstract class ParticleEmitter
    {
        protected string TextureName { get; set; }

        private Texture2D texture;

        protected Texture2D Texture
        {
            get
            {
                if (texture == null)
                {
                    texture = Resources.Textures[TextureName];
                }

                return texture;
            }
        }

        public abstract void Emit(GameTime gameTime, Vector2 position);
    }
}