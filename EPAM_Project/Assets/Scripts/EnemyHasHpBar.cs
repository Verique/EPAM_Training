using System;
using UnityEngine;
using Services;
using UI;

[RequireComponent(typeof(Health))]
public class EnemyHasHpBar : MonoBehaviour
{
    private SpawnableUIManager infoManager;
    private readonly Vector3 offset = new Vector3(-30, 20, 0);
    private SpawnableUIManager.UIInfoPrefs prefs;
    private Health health;
    private EnemyHealthBar eBar;

    private void Start()
    {
        health = GetComponent<Health>();
        infoManager = ServiceLocator.Instance.Get<SpawnableUIManager>();
    }

    private void OnBecameVisible()
    {
        prefs = new SpawnableUIManager.UIInfoPrefs(transform, offset, EnemyHealthBar.PoolTag, health.MaxHealth);

        eBar = infoManager.Link<EnemyHealthBar, int>(prefs, out var action);
        health.HealthChanged += action;
    }

    private void OnBecameInvisible()
    {
        if (eBar != null)
            eBar.gameObject.SetActive(false);
    }
}
