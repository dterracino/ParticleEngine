using GameHelpersLib;

namespace ParticleEngine
{
    public class AngleModifier : IModifier
    {
        public float InitialAngle { get; set; }
        public float FinalAngle { get; set; }

        public void Apply(Particle particle)
        {
            particle.Angle = MathHelpers.Lerp(InitialAngle, FinalAngle, particle.TimePercentage);
        }
    }
}