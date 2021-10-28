using Services;

namespace UI
{
    public class PlayerHealthbar : UIBar
    {
        protected override void SetupBar()
        {
            base.SetupBar();
            var healthStat = ServiceLocator.Instance.Get<PlayerManager>().StatLoader.Stats.Health;
            MaxValue = healthStat.maxValue;
            healthStat.ValueChanged += UpdateBarHeight;
        }
    }
}