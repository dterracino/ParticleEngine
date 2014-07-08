using GameHelpersLib;

namespace ParticleEngine
{
    public class DirectionAngleModifier : IModifier
    {
        public float InitialMovementAngle { get; set; }
        public float MovementAngleOffset { get; set; }

        public void Apply(Particle particle)
        {
            float directionAngle = InitialMovementAngle + (MovementAngleOffset * particle.TimePercentage);
            particle.Direction = MathHelpers.RadianToVector2(directionAngle);
        }
    }
}