using UnityEngine;

using GameCore;
using GameCore.Actors;
using GameCore.Attributes.Armor;
using GameCore.Attributes.Damage;
using GameCore.Attributes.Expirience;
using GameCore.Attributes.Health;
using GameCore.Attributes.Mana;
using GameCore.Attributes.Specification;
using Zenject;
using GameCore.UI;

public class BaseUnit : Actor, IDamageable, IInteractionObject
{
    public int UnitLevel => _unitLevel;

    [Header("Unit level")]
    [SerializeField] private int _unitLevel;

    [Inject] private IHealth _health;
    [Inject] private IMana _mana;
    [Inject] private IDamage _damage;
    [Inject] private IArmor _armor;
    [Inject] private ISpecification _specification;
    [Inject] private IExpirience _expirience;
    
    
    #region MonoBehavior methods

    protected override void Awake()
    {
        base.Awake();

        _unitLevel = _expirience.CurrentLevel;
        _armor.RecalculateArmor(_unitLevel);
        _mana.ChangeMaxMana(50);

        _expirience.OnLevelUp += ChangeUnitLevel;
    }

    protected virtual void Update()
    {
        _health.RegenerationTick();
        _mana.RegenerationTick();
    }

    #endregion

    public virtual void ApplyDamage(float amount, EDamageType damageType)
    {
        var calculatedDamage = _armor.CalculateDamageByResistance(amount, damageType);
        _health.TryChangeCurrentHealth(-calculatedDamage);
    }

    public Vector3 GetTargetPosition()
    {
        return ActorPosition;
    }

    public virtual bool IsDead()
    {
        return _health.IsDead;
    }

    private void ChangeUnitLevel(int level)
    {
        _unitLevel = level;
    }




}
