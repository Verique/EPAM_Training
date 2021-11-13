using System;
using SaveData;
using UI.Menus;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services
{
    public class GameManager : MonoBehaviour, IService
    {
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private PauseScreen pauseScreen;
        [SerializeField] private int killGoal = 4;

        private EnemyManager enemyManager;
        private CameraManager cameraManager;
        private PlayerManager playerManager;

        private int kills;
        public event Action<int, int> EnemyKilled;

        public void AddKill()
        {
            kills++;
            
            EnemyKilled?.Invoke(kills, killGoal);
            if (kills != killGoal) return;
            
            var boss = enemyManager.SpawnBoss();
            boss.BossKilled += EndGame;
        }
        
        public GameState State { get; private set; }

        private void Awake()
        {
            State = GameState.Setup;
        }

        private void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            playerManager = ServiceLocator.Instance.Get<PlayerManager>();
            playerManager.StatLoader.Stats.Health.MinValueReached += EndGame;

            ServiceLocator.Instance.Get<InputManager>().PauseKeyUp += Pause;
            
            cameraManager = ServiceLocator.Instance.Get<CameraManager>();
            cameraManager.Target = playerManager.PlayerTarget;
            
            enemyManager = ServiceLocator.Instance.Get<EnemyManager>();
            enemyManager.SetTarget(playerManager.PlayerTarget);
            enemyManager.OnGameStart();

            EnemyKilled?.Invoke(0, killGoal);

            var saveName = PlayerPrefs.GetString("saveName");
            if (saveName != "")
                ServiceLocator.Instance.Get<SaveManager>().Load(saveName);
            
            State = GameState.Default;
        }

        public void Pause()
        {
            switch (State)
            {
                case GameState.Default:
                    State = GameState.Pause;
                    pauseScreen.SetActive(true);
                    break;
                
                case GameState.Pause:
                    State = GameState.Default;
                    pauseScreen.SetActive(false);
                    break;
            }
        }

        public void Restart()
        {
            PlayerPrefs.SetString("saveName", "");
            SceneManager.LoadScene("InitialScene");
        }

        public void RestartFromLastSave()
        {
            SceneManager.LoadScene("InitialScene");
        }

        private void EndGame()
        {
            State = GameState.GameOver;
            cameraManager.enabled = false;
            
            enemyManager.OnGameEnd();
            gameOverScreen.SetActive(true);
        }

        public void ToMainMenu()
        {
            State = GameState.MainMenu;
            SceneManager.LoadScene("MainMenu");
        }
    }
}