using System;
using System.Collections.Generic;
using Player;
using Player.Weapons;
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

        public event Action<BaseWeapon> WeaponSwitched;
        public event Action<float> WeaponReloading;

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
            inputManager.LmbHold += FireCurrentWeapon;
        }

        public void BindToPlayerHand(Transform handTransform)
        {
            foreach (var weaponGamePbject in weaponPrefabs)
            {
                if (!weaponGamePbject.TryGetComponent<BaseWeapon>(out var s)) continue;
                var go = Instantiate(weaponGamePbject, handTransform);
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

        private void FireCurrentWeapon()
        {
            if (gameManager.State == GameState.Default) CurrentWeapon.Fire();
        }

        private void SwitchWeapon(int index)
        {
            CurrentWeapon.WeaponReloading -= OnReload;
            currentWeaponIndex = index;
            CurrentWeapon.WeaponReloading += OnReload;
            WeaponSwitched?.Invoke(CurrentWeapon);
        }

        private void OnReload(float reloadTime)
        {
            WeaponReloading?.Invoke(reloadTime);
        }

        private void OnMouseScrolled(float scrollDelta)
        {
            var newIndex = (currentWeaponIndex + (int) scrollDelta + weapons.Count) % weapons.Count;
            Debug.Log(newIndex);
            SwitchWeapon(newIndex);
        }
    }
}