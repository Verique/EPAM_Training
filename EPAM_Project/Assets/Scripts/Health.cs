using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private List<string> damageSourceTags;
    [SerializeField] private int maxHealth;

    private const int MinHealth = 0;
    private int currentHealth;

    public UnityAction<int> HealthChanged;

    private int CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            HealthChanged?.Invoke(value);
        }
    }
    public int MaxHealth => maxHealth;
    
    private void OnEnable()
    {
        CurrentHealth = maxHealth;
    }

    protected virtual void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, MinHealth, maxHealth);

        if (CurrentHealth == MinHealth) Kill();
    }

    protected virtual void Kill()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionStay(Collision other)
    {
        var otherObj = other.gameObject;


        if (!damageSourceTags.Contains(otherObj.tag))
            return;

        if (otherObj.TryGetComponent(out DamageSource source)) TakeDamage(source.Damage);
    }
}