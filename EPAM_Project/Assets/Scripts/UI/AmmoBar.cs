using Player;
using Services;
using Stats;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class AmmoBar : UIBar
    {
        private Image image;
        private readonly Color reloadColor = Color.black;
        private readonly Color defaultColor = new Color32(219, 164, 99, 255);
        private PlayerManager playerManager;
        private Stat<int> clipStat;
        
        protected override void SetupBar()
        {
            base.SetupBar();

            playerManager = ServiceLocator.Instance.Get<PlayerManager>();
            clipStat = playerManager.StatLoader.Stats.Clip;
            
            image = GetComponent<Image>();
            MaxValue = clipStat.maxValue;
            clipStat.ValueChanged += UpdateBarHeight;
            clipStat.MaxValueReached += () => ReloadIndication(false);
            ServiceLocator.Instance.Get<InputManager>().ReloadKeyUp += () => ReloadIndication(true);
        }

        private void ReloadIndication(bool reloading)
        {
            image.color = reloading ? reloadColor : defaultColor;
        }
    }
}