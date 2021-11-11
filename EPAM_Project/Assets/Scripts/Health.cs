using System;
using System.Collections.Generic;
using Stats;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private List<string> damageSourceTags;

    public event Action<int> DamageTaken;
    public event Action<string> KilledBy;

    private Stat<int> healthStat;

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

    public bool TakeDamage(int damage, string dmgTag)
    {
        if (!damageSourceTags.Contains(dmgTag)) return false;
        
        healthStat.Value -= damage;
        
        if (healthStat.Value == healthStat.minValue) KilledBy?.Invoke(dmgTag);
        DamageTaken?.Invoke(damage);
        return true;
    }
}