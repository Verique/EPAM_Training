using Services;

namespace UI
{
    public class ExpBar : UIBar
    {
        protected override void SetupBar()
        {
            base.SetupBar();
            var expStat = ServiceLocator.Instance.Get<PlayerManager>().StatLoader.Stats.Experience;
            
            MaxValue = expStat.maxValue;
            UpdateBarHeight(expStat.Value);
            expStat.ValueChanged += UpdateBarHeight;
        }
    }
}
