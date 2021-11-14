using System;
using Player.Weapons;
using Stats;
using UI;
using UnityEngine;

namespace Services
{
    public class UIManager : MonoBehaviour, IService
    {
        [SerializeField] private UIElement endGameStatsView;
        public event Action<GameStats> GameEnded;

        [SerializeField] private UIElement weaponIconView;
        public event Action<BaseWeapon, BaseWeapon, BaseWeapon> WeaponSwitched;
        
        [SerializeField] private UIElement levelView;
        public event Action<int> PlayerLevelUp;

        [SerializeField] private UIElement killView;
        public event Action<int, int> PlayerKills;

        [SerializeField] private UIElement playerHealthView;
        public event Action<Stat<int>> PlayerHealthChanged;

        [SerializeField] private UIElement playerExperienceView;
        public event Action<Stat<int>> PlayerExpChanged;
        
        [SerializeField] private UIElement playerAmmoView;
        public event Action<Stat<int>> PlayerAmmoChanged;
        public event Action<float, Stat<int>> PlayerReloading;

        [SerializeField] private UIElement bossHealthView;
        public event Action<string> BossSpawned;
        public event Action BossKilled;
        public event Action<Stat<int>> BossHealthChanged;

        private void Awake()
        {
            var weaponManager = ServiceLocator.Instance.Get<WeaponManager>();
            var playerManager = ServiceLocator.Instance.Get<PlayerManager>();
            var gameManager = ServiceLocator.Instance.Get<GameManager>();
            var enemyManager = ServiceLocator.Instance.Get<EnemyManager>();
            
            endGameStatsView.Init(this);
            gameManager.GameEnded += stats => GameEnded?.Invoke(stats);
            
            weaponIconView.Init(this);
            weaponManager.WeaponSwitched += (w1, w2, w3) => WeaponSwitched?.Invoke(w1, w2, w3);
            
            levelView.Init(this);
            playerManager.LevelUp += i => PlayerLevelUp?.Invoke(i);
            
            killView.Init(this);
            gameManager.EnemyKilled += (kills, goal) => PlayerKills?.Invoke(kills, goal);

            playerHealthView.Init(this);
            playerManager.PlayerHealthChanged += healthStat => PlayerHealthChanged?.Invoke(healthStat);

            playerExperienceView.Init(this);
            playerManager.PlayerGainedExp += expStat => PlayerExpChanged?.Invoke(expStat);

            playerAmmoView.Init(this);
            weaponManager.AmmoChanged += ammoStat => PlayerAmmoChanged?.Invoke(ammoStat);
            weaponManager.WeaponReloading += (reloadTime, ammoStat )=> PlayerReloading?.Invoke(reloadTime, ammoStat);

            bossHealthView.Init(this);
            enemyManager.BossSpawned += bossName => BossSpawned?.Invoke(bossName);
            enemyManager.BossKilled += () => BossKilled?.Invoke();
            enemyManager.BossHealthChanged += stat => BossHealthChanged?.Invoke(stat);
        }
    }
}