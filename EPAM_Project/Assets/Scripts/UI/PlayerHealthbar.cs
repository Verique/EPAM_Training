using System;
using Services;
using UnityEngine;

namespace UI
{
    public class PlayerHealthbar : UIBar
    {
        private Health health;
        
        protected override void SetupBar()
        {
            base.SetupBar();
            health = ServiceLocator.Instance.Get<PlayerManager>().Health;
            MaxValue = health.MaxHealth;
            health.DamageTaken += UpdateBarHeight;
        }
    }
}