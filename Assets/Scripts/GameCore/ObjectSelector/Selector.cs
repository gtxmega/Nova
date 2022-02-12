using System;
using GameCore.Actors;
using UnityEngine;

namespace GameCore.ObjectSelector
{
    public class Selector : MonoBehaviour, ISelector
    {
        public event Action<IInteractionObject> OnSelectingEntity;
        public event Action<IInteractionObject> OnDeselectEntity;

        public Actor ActorSelecting => _actorSelecting;

        [SerializeField] private LayerMask _layersForSelect;
        [SerializeField] private LayerMask _layerGround;
        [SerializeField] private float _maxRayCastDistance;

        private IInteractionObject _entitySelecting;
        private Actor _actorSelecting;

        private Camera _rayCastCamera;

        private void Awake()
        {
            _rayCastCamera = Camera.main;
        }

        public void SetSelectingActor(Actor actor)
        {
            if(_entitySelecting != null) _entitySelecting.OnDeselect();
            
            if(actor.TryGetComponent<IInteractionObject>(out _entitySelecting))
            {
                _actorSelecting = actor;
                _entitySelecting.OnSelect();
                
                OnSelectingEntity?.Invoke(_entitySelecting);
            }
        }

        public IInteractionObject TrySelectObjectByCursor()
        {
            var actor = TryGetActorByCursor();
            if(actor.TryGetComponent<IInteractionObject>(out var entitySelecting))
            {
                SetSelectingActor(actor);
                return entitySelecting;
            }

            return null;
        }

        public Actor TryGetActorByCursor()
        {
            var ray = _rayCastCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hitResult, _maxRayCastDistance, _layersForSelect))
            {
                return hitResult.collider.GetComponent<Actor>();
            }

            return null;
        }

        public Vector3 GetWorldPointByCursor()
        {
            var worldPoint = Vector3.zero;
            
            var ray = _rayCastCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitResult, _maxRayCastDistance, _layerGround))
            {
                worldPoint = hitResult.point;
            }

            return worldPoint;
        }

        public void DeselectEntity()
        {
            if (_entitySelecting != null)
            {
                _entitySelecting.OnDeselect();
                OnDeselectEntity?.Invoke(_entitySelecting);

                _entitySelecting = null;
            }
        }
    }
}
