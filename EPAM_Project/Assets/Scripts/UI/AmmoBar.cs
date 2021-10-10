using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class AmmoBar : UIBar
    {
        private readonly Color reloadColor = Color.black;
        private readonly Color defaultColor = new Color32(219, 164, 99, 255);

        [SerializeField] private Weapon weapon;

        protected override void SetupBar()
        {
            base.SetupBar();
            MaxValue = weapon.ClipSize;
            weapon.BulletCountChanged += UpdateBarHeight;
            weapon.Reloading += ReloadIndication;
        }

        private void ReloadIndication(bool reloading)
        {
            Image.color = reloading ? reloadColor : defaultColor;
        }
    }
}