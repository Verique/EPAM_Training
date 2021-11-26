using System.Collections;
using Player.Weapons;
using Stats;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    [RequireComponent(typeof(Image))]
    public class AmmoBar : UIBar<HUDManager>
    {
        private Coroutine reloadIndication;
        private Image image;
        private readonly Color reloadColor = Color.black;
        private readonly Color defaultColor = new Color32(219, 164, 99, 255);
        
        private void StartReloadIndication(float reloadTime, Stat<int> ammoStat)
        {
            reloadIndication = StartCoroutine(ReloadIndication(reloadTime, ammoStat));
        }

        private void OnAmmoChange(Stat<int> ammo)
        {
            MaxValue = ammo.maxValue;
            UpdateBarHeight(ammo.Value);
        }

        private IEnumerator ReloadIndication(float reloadTime, Stat<int> ammoStat)
        {
            image.color = reloadColor;
            var startTime = Time.time;
            var timeDiff = Time.time - startTime;

            var startAmmo = ammoStat.Value;
            var ammoDiff = ammoStat.maxValue - startAmmo;

            while (timeDiff <= reloadTime)
            {
                var ratio = timeDiff / reloadTime;
                image.color = Color.Lerp(reloadColor, defaultColor, ratio);
                UpdateBarHeight(startAmmo + Mathf.CeilToInt(ratio * ammoDiff));
                yield return new WaitForEndOfFrame();
                timeDiff = Time.time - startTime;
            }
        }
        
        public override void Init(HUDManager manager)
        {
            image = GetComponent<Image>();
            manager.PlayerAmmoChanged += OnAmmoChange;
            manager.PlayerReloading += StartReloadIndication;
            manager.WeaponSwitched += OnWeaponSwitched;
        }

        private void OnWeaponSwitched(BaseWeapon arg1, BaseWeapon arg2, BaseWeapon arg3)
        {
            if (reloadIndication != null) StopCoroutine(reloadIndication);
            image.color = defaultColor;
        }
    }
}