using Stats;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class BossBar : UIBar<HUDManager>
    {
        [SerializeField] private GameObject barGO;
        [SerializeField] private Text bossNameText;

        private void SetActive(bool isActive)
        {
            barGO.SetActive(isActive);
        }

        private void OnBossSpawned(string bossName)
        {
            SetActive(true);
            bossNameText.text = bossName;
        }

        public override void Init(HUDManager manager)
        {
            manager.BossSpawned += OnBossSpawned;
            manager.BossHealthChanged += OnBossHealthChanged;
            manager.GameEnded += () => SetActive(false);
        }

        private void OnBossHealthChanged(Stat<int> healthStat)
        {
            MaxValue = healthStat.maxValue;
            UpdateBarWidth(healthStat.Value);
        }
    }
}