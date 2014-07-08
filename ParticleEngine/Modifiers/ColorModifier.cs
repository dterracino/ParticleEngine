using Microsoft.Xna.Framework;

namespace ParticleEngine
{
    public class ColorModifier : IModifier
    {
        public Vector3 InitialColor { get; set; }
        public Vector3 FinalColor { get; set; }

        public void Apply(Particle particle)
        {
            particle.Color = Vector3.Lerp(InitialColor, FinalColor, particle.TimePercentage);
        }
    }
}