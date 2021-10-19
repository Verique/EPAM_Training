using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Services
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject pauseScreen;

        private bool isPaused;
        private EnemySpawner enemySpawner;
        private CameraManager cameraManager;
        private Transform playerTransform;
        private void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            var player = ServiceLocator.Instance.Get<PlayerManager>();
            playerTransform = player.Transform;
            playerTransform.GetComponent<Health>().IsDead += EndGame;

            ServiceLocator.Instance.Get<InputManager>().PauseKeyUp += Pause;
            
            cameraManager = ServiceLocator.Instance.Get<CameraManager>();
            cameraManager.Target = playerTransform;
            
            enemySpawner = ServiceLocator.Instance.Get<EnemySpawner>();
            enemySpawner.StartSpawning();

            gameOverScreen.GetComponentInChildren<Button>().onClick.AddListener(Restart);
            pauseScreen.GetComponentInChildren<Button>().onClick.AddListener(Pause);
        }

        private void Pause()
        {
            isPaused = !isPaused;
            pauseScreen.SetActive(isPaused);
            Time.timeScale = isPaused ? 0 : 1; 
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