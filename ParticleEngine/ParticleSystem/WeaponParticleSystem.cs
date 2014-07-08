using GameHelpersLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParticleEngine
{
    public class WeaponParticleSystem : ParticleSystem
    {
        public float FireDelay = 0.1f;
        public float SpreadingBulletsCount = 10;
        public float SpreadingBulletsSpread = 7;

        private GameTimer timer;

        public WeaponParticleSystem(Game game)
            : base(game)
        {
            AddTextureNames("Square");
            timer = new GameTimer(FireDelay);
        }

        public void Emit(GameTime gameTime, Vector2 emitterLocation, Vector2 cursorLocation)
        {
            if (timer.Update(gameTime))
            {
                for (int i = 0; i < SpreadingBulletsCount; i++)
                {
                    Particle p = GetFreeParticle();

                    if (p != null)
                    {
                        float offset = (-((SpreadingBulletsCount - 1) / 2) + i) * SpreadingBulletsSpread;
                        float direction = MathHelpers.LookAtRadian(emitterLocation, cursorLocation);
                        direction += MathHelpers.DegreeToRadian(offset);

                        p.Position = emitterLocation;
                        p.Lifetime = 3;
                        p.Color = MathHelpers.Random.NextColor(Color.White, Color.CornflowerBlue).ToVector3();
                        p.Speed = 750;
                        p.Direction = MathHelpers.RadianToVector2(direction);
                        p.Scale = 0.2f;
                        p.AutoAngle = true;
                        p.RectangleLimit = GraphicsDevice.Viewport.Bounds;
                        p.RectangleLimitAction = ParticleRectangleLimitAction.Bounce;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}