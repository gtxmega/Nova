using System.Collections;
using System.Collections.Generic;
using GameCore.Actors;
using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu(fileName = "Tower cards", menuName = "Towers/TowerCards/Tower cards")]
    public class TowerCards : ScriptableObject
    {
        [SerializeField] private string _towerName;
        [SerializeField] private string _towerDescription;
        [SerializeField] private Sprite _towerIcone;
        [SerializeField] private Actor _towerPrefab;
    }
}
