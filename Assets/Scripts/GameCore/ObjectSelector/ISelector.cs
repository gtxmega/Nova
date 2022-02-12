using System;
using GameCore.Actors;
using UnityEngine;


namespace GameCore
{
    public interface ISelector
    {
        event Action<IInteractionObject> OnSelectingEntity;
        event Action<IInteractionObject> OnDeselectEntity;

        Actor ActorSelecting { get; }
        
        void SetSelectingActor(Actor actor);
        Actor TryGetActorByCursor();
        IInteractionObject TrySelectObjectByCursor();

        Vector3 GetWorldPointByCursor();
    }
}
