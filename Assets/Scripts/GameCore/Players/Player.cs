using GameCore.Actors;
using GameCore.CustomDataStruct;
using UnityEngine;

namespace GameCore.Players
{
    public class Player : MonoBehaviour, IPlayer
    {
        public string PlayerName => _playerName;
        public EPlayerTeams Team => _team;
        public EPlayers PlayerID => _playerID;
        
        
        [SerializeField] private string _playerName;
        [SerializeField] private EPlayerTeams _team;
        [SerializeField] private EPlayers _playerID;

        [SerializeField] private Actor _mainActor;

        private void Awake()
        {
            _mainActor.InitActor(_playerID, _team);
        }

        public Actor GetMainActor()
        {
            return _mainActor;
        }

    }
}
