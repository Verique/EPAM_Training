using System;
using System.Collections.Generic;
using SaveData;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float timeSinceDamageTaken;
    private HealthData healthData;

    public void Init(HealthData data)
    {
        healthData = data;
    }

    private void OnEnable()
    {
        if (healthData != null)
            healthData.CurrentHealth = healthData.maxHealth;
    }

    private void TakeDamage(int damage)
    {
        if (Time.time - timeSinceDamageTaken < healthData.invTime)
            return;

        timeSinceDamageTaken = Time.time;
        healthData.CurrentHealth -= damage;
    }

    private void OnCollisionStay(Collision other)
    {
        var otherObj = other.gameObject;


        if (!healthData.damageSourceTags.Contains(otherObj.tag))
            return;

        if (otherObj.TryGetComponent(out DamageSource source)) TakeDamage(source.Damage);
    }
}