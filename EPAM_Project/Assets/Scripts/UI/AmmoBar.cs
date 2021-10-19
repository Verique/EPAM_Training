using Player;
using Services;
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
        
        protected override void SetupBar()
        {
            base.SetupBar();

            playerManager = ServiceLocator.Instance.Get<PlayerManager>();
            image = GetComponent<Image>();
            MaxValue = playerManager.ClipSize;
            playerManager.BulletCountChanged += UpdateBarHeight;
            playerManager.Reloading += ReloadIndication;
        }

        private void ReloadIndication(bool reloading)
        {
            image.color = reloading ? reloadColor : defaultColor;
        }
    }
}