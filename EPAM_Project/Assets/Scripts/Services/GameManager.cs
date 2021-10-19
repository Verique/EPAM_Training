using Enemy;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Services
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverScreen;
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
            
            ServiceLocator.Instance.Get<CameraManager>().Target = playerTransform;
            ServiceLocator.Instance.Get<EnemySpawner>().StartSpawning();

            gameOverScreen.GetComponentInChildren<Button>().onClick.AddListener(Restart);
        }

        private void Restart()
        {
            Debug.Log("Restart");
        }

        private void EndGame()
        {
            gameOverScreen.SetActive(true);
        }
    }
}