using GameCore.Attributes.Armor;
using GameCore.CustomDataStruct;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI.Panels
{
    public class ActorPanel : MonoBehaviour
    {
        [Header("UI Panels")]
        [SerializeField] private GameObject _actorPanel;

        [Header("Avatar panel")]
        [SerializeField] private GameObject _avatarPanel;
        [SerializeField] private Image _avatarActor;

        [Header("Health mana panel")]
        [SerializeField] private GameObject _healthManaPanel;
        [SerializeField] private Text _textHealth;
        [SerializeField] private Text _textMana;

        [Header("Attributes panel")]
        [SerializeField] private GameObject _attributesPanel;
        [Space]
        [SerializeField] private Text _textName;
        [SerializeField] private Text _textLevel;
        [Space]
        [SerializeField] private GameObject _statusPanel;

        [Header("Attack panel")]
        [SerializeField] private GameObject _attackPanel;
        [Space]
        [SerializeField] private Image _attackIcone;
        [SerializeField] private Text _textDamage;
        
        [Header("Armor panel")]
        [SerializeField] private GameObject _armorPanel;
        [Space]
        [SerializeField] private Image _armorImage;
        [SerializeField] private Text _textArmor;
        
        [Header("Specification panel")]
        [SerializeField] private GameObject _specificationPanel;
        [Space]
        [SerializeField] private Image _mainSpecificationImage;
        [SerializeField] private Text _textStrength;
        [SerializeField] private Text _textAgility;
        [SerializeField] private Text _textIntelligence;

        public void SetNameText(string textName)
        {
            _textName.text = textName;
        }

        public void SetHealthText(float current, float max)
        {
            if (current == 0 && max == 0)
            {
                _textMana.text = "";
                return;
            }
            
            _textHealth.text = Mathf.Round(current).ToString() + " / " + max.ToString();
        }

        public void SetManaText(float current, float max)
        {
            if (current == 0 && max == 0)
            {
                _textMana.text = "";
                return;
            }

            _textMana.text = Mathf.Round(current).ToString() + " / " + max.ToString();
        }

        public void SetDamageText(FloatRange damage, float bonusDamage, float decreaseDamage)
        {
            var baseDamageText = damage.Min.ToString() + " - " + damage.Max.ToString();
            _textDamage.text = baseDamageText + GetPositiveOrNegativeText(bonusDamage, decreaseDamage);
        }

        public void SetArmorText(IArmor entityArmor)
        {
            var baseArmorText = (entityArmor.Armor + entityArmor.BaseArmor).ToString();

            var armorText = baseArmorText + GetPositiveOrNegativeText(entityArmor.BonusArmor, entityArmor.DecreaseArmor);

            _textArmor.text = armorText;

        }

        public void SetSpecificationText(ThreeSpecification entitySpecification, ThreeSpecification entityBonusSpecification)
        {
            string strengthText = entitySpecification.Strength.ToString() + 
                                  GetPositiveOrNegativeText(entityBonusSpecification.Strength, 0);

            string agilityText = entitySpecification.Agility.ToString() + 
                                 GetPositiveOrNegativeText(entityBonusSpecification.Agility, 0);
            
            string intelligenceText = entitySpecification.Intelligence.ToString() + 
                                      GetPositiveOrNegativeText(entityBonusSpecification.Intelligence, 0);

            _textStrength.text = strengthText;
            _textAgility.text = agilityText;
            _textIntelligence.text = intelligenceText;
        }

        public void SetDamageIcone(Sprite sprite)
        {
            ChangeSprite(_attackIcone, sprite);
        }

        public void SetArmorIcone(Sprite sprite)
        {
            ChangeSprite(_armorImage, sprite);
        }

        public void SetSpecificationIcone(Sprite sprite)
        {
            ChangeSprite(_mainSpecificationImage, sprite);
        }

        public void EnableDisableActorInfoPanel(bool isEnable)
        {
            EnableDisablePanel(_actorPanel, isEnable);
        }

        public void EnableDisableHealthPanel(bool isEnable)
        {
            EnableDisablePanel(_healthManaPanel, isEnable);
        }

        public void EnableDisableAttackPanel(bool isEnable)
        {
            EnableDisablePanel(_attackPanel, isEnable);
        }

        public void EnableDisableArmorPanel(bool isEnable)
        {
            EnableDisablePanel(_armorPanel, isEnable);
        }

        public void EnableDisableSpecificationPanel(bool isEnable)
        {
            EnableDisablePanel(_specificationPanel, isEnable);
        }

        private void EnableDisablePanel(GameObject panel, bool isEnable)
        {
            panel.SetActive(isEnable);
        }

        private void ChangeSprite(Image image, Sprite sprite)
        {
            image.sprite = sprite;
        }


        private string GetPositiveOrNegativeText(float numberA, float numberB)
        {
            var total = numberA + numberB;
            var totalText = "<color=#51CE2B> +" + total.ToString() + "</color>";

            if (total == 0)
            {
                totalText = "";
            }
            else if (total < 0)
            {
                totalText = "<color=#B32134> -" + total.ToString() + "</color>";
            }

            return totalText;
        }

    }
}
