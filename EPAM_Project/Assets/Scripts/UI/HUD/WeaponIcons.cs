using Player.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class WeaponIcons : UIElement<HUDManager>
    {
        [SerializeField] private Image current;
        [SerializeField] private Image next;
        [SerializeField] private Image last;

        private void OnWeaponSwitched(BaseWeapon lastW, BaseWeapon currentW, BaseWeapon nextW)
        {
            current.sprite = currentW.WeaponIcon;
            next.sprite = nextW.WeaponIcon;
            last.sprite = lastW.WeaponIcon;
        }

        public override void Init(HUDManager manager)
        {
            manager.WeaponSwitched += OnWeaponSwitched;
        }
    }
}