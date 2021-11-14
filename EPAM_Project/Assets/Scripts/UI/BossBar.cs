using Enemy;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BossBar : UIBar
    {
        [SerializeField] private Text bossName;
        [SerializeField] private GameObject barBG;
        protected override void SetupBar()
        {
            base.SetupBar();
            var eManager = ServiceLocator.Instance.Get<EnemyManager>();
            eManager.BossSpawned += OnBossSpawned;
        }

        private void SetActive(bool isActive)
        {
            barBG.SetActive(isActive);
            barGO.SetActive(isActive);
            bossName.gameObject.SetActive(isActive);
        }

        private void OnBossSpawned(Boss boss)
        {
            SetActive(true);
            MaxValue = boss.Health.maxValue;
            bossName.text = boss.PoolTag;
            UpdateBarHeight(boss.Health.Value);
            boss.Health.ValueChanged += UpdateBarWidth;
            boss.BossKilled += () => SetActive(false);
        }
    }
}