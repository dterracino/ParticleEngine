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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Color = Microsoft.Xna.Framework.Color;

namespace GameHelpersLib
{
    public static class DrawingHelpers
    {
        public static Texture2D BitmapToTexture(GraphicsDevice gd, Image img)
        {
            int bufferSize = img.Height * img.Width * 4;

            using (MemoryStream ms = new MemoryStream(bufferSize))
            {
                img.Save(ms, ImageFormat.Png);
                return Texture2D.FromStream(gd, ms);
            }
        }

        public static Texture2D IntsToTexture(GraphicsDevice gd, int[] img, int width, int height)
        {
            Texture2D texture = new Texture2D(gd, width, height);
            texture.SetData(img);
            return texture;
        }

        public static Image TextureToImage(Texture2D texture)
        {
            return TextureToImage(texture, texture.Width, texture.Height);
        }

        public static Image TextureToImage(Texture2D texture, int width, int height)
        {
            MemoryStream ms = new MemoryStream();
            texture.SaveAsPng(ms, width, height);
            return Image.FromStream(ms);
        }

        public static Texture2D CreateOnePixelTexture(GraphicsDevice gd, Color color)
        {
            Texture2D texture = new Texture2D(gd, 1, 1);
            texture.SetData<Color>(new Color[1] { color });
            return texture;
        }

        private static Vector2[] shadowOffset = { new Vector2(-1, -1), new Vector2(1, -1), new Vector2(1, 1), new Vector2(-1, 1) };

        public static void DrawTextWithShadow(SpriteBatch sb, string text, Vector2 position, SpriteFont font, Color color, Color shadowColor)
        {
            sb.DrawString(font, text, position + shadowOffset[0], shadowColor);
            sb.DrawString(font, text, position + shadowOffset[1], shadowColor);
            sb.DrawString(font, text, position + shadowOffset[2], shadowColor);
            sb.DrawString(font, text, position + shadowOffset[3], shadowColor);
            sb.DrawString(font, text, position, color);
        }
    }
}