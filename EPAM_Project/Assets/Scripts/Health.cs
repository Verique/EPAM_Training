using System;
using System.Collections.Generic;
using Stats;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private List<string> damageSourceTags;
    [SerializeField] private float invTime = 0;

    public event Action<int> DamageTaken; 

    private Stat<int> healthStat;
    private float timeSinceDamageTaken;

    public void Init(Stat<int> health)
    {
        healthStat = health;
    }

    private void OnEnable()
    {
        if (healthStat != null)
        {
            healthStat.Value = healthStat.maxValue;
        }
    }

    private void TakeDamage(int damage)
    {
        if (Time.time - timeSinceDamageTaken < invTime) return;
        
        healthStat.Value -= damage;
        DamageTaken?.Invoke(damage);
        timeSinceDamageTaken = Time.time;
    }

    private void OnCollisionStay(Collision other)
    {
        var otherObj = other.gameObject;

        if (!damageSourceTags.Contains(otherObj.tag)) return;

        if (otherObj.TryGetComponent(out DamageSource source)) TakeDamage(source.Damage);
    }
}