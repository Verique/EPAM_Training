using System;
using System.Collections.Generic;
using System.Linq;
using Player.Weapons;
using Stats;
using UnityEngine;

namespace Services
{
    public class WeaponManager : MonoBehaviour, IService
    {
        [SerializeField] private List<GameObject> weaponPrefabs;

        private List<BaseWeapon> weapons;
        private int currentWeaponIndex;
        private InputManager inputManager;
        private GameManager gameManager;

        private BaseWeapon CurrentWeapon => weapons[currentWeaponIndex];
        private BaseWeapon GetWeapon(int index) => weapons[ConvertIndex(index)];

        public event Action<BaseWeapon, BaseWeapon, BaseWeapon> WeaponSwitched;
        public event Action<Stat<int>> AmmoChanged;
        public event Action<float, Stat<int>> WeaponReloading;

        private void Awake()
        {
            weapons = new List<BaseWeapon>();
            currentWeaponIndex = 0;
        }

        private void Start()
        {
            gameManager = ServiceLocator.Instance.Get<GameManager>();
            inputManager = ServiceLocator.Instance.Get<InputManager>();

            inputManager.MouseScrolled += OnMouseScrolled;
            inputManager.ReloadKeyUp += ReloadCurrentWeapon;
            inputManager.PrimaryFire += FireCurrentWeapon;
            inputManager.WeaponOne += () => SwitchWeapon(0);
            inputManager.WeaponTwo += () => SwitchWeapon(1);
            inputManager.WeaponThree += () => SwitchWeapon(2);
        }

        public void BindToPlayerHand(Transform handTransform)
        {
            foreach (var weaponGameObject in weaponPrefabs.Where(go => go.TryGetComponent(out BaseWeapon _)))
            {
                var go = Instantiate(weaponGameObject, handTransform);
                weapons.Add(go.GetComponent<BaseWeapon>());
            }

            if (weapons.Count > 0)
            {
                SwitchWeapon(0);
            }
        }

        private void ReloadCurrentWeapon()
        {
            if (gameManager.State == GameState.Default) CurrentWeapon.Reload();
        }

        private void FireCurrentWeapon(Vector3 destination)
        {
            if (gameManager.State == GameState.Default) CurrentWeapon.Fire(destination);
        }

        private void SwitchWeapon(int index)
        {
            CurrentWeapon.WeaponReloading -= OnReload;
            CurrentWeapon.ClipStat.ValueChanged -= OnAmmoChanged;
            CurrentWeapon.PrepareToSwitch();
            currentWeaponIndex = index;
            CurrentWeapon.WeaponReloading += OnReload;
            CurrentWeapon.ClipStat.ValueChanged += OnAmmoChanged;
            OnAmmoChanged(0);
            WeaponSwitched?.Invoke(
                GetWeapon(currentWeaponIndex - 1),
                GetWeapon(currentWeaponIndex),
                GetWeapon(currentWeaponIndex + 1));
        }

        private void OnReload(float reloadTime)
        {
            WeaponReloading?.Invoke(reloadTime, CurrentWeapon.ClipStat);
        }

        private void OnAmmoChanged(int ammo)
        {
            AmmoChanged?.Invoke(CurrentWeapon.ClipStat);
        }

        private void OnMouseScrolled(float scrollDelta)
        {
            var newIndex = ConvertIndex(currentWeaponIndex + (int) scrollDelta);
            SwitchWeapon(newIndex);
        }

        private int ConvertIndex(int index)
        {
            var tmpIndex = index % weapons.Count;
            return index < 0 ? weapons.Count + tmpIndex : tmpIndex;
        }
    }
}