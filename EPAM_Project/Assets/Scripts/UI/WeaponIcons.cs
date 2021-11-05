using Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WeaponIcons : MonoBehaviour
    {
        [SerializeField] private Image current;
        [SerializeField] private Image next;
        [SerializeField] private Image last;

        private WeaponManager weaponManager;

        private void Start()
        {
            weaponManager = ServiceLocator.Instance.Get<WeaponManager>();

            weaponManager.WeaponSwitched += OnWeaponSwitched;
        }

        private void OnWeaponSwitched(int index)
        {
            current.sprite = weaponManager.GetWeapon(index).WeaponIcon;
            next.sprite = weaponManager.GetWeapon(index + 1).WeaponIcon;
            last.sprite = weaponManager.GetWeapon(index - 1).WeaponIcon;
        }
    }
}