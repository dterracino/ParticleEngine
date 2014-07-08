using GameHelpersLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ParticleEngine
{
    public class PlasmaParticleSystem : ParticleSystem
    {
        public PlasmaParticleSystem(Game game)
            : base(game)
        {
            AddTextureNames("Gradient");
        }

        public override void Emit(Vector2 emitterLocation)
        {
            for (int i = 0; i < 5; i++)
            {
                Particle p = GetFreeParticle();

                if (p != null)
                {
                    p.Position = emitterLocation;
                    p.Lifetime = MathHelpers.Random.NextFloat(1, 3);
                    p.Color = MathHelpers.Random.NextColor(Color.White, Color.CornflowerBlue).ToVector3();
                    p.Speed = MathHelpers.Random.Next(1, 300);
                    p.Scale = MathHelpers.Random.NextFloat(0.5f, 2f);
                    p.Direction = MathHelpers.RadianToVector2(MathHelpers.Random.NextAngle());
                    p.RectangleLimit = GraphicsDevice.Viewport.Bounds;
                    p.RectangleLimitAction = ParticleRectangleLimitAction.Bounce;
                    p.Modifiers = new List<IModifier> {
                        new OpacityModifier
                        {
                            InitialOpacity = 1,
                            FinalOpacity = 0
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