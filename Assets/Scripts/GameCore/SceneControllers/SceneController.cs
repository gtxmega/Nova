using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour GameModeInterface;

        private IGameMode _gameMode;

        private void Awake()
        {
            _gameMode = GameModeInterface as IGameMode;
        }


    }

}
