using GameCore.Abilities;
using GameCore.Actors;
using GameCore.Movement;
using GameCore.Players;
using UnityEngine;
using Zenject;

namespace GameCore.InputControllers
{
    public class InputHandler : MonoBehaviour
    {
        [Inject] private IPlayer _player;
        [Inject] private ISelector _selector;

        private void Update()
        {
            var currentActor = GetCurrentActor();
            
            if (Input.GetMouseButtonDown(0))
            {
                if (ActorIsWaitApplyingAbility(currentActor))
                {
                    ConfirmActorAbility(currentActor);
                }
                else
                {
                    SelectActor();    
                }
                
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (ActorIsWaitApplyingAbility(currentActor))
                {
                    ActorCancelAbility(currentActor);
                }
                else
                {
                    if (_selector.ActorSelecting == null || _selector.ActorSelecting.GetPlayerOwner() != _player.PlayerID)
                    {
                        TrySwitchSelectingActorToMainActor();
                    }
                    else
                    {
                        if(_selector.ActorSelecting.TryGetComponent<IMovement>(out var movement))
                            movement.SetDestination(_selector.GetWorldPointByCursor());
                    }

                }
            }

            if (currentActor.TryGetComponent<IApplicationAbility>(out var applicationAbility))
            {
                applicationAbility.CheckingPressedKeys();
            }

        }

        private void SelectActor()
        {
            var selectEntity = _selector.TryGetActorByCursor();
            if (selectEntity != null)
            {
                _selector.SetSelectingActor(selectEntity);
            }
        }

        private bool ActorIsWaitApplyingAbility(Actor actor)
        {
            if (actor.TryGetComponent<IApplicationAbility>(out var applicationAbility))
            {
                return applicationAbility.IsWaitApplyingAbility();
            }

            return false;
        }

        private void ConfirmActorAbility(Actor actor)
        {
            if (actor.TryGetComponent<IApplicationAbility>(out var applicationAbility))
            {
                applicationAbility.ConfirmAbility(_selector.TryGetActorByCursor(), _selector.GetWorldPointByCursor());
            }
        }

        private void ActorCancelAbility(Actor actor)
        {
            if (actor.TryGetComponent<IApplicationAbility>(out var applicationAbility))
            {
                applicationAbility.CancelAbility();
            }
        }

        private Actor GetCurrentActor()
        {
            var selectingActor = _selector.ActorSelecting;
            var mainActor = _player.GetMainActor();

            return selectingActor != null ? selectingActor : mainActor;
        }

        private bool TrySwitchSelectingActorToMainActor()
        {
            var selectingActor = _selector.ActorSelecting;
            var mainActor = _player.GetMainActor();
            
            if (selectingActor != null && selectingActor != mainActor)
            {
                _selector.ActorSelecting.OnDeselect();
                _selector.SetSelectingActor(_player.GetMainActor());

                return true;
            }

            return false;
        }

    }
}
