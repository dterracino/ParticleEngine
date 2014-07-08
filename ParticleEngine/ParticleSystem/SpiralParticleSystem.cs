using GameHelpersLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ParticleEngine
{
    public class SpiralParticleSystem : ParticleSystem
    {
        private float angle;

        public SpiralParticleSystem(Game game)
            : base(game)
        {
            AddTextureNames("Cloud004");
        }

        public void Emit(Vector2 emitterLocation)
        {
            for (int i = 0; i < 10; i++)
            {
                Particle p = GetFreeParticle();

                if (p != null)
                {
                    p.Position = emitterLocation;
                    p.Direction = MathHelpers.RadianToVector2(MathHelpers.Random.NextAngle());
                    p.Speed = MathHelpers.Random.NextFloat(300, 500);
                    p.Lifetime = MathHelpers.Random.NextFloat(1.0f, 2.0f);
                    p.Angle = MathHelpers.Random.NextAngle();
                    p.AutoAngle = true;
                    p.RectangleLimit = GraphicsDevice.Viewport.Bounds;
                    p.RectangleLimitAction = ParticleRectangleLimitAction.Bounce;
                    p.Modifiers = new List<IModifier> {
                        new ColorModifier
                        {
                            InitialColor = MathHelpers.Random.NextColor(Color.DarkBlue, Color.CornflowerBlue).ToVector3(),
                            FinalColor = Color.Black.ToVector3()
                        },
                        new ScaleModifier
                        {
                            InitialScale = MathHelpers.Random.NextFloat(0.5f, 0.7f),
                            FinalScale = 1.0f
                        },
                        new DirectionModifier
                        {
                            InitialDirection = MathHelpers.RadianToVector2(angle),
                            FinalDirection = MathHelpers.DegreeToVector2(90, 2)
                        }
                    };

                    angle += MathHelpers.DegreeToRadian(1);
                }
                else
                {
                    break;
                }
            }
        }
    }
}