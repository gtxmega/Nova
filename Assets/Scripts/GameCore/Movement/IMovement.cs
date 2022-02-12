using System;
using UnityEngine;

namespace GameCore.Movement
{
    public interface IMovement
    {
        event Action<bool> OnDestination;

        void SetDestination(Vector3 point);
        void StopMovement();
    }

}
