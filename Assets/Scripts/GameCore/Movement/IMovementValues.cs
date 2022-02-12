

namespace GameCore.Movement
{
    public interface IMovementValues
    {
        public float MaxSpeed { get; }
        public float BaseSpeed { get; }

        public float BonusSpeed { get; }
        public float DecreasingSpeed { get; }
    }
}
