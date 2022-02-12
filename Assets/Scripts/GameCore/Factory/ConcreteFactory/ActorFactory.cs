using GameCore.Actors;
using UnityEngine;

using Zenject;

namespace GameCore.Factory
{
    public class ActorFactory : AbstractFactory
    {
        [Inject] DiContainer _diContainer;

        public override Actor Create(Actor actor, Vector3 position)
        {
            var actorInstance = _diContainer.InstantiatePrefab(actor.gameObject, position, Quaternion.identity, null);
            return actorInstance.GetComponent<Actor>();
        }
    }
}
