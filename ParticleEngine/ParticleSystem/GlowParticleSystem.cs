using GameHelpersLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ParticleEngine
{
    public class GlowParticleSystem : ParticleSystem
    {
        public GlowParticleSystem(Game game)
            : base(game)
        {
            AddTextureNames("BeamBlurred");
        }

        public void Emit(Vector2 emitterLocation)
        {
            for (int i = 0; i < 20; i++)
            {
                Particle p = GetFreeParticle();

                if (p != null)
                {
                    p.Position = emitterLocation;
                    p.Lifetime = 3;
                    p.RectangleLimit = GraphicsDevice.Viewport.Bounds;
                    p.RectangleLimitAction = ParticleRectangleLimitAction.Kill;
                    p.Direction = MathHelpers.RadianToVector2(MathHelpers.Random.NextAngle());
                    p.AutoAngle = true;
                    p.Speed = MathHelpers.Random.Next(1, 700);
                    p.Scale = MathHelpers.Random.NextFloat(0.1f, 3f);
                    p.Modifiers = new List<IModifier> {
                        new ColorModifier
                        {
                            InitialColor = MathHelpers.Random.NextColor(Color.White, Color.Green).ToVector3(),
                            FinalColor = Color.Black.ToVector3()
                        }
                    };
                }
                else
                {
                    break;
                }
            }
        }
    }
}