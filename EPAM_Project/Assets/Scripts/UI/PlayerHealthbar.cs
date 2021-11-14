using Services;
using Stats;

namespace UI
{
    public class PlayerHealthbar : UIBar
    {
        public override void Init(UIManager manager)
        {
            manager.PlayerHealthChanged += OnPlayerHealthChanged;
        }

        private void OnPlayerHealthChanged(Stat<int> healthStat)
        {
            MaxValue = healthStat.maxValue;
            UpdateBarHeight(healthStat.Value);
        }
    }
}