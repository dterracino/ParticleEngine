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

namespace GameHelpersLib
{
    public class HueAnimation
    {
        /// <summary>
        /// Hue step per second.
        /// </summary>
        public float Step { get; set; }

        /// <summary>
        /// Value between 0 and 6
        /// </summary>
        public float Hue { get; set; }

        public float Saturation { get; set; }

        public float Value { get; set; }

        public HueAnimation()
        {
            Saturation = 1f;
            Value = 1f;
        }

        public HueAnimation(float step)
            : this()
        {
            Step = step;
        }

        public HueAnimation(float step, float hue)
            : this()
        {
            Step = step;
            Hue = hue;
        }

        public Color Next(float deltaTime)
        {
            Hue += Step * deltaTime;
            Hue %= 6;
            return Helpers.HSVToColor(Hue, Saturation, Value);
        }
    }
}