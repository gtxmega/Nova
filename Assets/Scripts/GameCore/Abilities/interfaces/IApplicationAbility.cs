using GameCore.Actors;
using UnityEngine;

namespace GameCore.Abilities
{
    public interface IApplicationAbility
    {
        bool IsWaitApplyingAbility();

        bool CheckingPressedKeys();

        EAbilityStatus ConfirmAbility(Actor actor, Vector3 goundPoint);
        void CancelAbility();
    }
}
