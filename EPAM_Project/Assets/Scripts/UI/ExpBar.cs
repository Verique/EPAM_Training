using Services;
using Stats;

namespace UI
{
    public class ExpBar : UIBar
    {
        public override void Init(UIManager manager)
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
