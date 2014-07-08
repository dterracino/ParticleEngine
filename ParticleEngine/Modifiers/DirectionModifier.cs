using Microsoft.Xna.Framework;

namespace ParticleEngine
{
    public class DirectionModifier : IModifier
    {
        public Vector2 InitialDirection { get; set; }
        public Vector2 FinalDirection { get; set; }

        public void Apply(Particle particle)
        {
            particle.Direction = Vector2.Lerp(InitialDirection, FinalDirection, particle.TimePercentage);
        }
    }
}