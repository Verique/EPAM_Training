using System;
using System.Collections.Generic;
using Stats;
using UnityEngine;

[RequireComponent(typeof(StatLoader))]
public class Health : MonoBehaviour
{
    [SerializeField] private List<string> damageSourceTags;
    [SerializeField] private float invTime = 0;

    private float timeSinceDamageTaken;
    private int maxHealth;

    private const int MinHealth = 0;
    
    public event Action<int> DamageTaken;
    public event Action IsDead;
    public int CurrentHealth { get; private set; }
    public int MaxHealth => maxHealth;
    
    private void OnEnable()
    {
        maxHealth = GetComponent<StatLoader>().Stats.GetInt("health");
        CurrentHealth = maxHealth;
    }

    private void TakeDamage(int damage)
    {
        if (Time.time - timeSinceDamageTaken < invTime)
            return;
        
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, MinHealth, maxHealth);
        timeSinceDamageTaken = Time.time;
        DamageTaken?.Invoke(CurrentHealth);
        if (CurrentHealth <= MinHealth) IsDead?.Invoke();
    }

    private void OnCollisionStay(Collision other)
    {
        var otherObj = other.gameObject;


        if (!damageSourceTags.Contains(otherObj.tag))
            return;

        if (otherObj.TryGetComponent(out DamageSource source)) TakeDamage(source.Damage);
    }
}