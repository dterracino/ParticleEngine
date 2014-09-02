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

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace GameHelpersLib
{
    public static class Resources
    {
        public static Dictionary<string, Texture2D> Textures { get; private set; }

        public static Dictionary<string, SpriteFont> Fonts { get; private set; }

        public static void LoadContent(ContentManager content)
        {
            LoadTextures(content);
            LoadFonts(content);
        }

        private static void LoadTextures(ContentManager content)
        {
            Textures = new Dictionary<string, Texture2D>();

            foreach (string filepath in Directory.GetFiles("Content\\Textures", "*.xnb"))
            {
                string name = Path.GetFileNameWithoutExtension(filepath);
                Textures.Add(name, content.Load<Texture2D>("Textures\\" + name));
            }
        }

        private static void LoadFonts(ContentManager content)
        {
            Fonts = new Dictionary<string, SpriteFont>();

            foreach (string filepath in Directory.GetFiles("Content\\Fonts", "*.xnb"))
            {
                string name = Path.GetFileNameWithoutExtension(filepath);
                Fonts.Add(name, content.Load<SpriteFont>("Fonts\\" + name));
            }
        }
    }
}