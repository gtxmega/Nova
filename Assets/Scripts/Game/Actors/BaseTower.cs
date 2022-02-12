using UnityEngine;

using Zenject;
using GameCore;
using GameCore.Actors;
using GameCore.Attributes.Armor;
using GameCore.Attributes.Health;
using GameCore.UI;

public class BaseTower : Actor, IInteractionObject
{
    public int TowerLevel { get { return _towerLevel; } private set { } }


    [SerializeField] private int _towerLevel;

    [Inject] private IHealth _health;
    [Inject] private IArmor _armor;

    #region MonoBehavior methods

    protected override void Awake()
    {
        base.Awake();

        _armor.RecalculateArmor(_towerLevel);
    }

    public virtual void Update()
    {
        _health.RegenerationTick();
    }

    #endregion
    

}
