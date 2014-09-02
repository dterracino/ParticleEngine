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
using System;

namespace GameHelpersLib
{
    public class FrameRateCounter : DrawableGameComponent
    {
        public int CurrentFrameRate { get; private set; }
        public bool RenderOnScreen { get; set; }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        public Vector2 ShadowPosition { get; set; }
        public Color ShadowColor { get; set; }

        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private int frameCounter = 0;
        private TimeSpan elapsedTime = TimeSpan.Zero;
        private readonly TimeSpan oneSecond = TimeSpan.FromSeconds(1);

        public FrameRateCounter(Game game)
            : base(game)
        {
            RenderOnScreen = false;
        }

        public FrameRateCounter(Game game, Vector2 position, Color color, Color shadowColor)
            : base(game)
        {
            RenderOnScreen = true;
            Position = position;
            ShadowPosition = new Vector2(Position.X + 1, Position.Y + 1);
            Color = color;
            ShadowColor = shadowColor;
        }

        public void LoadFont(SpriteFont font)
        {
            spriteFont = font;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public override void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > oneSecond)
            {
                elapsedTime -= oneSecond;
                CurrentFrameRate = frameCounter;
                frameCounter = 0;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            frameCounter++;

            if (RenderOnScreen && spriteFont != null)
            {
                string text = "FPS: " + CurrentFrameRate;

                spriteBatch.Begin();
                spriteBatch.DrawString(spriteFont, text, ShadowPosition, ShadowColor);
                spriteBatch.DrawString(spriteFont, text, Position, Color);
                spriteBatch.End();
            }
        }
    }
}