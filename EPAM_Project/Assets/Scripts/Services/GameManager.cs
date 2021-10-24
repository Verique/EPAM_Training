using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services
{
    public class GameManager : MonoBehaviour, IService
    {
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private PauseScreen pauseScreen;

        private EnemyManager enemyManager;
        private CameraManager cameraManager;
        private PlayerManager playerManager;
        private Transform playerTransform;
        
        
        private void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            playerManager = ServiceLocator.Instance.Get<PlayerManager>();
            playerTransform = playerManager.Transform;
            playerTransform.GetComponent<Health>().IsDead += EndGame;

            ServiceLocator.Instance.Get<InputManager>().PauseKeyUp += Pause;
            
            cameraManager = ServiceLocator.Instance.Get<CameraManager>();
            cameraManager.Target = playerTransform;
            
            enemyManager = ServiceLocator.Instance.Get<EnemyManager>();
            enemyManager.StartSpawning();
            
            foreach (var enemy in enemyManager.Enemys)
            {
                enemy.GetComponent<Enemy.EnemyBehaviour>().Target = playerTransform;
            }

            CurrentGameState.State = GameState.NewGame;
        }

        public void Pause()
        {
            switch (CurrentGameState.State)
            {
                case GameState.NewGame:
                    CurrentGameState.State = GameState.Pause;
                    pauseScreen.SetActive(true);
                    break;
                case GameState.Pause:
                    CurrentGameState.State = GameState.NewGame;
                    pauseScreen.SetActive(false);
                    break;
            }
        }

        public void Restart()
        {
            SceneManager.LoadScene("InitialScene");
        }

        private void EndGame()
        {
            CurrentGameState.State = GameState.GameOver;
            enemyManager.StopSpawning();
            cameraManager.enabled = false;
            
            foreach (var enemy in enemyManager.Enemys)
            {
                enemy.gameObject.SetActive(false);
            }
            
            gameOverScreen.SetActive(true);
        }

        public void ToMainMenu()
        {
            CurrentGameState.State = GameState.MainMenu;
            SceneManager.LoadScene("MainMenu");
        }
    }
}