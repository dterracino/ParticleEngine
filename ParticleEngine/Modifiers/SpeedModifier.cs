using GameHelpersLib;

namespace ParticleEngine
{
    public class SpeedModifier : IModifier
    {
        public float InitialSpeed { get; set; }
        public float FinalSpeed { get; set; }

        public void Apply(Particle particle)
        {
            particle.Speed = MathHelpers.Lerp(InitialSpeed, FinalSpeed, particle.TimePercentage);
        }
    }
}