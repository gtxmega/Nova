using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameCore
{
    public interface IDamageable
    {
        Vector3 GetTargetPosition();
        void ApplyDamage(float amount, EDamageType damageType);
        bool IsDead();
    }
}
