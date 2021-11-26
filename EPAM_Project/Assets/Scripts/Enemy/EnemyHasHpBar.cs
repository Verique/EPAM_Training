using System;
using Services;
using Stats;
using UI.SpawnableUIElement;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyStatLoader))]
    public class EnemyHasHpBar : MonoBehaviour
    {
        private SpawnableUIManager sUIManager;
        private Stat<int> healthStat;
        private EnemyHealthBar bar;

        public event Action<Vector3> EnemyMoved;
        public event Action<Stat<int>> HealthChanged;

        private void Start()
        {
            healthStat = GetComponent<EnemyStatLoader>().Stats.Health;
            sUIManager = ServiceLocator.Instance.Get<SpawnableUIManager>();
            healthStat.ValueChanged += i => HealthChanged?.Invoke(healthStat);
        }

        private void Update()
        {
            EnemyMoved?.Invoke(transform.position);
        }

        private void OnBecameVisible()
        {
            bar = sUIManager.GetHpBar(this);
            HealthChanged?.Invoke(healthStat);
        }

        private void OnBecameInvisible()
        {
            sUIManager.ReleaseHpBar(bar);
        }
    }
}
