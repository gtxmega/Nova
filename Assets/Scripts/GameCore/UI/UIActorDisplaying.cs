using GameCore.Actors;
using GameCore.Attributes.Armor;
using GameCore.Attributes.Damage;
using GameCore.Attributes.Health;
using GameCore.Attributes.Mana;
using GameCore.Attributes.Specification;
using GameCore.UI.Panels;
using UnityEngine;


namespace GameCore.UI
{
    public class UIActorDisplaying : MonoBehaviour, UIDisplayingInfo
    {
        private ActorPanel _actorPanel;

        private IHealth _actorHealth;
        private IMana _actorMana;
        private IArmor _actorArmor;
        private IDamage _actorDamage;
        private ISpecification _actorSpecification;

        private void Awake()
        {
            _actorPanel = FindObjectOfType<ActorPanel>();
        }

        public void Displaying(Actor actor)
        {
            _actorSpecification = actor.GetComponent<ISpecification>();

            _actorPanel.SetNameText(actor.ActorName);

            TrySetAndShowHealth(actor);
            TrySetAndShowMana(actor);
            TrySetAndShowDamage(actor);
            TrySetAndShowArmor(actor);
            TrySetAndShowSpecification(actor);

            SubscribeMethodsOnAction();
            
            _actorPanel.EnableDisableActorInfoPanel(true);
        }

        private void TrySetAndShowHealth(Actor actor)
        {
            if (actor.TryGetComponent<IHealth>(out _actorHealth))
            {
                _actorPanel.SetHealthText(_actorHealth.CurrentHealth, _actorHealth.MaxHealth);
            }
            else
            {
                _actorPanel.SetHealthText(0, 0);
            }
        }

        private void TrySetAndShowMana(Actor actor)
        {
            if (actor.TryGetComponent<IMana>(out _actorMana))
            {
                _actorPanel.SetManaText(_actorMana.CurrentMana, _actorMana.MaxMana);
            }
            else
            {
                _actorPanel.SetManaText(0, 0);
            }
        }

        private void TrySetAndShowDamage(Actor actor)
        {
            if (actor.TryGetComponent<IDamage>(out _actorDamage))
            {
                _actorPanel.SetDamageText(_actorDamage.Damage, _actorDamage.BonusDamage, _actorDamage.DecreaseDamage);
                _actorPanel.EnableDisableAttackPanel(true);
            }
            else
            {
                _actorPanel.EnableDisableAttackPanel(false);
            }
        }

        private void TrySetAndShowArmor(Actor actor)
        {
            if (actor.TryGetComponent<IArmor>(out _actorArmor))
            {
                _actorPanel.SetArmorText(_actorArmor);
                _actorPanel.EnableDisableArmorPanel(true);
            }
            else
            {
                _actorPanel.EnableDisableArmorPanel(false);
            }
        }

        private void TrySetAndShowSpecification(Actor actor)
        {
            if (actor.TryGetComponent<ISpecification>(out _actorSpecification))
            {
                _actorPanel.SetSpecificationText(_actorSpecification.Specifications, _actorSpecification.BonusSpecifications);
                _actorPanel.EnableDisableSpecificationPanel(true);
            }
            else
            {
                _actorPanel.EnableDisableSpecificationPanel(false);
            }
        }

        public void HideDisplaying()
        {
            UnsubscribeMethodsOnAction();
            _actorPanel.EnableDisableActorInfoPanel(false);
        }

        private void SubscribeMethodsOnAction()
        {
            if(_actorHealth != null)
                _actorHealth.OnChangeHealth += _actorPanel.SetHealthText;

            if (_actorMana != null)
                _actorMana.OnChangeMana += _actorPanel.SetManaText;

            if(_actorArmor != null)
                _actorArmor.OnChangeArmor += _actorPanel.SetArmorText;

            if (_actorDamage != null)
                _actorDamage.OnChangeDamage += _actorPanel.SetDamageText;

            if (_actorSpecification != null)
                _actorSpecification.OnChangeSpecification += _actorPanel.SetSpecificationText;


        }

        private void UnsubscribeMethodsOnAction()
        {
            if(_actorHealth != null)
                _actorHealth.OnChangeHealth -= _actorPanel.SetHealthText;
            
            if (_actorMana != null)
                _actorMana.OnChangeMana -= _actorPanel.SetManaText;
            
            if (_actorArmor != null)
                _actorArmor.OnChangeArmor -= _actorPanel.SetArmorText;

            if (_actorDamage != null)
                _actorDamage.OnChangeDamage -= _actorPanel.SetDamageText;

            if (_actorSpecification != null)
                _actorSpecification.OnChangeSpecification -= _actorPanel.SetSpecificationText;
        }

    }
}
