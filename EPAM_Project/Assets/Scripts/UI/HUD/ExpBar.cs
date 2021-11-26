using Stats;

namespace UI.HUD
{
    public class ExpBar : UIBar<HUDManager>
    {
        public override void Init(HUDManager manager)
        {
            manager.PlayerExpChanged += OnPlayerExpChanged;
        }

        private void OnPlayerExpChanged(Stat<int> expStat)
        {
            MaxValue = expStat.maxValue;
            UpdateBarHeight(expStat.Value);
        }
    }
}
