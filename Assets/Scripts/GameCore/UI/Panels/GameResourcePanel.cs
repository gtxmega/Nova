using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI.Panels
{
    public class GameResourcePanel : MonoBehaviour
    {
        [Header("UI panels")]
        [SerializeField] private GameObject _panelGold;
        [SerializeField] private GameObject _panelWave;
        [SerializeField] private GameObject _panelKillUnits;
        [SerializeField] private GameObject _panelLostUnits;

        [Header("UI text")]
        [SerializeField] private Text _textGold;
        [SerializeField] private Text _textWaves;
        [SerializeField] private Text _textKillUnits;
        [SerializeField] private Text _textLostUnits;

        public void SetGoldText(int gold)
        {
            _textGold.text = gold.ToString();
        }

        public void SetWaveText(int currentWave, int maxWave)
        {
            _textWaves.text = currentWave.ToString() + "/" + maxWave.ToString();
        }

        public void SetKillUnitsText(int currentKills, int maxKillsPerWave)
        {
            _textKillUnits.text = currentKills.ToString() + "/" + maxKillsPerWave.ToString();
        }

        public void SetLostUnitsText(int lostUnitsText)
        {
            _textLostUnits.text = lostUnitsText.ToString();
        }
    }
}
