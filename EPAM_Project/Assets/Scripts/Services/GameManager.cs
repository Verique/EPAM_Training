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
        private float time;

        public event Action<int, int> EnemyKilled;
        public event Action<GameStats> GameEnded;

        private int Kills
        {
            get => kills;
            set
            {
                kills = value;
                EnemyKilled?.Invoke(Kills, killGoal);
                if (Kills != killGoal) return;
            
                enemyManager.SpawnBoss();
            }
        }

        public GameState State { get; private set; }

        public void AddKill()
        {
            Kills++;
        }

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
            playerManager.StatLoader.Stats.Health.MinValueReached += () => EndGame("You are dead!");

            ServiceLocator.Instance.Get<InputManager>().PauseKeyUp += Pause;
            
            cameraManager = ServiceLocator.Instance.Get<CameraManager>();
            cameraManager.Target = playerManager.PlayerTarget;
            
            enemyManager = ServiceLocator.Instance.Get<EnemyManager>();
            enemyManager.SetTarget(playerManager.PlayerTarget);
            enemyManager.OnGameStart();
            enemyManager.BossKilled += () => EndGame("You win!");

            EnemyKilled?.Invoke(Kills, killGoal);

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

        private void EndGame(string message)
        {
            State = GameState.GameOver;
            cameraManager.enabled = false;
            
            GameEnded?.Invoke(new GameStats(Kills, Time.timeSinceLevelLoad + time, message));
            
            enemyManager.OnGameEnd();
            gameOverScreen.SetActive(true);
        }

        public void ToMainMenu()
        {
            State = GameState.MainMenu;
            SceneManager.LoadScene("MainMenu");
        }

        public GameStateData GetSaveData()
        {
            var saveTime = Time.timeSinceLevelLoad + time;
            return new GameStateData(kills, saveTime);
        }
        
        public void LoadData(GameStateData data)
        {
            kills = data.kills;
            EnemyKilled?.Invoke(kills, killGoal);
            time = data.time;
        }
    }
}