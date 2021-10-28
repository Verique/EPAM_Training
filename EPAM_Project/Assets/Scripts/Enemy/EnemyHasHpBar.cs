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
        private SpawnableUIManager.UIInfoPrefs prefs;
        
        private EnemyStats stats;
        private Action<int> action;
        
        private GameObject eBar;

        private void Start()
        {
            stats = GetComponent<EnemyStatLoader>().Stats;
            infoManager = ServiceLocator.Instance.Get<SpawnableUIManager>();
        }

        private void OnBecameVisible()
        {
            prefs = new SpawnableUIManager.UIInfoPrefs(transform, offset, stats.Health.maxValue);
            eBar = infoManager.Link(prefs, "hbar", out action);
            action(stats.Health.Value);
            stats.Health.ValueChanged += action;
        }

        private void OnBecameInvisible()
        {
            if (eBar != null) eBar.SetActive(false);
            
            stats.Health.ValueChanged -= action;
        }
    }
}
