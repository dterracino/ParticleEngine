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

using GameHelpersLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ParticleEngine
{
    public class TestWindow : Game
    {
        public static string Title { get; set; }
        public static int Width { get; set; }
        public static int Height { get; set; }

        public static Rectangle Bounds
        {
            get
            {
                return new Rectangle(0, 0, Width, Height);
            }
        }

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private FrameRateCounter frameRateCounter;
        private GameConsole console;
        private ParticleEmitter emitter, emitter2, emitter3;
        private EntityPlasma entityPlasma;

        public TestWindow()
        {
            Title = "Particle Test";
            Width = 1280;
            Height = 720;

            Window.Title = Title;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Width;
            graphics.PreferredBackBufferHeight = Height;
            //graphics.PreferMultiSampling = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            frameRateCounter = new FrameRateCounter(this);
            Components.Add(frameRateCounter);

            console = new GameConsole(this);
            Components.Add(console);

            emitter = new PlasmaEmitter();
            emitter2 = new SpiralEmitter();
            emitter3 = new TestEmitter();
            entityPlasma = new EntityPlasma();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Resources.LoadContent(Content);

            //frameRateCounter.LoadFont(Resources.Fonts["MainFont"]);
            console.LoadFont(Resources.Fonts["MainFont"]);

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();

            if (IsActive)
            {
                if (Input.IsKeyDown(Keys.Escape))
                {
                    Exit();
                }

                Vector2 mousePosition = Input.GetMousePosition();

                if (Input.IsMouseDown(MouseButtons.Left))
                {
                    emitter.Emit(gameTime, mousePosition);
                }

                if (Input.IsMouseDown(MouseButtons.Right))
                {
                    emitter2.Emit(gameTime, mousePosition);
                }

                if (Input.IsMouseDown(MouseButtons.Middle))
                {
                    emitter3.Emit(gameTime, mousePosition);
                }
            }

            entityPlasma.Update(gameTime);

            ParticleSystem.Update(gameTime);

            base.Update(gameTime);

            Window.Title = string.Format("{0} [FPS: {1}] [Particles: {2} / {3}]", Title, frameRateCounter.CurrentFrameRate, ParticleSystem.ParticleCount, ParticleSystem.ParticleLimit);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            ParticleSystem.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}