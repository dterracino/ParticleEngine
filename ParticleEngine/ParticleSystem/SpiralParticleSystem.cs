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
            AddTextureNames("Particle006");
        }

        public override void Emit(Vector2 emitterLocation)
        {
            for (int i = 0; i < 100; i++)
            {
                Particle p = GetFreeParticle();

                if (p != null)
                {
                    p.Position = emitterLocation;
                    p.Direction = MathHelpers.RadianToVector2(MathHelpers.Random.NextAngle());
                    p.Speed = 500;
                    p.Lifetime = MathHelpers.Random.NextFloat(1.0f, 2.0f);
                    p.Angle = MathHelpers.Random.NextAngle();
                    p.AutoAngle = true;
                    p.RectangleLimit = GraphicsDevice.Viewport.Bounds;
                    p.RectangleLimitAction = ParticleRectangleLimitAction.Bounce;
                    p.Scale = MathHelpers.Random.NextFloat(0.1f, 1f);
                    p.Color = MathHelpers.Random.NextColor(Color.DarkBlue, Color.CornflowerBlue).ToVector3();
                    p.Modifiers = new List<IModifier> {
                        new DirectionModifier
                        {
                            InitialDirection = MathHelpers.RadianToVector2(angle),
                            FinalDirection = MathHelpers.DegreeToVector2(90, 2)
                        },
                        new OpacityModifier
                        {
                            InitialOpacity = 1,
                            FinalOpacity = 0
                        }
                    };

                    angle += MathHelpers.DegreeToRadian(0.5f);
                }
                else
                {
                    break;
                }
            }
        }
    }
}