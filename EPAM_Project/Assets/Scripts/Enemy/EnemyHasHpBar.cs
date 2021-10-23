using System;
using SaveData;
using Services;
using Stats;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyDataLoader))]
    public class EnemyHasHpBar : MonoBehaviour
    {
        private SpawnableUIManager infoManager;
        private readonly Vector3 offset = new Vector3(-30, 20, 0);
        private SpawnableUIManager.UIInfoPrefs prefs;
        private GameObject eBar;
        private Action<int> action;
        private EnemyData eData;

        private void Start()
        {
            eData = GetComponent<EnemyDataLoader>().Data;
            infoManager = ServiceLocator.Instance.Get<SpawnableUIManager>();
        }

        private void OnBecameVisible()
        {
            prefs = new SpawnableUIManager.UIInfoPrefs(transform, offset, eData.HealthData.maxHealth);
            eBar = infoManager.Link(prefs, "hbar", out action);
            action(eData.HealthData.CurrentHealth);
            eData.HealthData.HealthChanged += action;
        }

        private void OnBecameInvisible()
        {
            if (eBar != null)
                eBar.SetActive(false);
            eData.HealthData.HealthChanged -= action;
        }
    }
}
