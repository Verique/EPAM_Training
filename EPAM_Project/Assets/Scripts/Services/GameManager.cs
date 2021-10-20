using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Services
{
    public class GameManager : MonoBehaviour, IService
    {
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private PauseScreen pauseScreen;

        private EnemySpawner enemySpawner;
        private CameraManager cameraManager;
        private PlayerManager playerManager;
        private Transform playerTransform;
        
        public bool GamePaused { get; private set; }

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
            
            enemySpawner = ServiceLocator.Instance.Get<EnemySpawner>();
            enemySpawner.StartSpawning();
            
            foreach (var enemy in enemySpawner.Enemys)
            {
                enemy.GetComponent<Enemy.Enemy>().Target = playerTransform;
            }

            GamePaused = false;
        }

        public void Pause()
        {
            GamePaused = !GamePaused;
            pauseScreen.SetActive(GamePaused);
        }

        public void Restart()
        {
            SceneManager.LoadScene("InitialScene");
        }

        private void EndGame()
        {
            enemySpawner.StopSpawning();
            cameraManager.enabled = false;
            
            foreach (var enemy in enemySpawner.Enemys)
            {
                enemy.SetActive(false);
            }
            
            gameOverScreen.SetActive(true);
        }

        public void ToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}