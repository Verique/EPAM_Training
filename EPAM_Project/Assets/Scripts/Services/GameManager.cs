using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Services
{
    public class GameManager : MonoBehaviour, IService
    {
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject pauseScreen;

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

            gameOverScreen.GetComponentInChildren<Button>().onClick.AddListener(Restart);
            pauseScreen.GetComponentInChildren<Button>().onClick.AddListener(Pause);

            GamePaused = false;
        }

        private void Pause()
        {
            GamePaused = !GamePaused;
            pauseScreen.SetActive(GamePaused);
            Time.timeScale = GamePaused ? 0 : 1;
        }

        private void Restart()
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
    }
}