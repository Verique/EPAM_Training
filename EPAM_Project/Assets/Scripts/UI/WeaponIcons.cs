using Player.Weapons;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WeaponIcons : UIElement
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

        public override void Init(UIManager manager)
        {
            manager.WeaponSwitched += OnWeaponSwitched;
        }
    }
}