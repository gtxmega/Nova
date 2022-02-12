using GameCore.Actors;
using UnityEngine;


namespace GameCore.Factory
{
    public abstract class AbstractFactory : MonoBehaviour
    {
        public abstract Actor Create(Actor actor, Vector3 position);
    }
}
