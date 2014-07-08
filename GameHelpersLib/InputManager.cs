#region License Information (GPL v3)

/*
    Copyright (C) 2014 Jaex

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
    public class InputManager
    {
        public delegate void MouseClickDelegate(Vector2 clickPosition, bool wasSingleClick);

        public event MouseClickDelegate OnMouse1Press;
        public event MouseClickDelegate OnMouse1Release;

        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;
        private MouseState currentMouseState;
        private MouseState previousMouseState;

        public bool IsShiftDown
        {
            get
            {
                return IsKeyDown(Keys.LeftShift) || IsKeyDown(Keys.RightShift);
            }
        }

        public bool IsControlDown
        {
            get
            {
                return IsKeyDown(Keys.LeftControl) || IsKeyDown(Keys.RightControl);
            }
        }

        public bool IsAltDown
        {
            get
            {
                return IsKeyDown(Keys.LeftAlt) || IsKeyDown(Keys.RightAlt);
            }
        }

        public InputManager()
        {
            currentKeyboardState = new KeyboardState();
            previousKeyboardState = new KeyboardState();
            currentMouseState = new MouseState();
            previousMouseState = new MouseState();
        }

        public void Update()
        {
            previousKeyboardState = currentKeyboardState;
            previousMouseState = currentMouseState;

            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();

            if (OnMouse1Press != null && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                OnMouse1Press(GetMousePos(), previousMouseState.LeftButton == ButtonState.Released);
            }

            if (OnMouse1Release != null && currentMouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed)
            {
                OnMouse1Release(GetMousePos(), true);
            }
        }

        public bool IsKeyDown(Keys key, bool once = false)
        {
            if (once)
            {
                return currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key);
            }

            return currentKeyboardState.IsKeyDown(key);
        }

        public bool IsKeyUp(Keys key, bool once = false)
        {
            if (once)
            {
                return currentKeyboardState.IsKeyUp(key) && previousKeyboardState.IsKeyDown(key);
            }

            return currentKeyboardState.IsKeyUp(key);
        }

        public Vector2 GetMousePos()
        {
            return new Vector2(currentMouseState.X, currentMouseState.Y);
        }
    }
}