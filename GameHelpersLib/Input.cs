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
using Microsoft.Xna.Framework.Input;

namespace GameHelpersLib
{
    public enum MouseButtons
    {
        Left, Right, Middle
    }

    public static class Input
    {
        private static KeyboardState currentKeyboardState;
        private static KeyboardState previousKeyboardState;
        private static MouseState currentMouseState;
        private static MouseState previousMouseState;

        public static bool IsShiftDown
        {
            get
            {
                return IsKeyDown(Keys.LeftShift) || IsKeyDown(Keys.RightShift);
            }
        }

        public static bool IsControlDown
        {
            get
            {
                return IsKeyDown(Keys.LeftControl) || IsKeyDown(Keys.RightControl);
            }
        }

        public static bool IsAltDown
        {
            get
            {
                return IsKeyDown(Keys.LeftAlt) || IsKeyDown(Keys.RightAlt);
            }
        }

        public static bool UpdateKeyboardState { get; set; }
        public static bool UpdateMouseState { get; set; }

        static Input()
        {
            currentKeyboardState = new KeyboardState();
            previousKeyboardState = new KeyboardState();
            currentMouseState = new MouseState();
            previousMouseState = new MouseState();

            UpdateKeyboardState = true;
            UpdateMouseState = true;
        }

        public static void Update()
        {
            if (UpdateKeyboardState)
            {
                previousKeyboardState = currentKeyboardState;
                currentKeyboardState = Keyboard.GetState();
            }

            if (UpdateMouseState)
            {
                previousMouseState = currentMouseState;
                currentMouseState = Mouse.GetState();
            }
        }

        public static bool IsKeyDown(Keys key, bool isFirst = false)
        {
            bool result = currentKeyboardState.IsKeyDown(key);
            if (isFirst) result &= previousKeyboardState.IsKeyUp(key);
            return result;
        }

        public static bool IsKeyUp(Keys key, bool isFirst = false)
        {
            bool result = currentKeyboardState.IsKeyUp(key);
            if (isFirst) result &= previousKeyboardState.IsKeyDown(key);
            return result;
        }

        public static bool IsMouseDown(MouseButtons button, bool isFirst = false)
        {
            bool result;

            switch (button)
            {
                default:
                case MouseButtons.Left:
                    result = currentMouseState.LeftButton == ButtonState.Pressed;
                    if (isFirst) result &= previousMouseState.LeftButton == ButtonState.Released;
                    break;
                case MouseButtons.Right:
                    result = currentMouseState.RightButton == ButtonState.Pressed;
                    if (isFirst) result &= previousMouseState.RightButton == ButtonState.Released;
                    break;
                case MouseButtons.Middle:
                    result = currentMouseState.MiddleButton == ButtonState.Pressed;
                    if (isFirst) result &= previousMouseState.MiddleButton == ButtonState.Released;
                    break;
            }

            return result;
        }

        public static bool IsMouseUp(MouseButtons button, bool isFirst = false)
        {
            bool result;

            switch (button)
            {
                default:
                case MouseButtons.Left:
                    result = currentMouseState.LeftButton == ButtonState.Released;
                    if (isFirst) result &= previousMouseState.LeftButton == ButtonState.Pressed;
                    break;
                case MouseButtons.Right:
                    result = currentMouseState.RightButton == ButtonState.Released;
                    if (isFirst) result &= previousMouseState.RightButton == ButtonState.Pressed;
                    break;
                case MouseButtons.Middle:
                    result = currentMouseState.MiddleButton == ButtonState.Released;
                    if (isFirst) result &= previousMouseState.MiddleButton == ButtonState.Pressed;
                    break;
            }

            return result;
        }

        public static Vector2 GetMousePosition()
        {
            return new Vector2(currentMouseState.X, currentMouseState.Y);
        }

        public static void SetMousePosition(Vector2 pos)
        {
            Mouse.SetPosition((int)pos.X, (int)pos.Y);
        }
    }
}