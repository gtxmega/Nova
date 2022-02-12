using System;
using UnityEngine;

using Zenject;
using GameCore;
using GameCore.Actors;
using GameCore.Factory;

public class ActorSpawner : MonoBehaviour, IActorSpawner
{
    public event Action<Actor> ActionSpawnedActor;
    public Actor _unitPrefab;

    [Inject] private AbstractFactory _actorFactory;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnActor(_unitPrefab);
        }
    }

    public Actor SpawnActor(Actor actor)
    {
        return SpawnActor(actor, Vector3.zero);
    }

    public Actor SpawnActor(Actor actor, Vector3 position)
    {
        return SpawnActorByFactory(_actorFactory, actor, position);
    }

    public Actor SpawnActorByFactory(AbstractFactory factory, Actor actor, Vector3 position)
    {
        var actorInstance = factory.Create(actor, position);
        ActionSpawnedActor?.Invoke(actorInstance);

        return actorInstance;
    }

}
