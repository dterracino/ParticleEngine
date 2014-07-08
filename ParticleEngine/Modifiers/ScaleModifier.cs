using GameHelpersLib;

namespace ParticleEngine
{
    public class ScaleModifier : IModifier
    {
        public float InitialScale { get; set; }
        public float FinalScale { get; set; }

        public void Apply(Particle particle)
        {
            particle.Scale = MathHelpers.Lerp(InitialScale, FinalScale, particle.TimePercentage);
        }
    }
}