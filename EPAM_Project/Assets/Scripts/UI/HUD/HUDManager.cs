using System;
using Player.Weapons;
using Services;
using Stats;
using UnityEngine;

namespace UI.HUD
{
    public class HUDManager : MonoBehaviour, IUIManager
    {
        public event Action GameEnded;
        
        [SerializeField] private UIElement<HUDManager> weaponIconView;
        public event Action<BaseWeapon, BaseWeapon, BaseWeapon> WeaponSwitched;
        
        [SerializeField] private UIElement<HUDManager> levelView;
        public event Action<int> PlayerLevelUp;

        [SerializeField] private UIElement<HUDManager> killView;
        public event Action<int, int> PlayerKills;

        [SerializeField] private UIElement<HUDManager> playerHealthView;
        public event Action<Stat<int>> PlayerHealthChanged;

        [SerializeField] private UIElement<HUDManager> playerExperienceView;
        public event Action<Stat<int>> PlayerExpChanged;
        
        [SerializeField] private UIElement<HUDManager> playerAmmoView;
        public event Action<Stat<int>> PlayerAmmoChanged;
        public event Action<float, Stat<int>> PlayerReloading;

        [SerializeField] private UIElement<HUDManager> bossHealthView;
        public event Action<string> BossSpawned;
        public event Action<Stat<int>> BossHealthChanged;

        [SerializeField] private UIElement<HUDManager> bossDirectionView;
        public event Action<Vector2> BossDirectionChanged;
        public event Action<Vector2> PlayerMovedOnScreen;

        private void Awake()
        {
            var weaponManager = ServiceLocator.Instance.Get<WeaponManager>();
            var playerManager = ServiceLocator.Instance.Get<PlayerManager>();
            var gameManager = ServiceLocator.Instance.Get<GameManager>();
            var enemyManager = ServiceLocator.Instance.Get<EnemyManager>();
            var cameraManager = ServiceLocator.Instance.Get<CameraManager>();

            weaponIconView.Init(this);
            levelView.Init(this);
            killView.Init(this);
            playerExperienceView.Init(this);
            playerAmmoView.Init(this);
            playerHealthView.Init(this);
            bossHealthView.Init(this);
            bossDirectionView.Init(this);
            
            gameManager.GameEnded += stats => GameEnded?.Invoke(); 
            gameManager.EnemyKilled += (kills, goal) => PlayerKills?.Invoke(kills, goal);
            playerManager.LevelUp += i => PlayerLevelUp?.Invoke(i);
            playerManager.PlayerHealthChanged += healthStat => PlayerHealthChanged?.Invoke(healthStat);
            playerManager.PlayerGainedExp += expStat => PlayerExpChanged?.Invoke(expStat);
            weaponManager.AmmoChanged += ammoStat => PlayerAmmoChanged?.Invoke(ammoStat);
            weaponManager.WeaponSwitched += (w1, w2, w3) => WeaponSwitched?.Invoke(w1, w2, w3);
            weaponManager.WeaponReloading += (reloadTime, ammoStat )=> PlayerReloading?.Invoke(reloadTime, ammoStat);
            enemyManager.BossSpawned += bossName => BossSpawned?.Invoke(bossName);
            enemyManager.BossHealthChanged += stat => BossHealthChanged?.Invoke(stat);
            enemyManager.BossDirection += vector => BossDirectionChanged?.Invoke(vector);
            cameraManager.TargetMovedOnScreen += vector => PlayerMovedOnScreen?.Invoke(vector);
        }
    }
}