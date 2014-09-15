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
    public static class Helpers
    {
        public static Color HSVToColor(float hue, float saturation, float value)
        {
            if (hue == 0 && saturation == 0)
            {
                return new Color(value, value, value);
            }

            float c = saturation * value;
            float x = c * (1 - Math.Abs(hue % 2 - 1));
            float m = value - c;

            if (hue < 1)
            {
                return new Color(c + m, x + m, m);
            }

            if (hue < 2)
            {
                return new Color(x + m, c + m, m);
            }

            if (hue < 3)
            {
                return new Color(m, c + m, x + m);
            }

            if (hue < 4)
            {
                return new Color(m, x + m, c + m);
            }

            if (hue < 5)
            {
                return new Color(x + m, m, c + m);
            }

            return new Color(c + m, m, x + m);
        }
    }
}