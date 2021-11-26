using Stats;

namespace UI.HUD
{
    public class PlayerHealthbar : UIBar<HUDManager>
    {
        public override void Init(HUDManager manager)
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