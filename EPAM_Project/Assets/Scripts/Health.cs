using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    private int minHealth = 0;
    protected int currentHealth;

    [SerializeField]
    private List<string> damageSourceTags;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    protected virtual void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, minHealth, maxHealth);

        if (currentHealth == minHealth)
        {
            Kill();
        }
    }

    private void Kill()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionStay(Collision other)
    {
        GameObject otherObj = other.gameObject;

        if (!damageSourceTags.Contains(otherObj.tag))
            return;

        if (otherObj.TryGetComponent(out DamageSource source))
        {
            TakeDamage(source.Damage);
        }
    }
}
