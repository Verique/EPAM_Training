using Services;
using UnityEngine;

namespace UI
{
    public class PlayerHealthbar : UIBar
    {
        protected override void SetupBar()
        {
            base.SetupBar();
            var healthStat = ServiceLocator.Instance.Get<PlayerManager>().StatLoader.Stats.Health;
            MaxValue = healthStat.maxValue;
            UpdateBarHeight(healthStat.Value);
            healthStat.ValueChanged += UpdateBarHeight;
        }
    }
}