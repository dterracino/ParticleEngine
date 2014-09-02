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

namespace GameHelpersLib
{
    public static class Extensions
    {
        public static float NextFloat(this Random rand)
        {
            return (float)rand.NextDouble();
        }

        public static float NextFloat(this Random rand, float max)
        {
            return rand.NextFloat() * max;
        }

        public static float NextFloat(this Random rand, float min, float max)
        {
            return min + rand.NextFloat() * (max - min);
        }

        public static Color NextColor(this Random rand)
        {
            return new Color(rand.Next(0, 256), rand.Next(0, 256), rand.Next(0, 256));
        }

        public static Color NextColor(this Random rand, Color min, Color max)
        {
            float random = rand.NextFloat();
            int r = (int)(min.R + (max.R - min.R) * random);
            int g = (int)(min.G + (max.G - min.G) * random);
            int b = (int)(min.B + (max.B - min.B) * random);
            return new Color(r, g, b);
        }

        public static float NextAngle(this Random rand)
        {
            return rand.NextFloat(MathHelpers.TwoPi);
        }

        public static Vector2 Offset(this Vector2 vector, int offset)
        {
            return new Vector2(vector.X + offset, vector.Y + offset);
        }

        public static Rectangle Offset(this Rectangle rect, int offset)
        {
            return new Rectangle(rect.X - offset, rect.Y - offset, rect.Width + offset * 2, rect.Height + offset * 2);
        }

        public static Rectangle LocationOffset(this Rectangle rect, int x, int y)
        {
            return new Rectangle(rect.X + x, rect.Y + y, rect.Width, rect.Height);
        }

        public static Rectangle LocationOffset(this Rectangle rect, int offset)
        {
            return rect.LocationOffset(offset, offset);
        }

        public static Rectangle SizeOffset(this Rectangle rect, int width, int height)
        {
            return new Rectangle(rect.X, rect.Y, rect.Width + width, rect.Height + height);
        }

        public static Rectangle SizeOffset(this Rectangle rect, int offset)
        {
            return rect.SizeOffset(offset, offset);
        }
    }
}