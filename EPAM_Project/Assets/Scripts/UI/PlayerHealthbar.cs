using UnityEngine;

namespace UI
{
    public class PlayerHealthbar : UIBar
    {
        [SerializeField] private Health health;

        protected override void SetupBar()
        {
            base.SetupBar();
            MaxValue = health.MaxHealth;
            health.HealthChanged += UpdateBarHeight;
        }
    }
}