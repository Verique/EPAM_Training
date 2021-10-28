using System;
using System.Collections.Generic;
using SaveData;
using Stats;
using UnityEngine;

[RequireComponent(typeof(StatLoader))]
public class Health : MonoBehaviour, ISaveable<Tuple<int, int>>
{
    [SerializeField] private List<string> damageSourceTags;
    [SerializeField] private float invTime = 0;

    private float timeSinceDamageTaken;

    private const int MinHealth = 0;
    
    public event Action<int> HealthChanged;
    public event Action IsDead;

    private int currentHealth;
    
    public int CurrentHealth
    {
        get => currentHealth;

        private set
        {
            currentHealth = value;
            HealthChanged?.Invoke(currentHealth);
        }
    }

    public float InvTime => invTime;

    public int MaxHealth { get; private set; }

    private void OnEnable()
    {
        MaxHealth = GetComponent<StatLoader>().GetInt("health");
        CurrentHealth = MaxHealth;
    }

    private void TakeDamage(int damage)
    {
        if (Time.time - timeSinceDamageTaken < invTime)
            return;
        
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, MinHealth, MaxHealth);
        timeSinceDamageTaken = Time.time;
        if (CurrentHealth <= MinHealth) IsDead?.Invoke();
    }

    private void OnCollisionStay(Collision other)
    {
        var otherObj = other.gameObject;


        if (!damageSourceTags.Contains(otherObj.tag))
            return;

        if (otherObj.TryGetComponent(out DamageSource source)) TakeDamage(source.Damage);
    }

    public Tuple<int, int> GetSaveData()
    {
        return new Tuple<int, int>(CurrentHealth, MaxHealth);
    }

    public void LoadData(Tuple<int, int> data)
    {
        var (currentHealth, maxHealth) = data;
        CurrentHealth = currentHealth;
        MaxHealth = maxHealth;
    }
}