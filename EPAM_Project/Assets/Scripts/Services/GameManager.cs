using System;
using SaveData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services
{
    public class GameManager : MonoBehaviour, IService
    {
        [SerializeField] private int killGoal = 4;

        private SoundManager soundManager;
        private int kills;
        private float time;

        public event Action<int, int> EnemyKilled;
        public event Action<int> GoalReached; 
        public event Action<GameStats> GameEnded;
        public event Action GamePaused;
        public event Action GameResumed;

        private int Kills
        {
            get => kills;
            set
            {
                kills = value;
                EnemyKilled?.Invoke(Kills, killGoal);
                if (Kills != killGoal) return;
                
                GoalReached?.Invoke(killGoal);
                soundManager.PlayMusic("bossMusic");
            }
        }

        public GameState State { get; private set; }

        private void Start()
        {
            soundManager = ServiceLocator.Instance.Get<SoundManager>();
            StartGame();
        }

        private void StartGame()
        { 
            State = GameState.Setup;
            
            var playerManager = ServiceLocator.Instance.Get<PlayerManager>();
            var cameraManager = ServiceLocator.Instance.Get<CameraManager>();
            var enemyManager = ServiceLocator.Instance.Get<EnemyManager>();
            var inputManager = ServiceLocator.Instance.Get<InputManager>();
            var saveManager = ServiceLocator.Instance.Get<SaveManager>();
            
            inputManager.PauseKeyUp += PauseResumeToggle;
            
            cameraManager.Target = playerManager.PlayerTarget;
            
            enemyManager.Target = playerManager.PlayerTarget;
            enemyManager.OnGameStart();
            enemyManager.EnemyKilledExp += exp => Kills++;

            enemyManager.BossKilled += GameWon;
            playerManager.StatLoader.Stats.Health.MinValueReached += GameLost;
            
            soundManager.PlayMusic("defaultMusic");

            EnemyKilled?.Invoke(Kills, killGoal);

            var saveName = PlayerPrefs.GetString("saveName");
            if (saveName != "") saveManager.Load(saveName);
            
            State = GameState.Default;
        }

        public void PauseResumeToggle()
        {
            switch (State)
            {
                case GameState.Default:
                    State = GameState.Pause;
                    GamePaused?.Invoke();
                    break;
                
                case GameState.Pause:
                    State = GameState.Default;
                    GameResumed?.Invoke();
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
            GameEnded?.Invoke(new GameStats(Kills, Time.timeSinceLevelLoad + time, message));
        }
        
        private void GameWon() => EndGame("You win!");
        private void GameLost() => EndGame("You are dead!");

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
            time = data.time;
            EnemyKilled?.Invoke(kills, killGoal);
        }
    }
}