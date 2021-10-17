using Player;
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

        [SerializeField] private Weapon weapon;

        protected override void SetupBar()
        {
            base.SetupBar();
            image = GetComponent<Image>();
            MaxValue = weapon.ClipSize;
            weapon.BulletCountChanged += UpdateBarHeight;
            weapon.Reloading += ReloadIndication;
        }

        private void ReloadIndication(bool reloading)
        {
            image.color = reloading ? reloadColor : defaultColor;
        }
    }
}