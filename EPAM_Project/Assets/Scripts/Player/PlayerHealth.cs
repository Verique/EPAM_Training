using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField]
    private float invTime;
    bool isDamageable = true;

    protected override void TakeDamage(int damage)
    {
        if (isDamageable)
        {
            base.TakeDamage(damage);
            StartCoroutine("PlayerDamageTaken");

            Debug.Log(string.Format("Player has taken damage! Remaining health : {0}", currentHealth));
        }
    }

    private IEnumerator PlayerDamageTaken()
    {
        isDamageable = false;
        yield return new WaitForSecondsRealtime(invTime);
        isDamageable = true;
    }
}

