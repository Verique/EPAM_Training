using System;
using System.Collections.Generic;
using Stats;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private List<string> damageSourceTags;

    public event Action<int> DamageTaken; 

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

    public bool TakeDamage(int damage, GameObject damageSourceObject)
    {
        if (!damageSourceTags.Contains(damageSourceObject.tag)) return false;
        
        healthStat.Value -= damage;
        DamageTaken?.Invoke(damage);
        return true;
    }
}