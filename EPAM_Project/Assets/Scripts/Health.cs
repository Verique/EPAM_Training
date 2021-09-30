using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private List<string> damageSourceTags;

    [SerializeField]
    private int maxHealth;

    private int minHealth = 0;
    private int currentHealth;

    public UnityAction<int> HEALTH_CHANGED;

    public int CurrentHealth
    {
        get { return currentHealth; }
        private set
        {
            currentHealth = value;
            HEALTH_CHANGED?.Invoke(value);
        }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
    }


    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    protected virtual void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, minHealth, maxHealth);

        if (CurrentHealth == minHealth)
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
