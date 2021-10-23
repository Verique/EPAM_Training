using System;
using Services;
using UnityEngine;

namespace UI
{
    public class PlayerHealthbar : UIBar
    {
        protected override void SetupBar()
        {
            base.SetupBar();
            var healthData = ServiceLocator.Instance.Get<PlayerManager>().Data.GetHealthData;
            MaxValue = healthData.CurrentHealth;
            healthData.HealthChanged += UpdateBarHeight;
        }
    }
}