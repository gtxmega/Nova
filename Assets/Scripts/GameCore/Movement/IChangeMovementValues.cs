
namespace GameCore.Movement
{
    public interface IChangeMovementValues
    {
        void ChangeBaseMoveSpeed(float amount);
        void ChangeBonusMoveSpeed(float amount);

        void EncreasingMoveSpeed(float amount);
        void DecreasingMoveSpeed(float amount);
    }
}
