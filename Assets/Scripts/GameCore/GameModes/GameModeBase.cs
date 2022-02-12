using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class GameModeBase : MonoBehaviour, IGameMode
    {
        [Header("Units settings")]
        [SerializeField] private Transform[] _unitsMovePoints;

        public void RunGameMode()
        {

        }
    }
}
