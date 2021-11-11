using System;
using System.Collections.Generic;
using System.Linq;
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
        public BaseWeapon GetWeapon(int index) => weapons[ConvertIndex(index)];

        public event Action<int> WeaponSwitched;
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
            CurrentWeapon.PrepareToSwitch();
            currentWeaponIndex = index;
            CurrentWeapon.WeaponReloading += OnReload;
            WeaponSwitched?.Invoke(index);
        }

        private void OnReload(float reloadTime)
        {
            WeaponReloading?.Invoke(reloadTime);
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