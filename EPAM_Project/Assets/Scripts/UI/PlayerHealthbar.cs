using UnityEngine;

namespace UI
{
    public class PlayerHealthBar : UIBar
    {
        [SerializeField] private Health health;

        protected override void SetupBar()
        {
            base.SetupBar();
            MaxValue = health.MaxHealth;
            health.HealthChanged += UpdateBar;
        }
    }
}