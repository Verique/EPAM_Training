using System.Collections;
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
        private BaseWeapon currentWeapon;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        protected override void SetupBar()
        {
            base.SetupBar();

            var weaponManager = ServiceLocator.Instance.Get<WeaponManager>();

            weaponManager.WeaponSwitched += OnWeaponSwitched;
            weaponManager.WeaponReloading += StartReloadIndication;
        }

        private void StartReloadIndication(float reloadTime)
        {
            StartCoroutine(nameof(ReloadIndication), reloadTime);
        }

        private void OnWeaponSwitched(BaseWeapon newWeapon)
        {
            if (currentWeapon != null)
            {
                currentWeapon.ClipStat.ValueChanged -= UpdateBarHeight;
            }
            
            MaxValue = newWeapon.ClipStat.maxValue;
            UpdateBarHeight(newWeapon.ClipStat.Value);
            newWeapon.ClipStat.ValueChanged += UpdateBarHeight;

            currentWeapon = newWeapon;
        }

        private IEnumerator ReloadIndication(float reloadTime)
        {
            image.color = reloadColor;
            var startTime = Time.time;
            var timeDiff = Time.time - startTime;

            var startAmmo = currentWeapon.ClipStat.Value;
            var ammoDiff = currentWeapon.ClipStat.maxValue - startAmmo;

            while (timeDiff < reloadTime)
            {
                var ratio = timeDiff / reloadTime;
                image.color = Color.Lerp(reloadColor, defaultColor, ratio);
                UpdateBarHeight(startAmmo + (int)(ratio * ammoDiff));
                yield return new WaitForEndOfFrame();
                timeDiff = Time.time - startTime;
            }
        }
    }
}