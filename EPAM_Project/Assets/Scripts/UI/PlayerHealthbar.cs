using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PlayerHealthbar : MonoBehaviour
{
    private Vector2 initialBarSize;
    private RectTransform barTransform;

    [SerializeField]
    private Health health;
    private int maxHealth;

    private void Start()
    {
        barTransform = GetComponent<RectTransform>();
        initialBarSize = barTransform.sizeDelta;
        maxHealth = health.MaxHealth;
        health.HEALTH_CHANGED += UpdateHealthbar;
    }

    public void UpdateHealthbar(int healthPoints)
    {
        float height = initialBarSize.y * healthPoints / maxHealth;
        barTransform.sizeDelta = new Vector2(initialBarSize.x, height);
    }
}
