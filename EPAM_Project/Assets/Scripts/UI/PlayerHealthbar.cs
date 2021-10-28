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
            var stats = ServiceLocator.Instance.Get<PlayerManager>().StatLoader.Stats;
            MaxValue = stats.Health.maxValue;
            stats.Health.ValueChanged += UpdateBarHeight;
        }
    }
}