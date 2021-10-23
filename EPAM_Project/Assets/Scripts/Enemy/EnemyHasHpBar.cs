using System;
using Services;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Health))]
    public class EnemyHasHpBar : MonoBehaviour
    {
        private SpawnableUIManager infoManager;
        private readonly Vector3 offset = new Vector3(-30, 20, 0);
        private SpawnableUIManager.UIInfoPrefs prefs;
        private Health health;
        private Action<int> action;
        
        private GameObject eBar;

        private void Start()
        {
            health = GetComponent<Health>();
            infoManager = ServiceLocator.Instance.Get<SpawnableUIManager>();
        }

        private void OnBecameVisible()
        {
            prefs = new SpawnableUIManager.UIInfoPrefs(transform, offset, health.MaxHealth);
            eBar = infoManager.Link(prefs, "hbar", out action);
            action(health.CurrentHealth);
            health.HealthChanged += action;
        }

        private void OnBecameInvisible()
        {
            if (eBar != null)
                eBar.SetActive(false);
            health.HealthChanged -= action;
        }
    }
}
