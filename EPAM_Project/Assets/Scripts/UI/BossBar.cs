using Services;
using Stats;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BossBar : UIBar
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

        public override void Init(UIManager manager)
        {
            manager.BossSpawned += OnBossSpawned;
            manager.BossHealthChanged += OnBossHealthChanged;
            manager.BossKilled += () => SetActive(false);
            manager.GameEnded += (s) => SetActive(false);
        }

        private void OnBossHealthChanged(Stat<int> healthStat)
        {
            MaxValue = healthStat.maxValue;
            UpdateBarWidth(healthStat.Value);
        }
    }
}