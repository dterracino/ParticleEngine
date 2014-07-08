using GameHelpersLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParticleEngine
{
    public class TestParticleSystem : ParticleSystem
    {
        public TestParticleSystem(Game game)
            : base(game)
        {
            AddTextureNames("Gradient");
        }

        public void Emit(Vector2 emitterLocation)
        {
            for (int i = 0; i < 5; i++)
            {
                Particle p = GetFreeParticle();

                if (p != null)
                {
                    p.Position = emitterLocation;
                    p.Lifetime = 3;
                    p.Color = Color.CornflowerBlue.ToVector3();
                    p.Speed = 500;
                    p.Scale = 1f;
                    p.Direction = MathHelpers.RadianToVector2(MathHelpers.Random.NextAngle());
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