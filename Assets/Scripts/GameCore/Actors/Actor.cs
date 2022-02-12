using GameCore.CustomDataStruct;
using UnityEngine;

using Zenject;
using GameCore.UI;

namespace GameCore.Actors
{
    public abstract class Actor : MonoBehaviour, IInteractionObject
    {
        public string ActorName => _actorName;
        public Vector3 ActorPosition => _transform.position;
        public EActorTypes ActorType => _actorType;
        public EPlayerTeams Team => _team;


        [SerializeField] private string _actorName;
        [SerializeField] private EActorTypes _actorType;
        [SerializeField] private EPlayerTeams _team;
        [SerializeField] private EPlayers _playerOwner;


        private Transform _transform;

        [Inject( Id = "ActorDisplaying")] protected UIDisplayingInfo _uiDisplayingInfo;
        [Inject( Id = "ActorAbilitiesDisplaying")] protected UIDisplayingInfo _uiActorAbilitiesDisplaying;

        public virtual void InitActor(EPlayers playerOwner, EPlayerTeams team)
        {
            _playerOwner = playerOwner;
            _team = team;
        }
        
        protected virtual void Awake()
        {
            _transform = GetComponent<Transform>();
        }


        public virtual void OnSelect()
        {
            _uiDisplayingInfo.Displaying(this);
            _uiActorAbilitiesDisplaying.Displaying(this);
        }

        public virtual void OnDeselect()
        {
            _uiDisplayingInfo.HideDisplaying();
            _uiActorAbilitiesDisplaying.HideDisplaying();
        }

        public Actor GetActor()
        {
            return this;
        }
        
        public EPlayers GetPlayerOwner()
        {
            return _playerOwner;
        }
    }
}
