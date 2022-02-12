using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameCore;
using GameCore.Actors;

[CreateAssetMenu(fileName = "Unit cards", menuName = "Units/UnitCards/Unit cards")]
public class UnitCards : ScriptableObject
{
    [SerializeField] private string _unitName;
    [SerializeField] private string _unitDescription;
    [SerializeField] private Actor _unitPrefab;

}
