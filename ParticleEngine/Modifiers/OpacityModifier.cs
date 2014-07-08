using GameHelpersLib;

namespace ParticleEngine
{
    public class OpacityModifier : IModifier
    {
        public float InitialOpacity { get; set; }
        public float FinalOpacity { get; set; }

        public void Apply(Particle particle)
        {
            particle.Opacity = MathHelpers.Lerp(InitialOpacity, FinalOpacity, particle.TimePercentage);
        }
    }
}