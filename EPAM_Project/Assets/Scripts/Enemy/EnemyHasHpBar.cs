using System;
using Services;
using Stats;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyStatLoader))]
    public class EnemyHasHpBar : MonoBehaviour
    {
        private readonly Vector3 offset = new Vector3(-30, 20, 0);
        
        private SpawnableUIManager infoManager;
        private Stat<int> healthStat;
        private Action<int> action;
        private GameObject eBar;

        private void Start()
        {
            healthStat = GetComponent<EnemyStatLoader>().Stats.Health;
            infoManager = ServiceLocator.Instance.Get<SpawnableUIManager>();
        }

        private void OnBecameVisible()
        {
            var prefs = new SpawnableUIManager.UIInfoPrefs(transform, offset, healthStat.maxValue);
            eBar = infoManager.Link(prefs, "hbar", out action);
            action(healthStat.Value);
            healthStat.ValueChanged += action;
        }

        private void OnBecameInvisible()
        {
            if (eBar != null) eBar.SetActive(false);
            healthStat.ValueChanged -= action;
        }
    }
}
