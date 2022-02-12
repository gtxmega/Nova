using System;
using UnityEngine;

using GameCore;
using GameCore.Actors;

public interface IActorSpawner
{
    event Action<Actor> ActionSpawnedActor;
    Actor SpawnActor(Actor actor);
    Actor SpawnActor(Actor actor, Vector3 position);
}
