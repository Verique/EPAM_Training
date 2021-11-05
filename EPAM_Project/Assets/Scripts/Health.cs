using System;
using System.Collections.Generic;
using Stats;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private List<string> damageSourceTags;
    [SerializeField] private float invTime;

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

    public void TakeDamage(int damage, GameObject damageSourceObject)
    {
        if (!damageSourceTags.Contains(damageSourceObject.tag)) return;
        if (Time.time - timeSinceDamageTaken < invTime) return;
        
        healthStat.Value -= damage;
        DamageTaken?.Invoke(damage);
        timeSinceDamageTaken = Time.time;
    }
}