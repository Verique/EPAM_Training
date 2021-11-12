using System.Collections;
using Extensions;
using Player;
using SaveData;
using Stats;
using UnityEngine;

namespace Services
{
    public class PlayerManager : MonoBehaviour, IService
    {
        private const float DamageTakenAnimTime = 1f;
        
        private Transform player;
        private Health health;
        
        private Renderer playerRenderer;
        private PlayerExperience experience;

        public ITarget PlayerTarget { get; private set; }
        public PlayerStatLoader StatLoader { get; private set; }

        public void GetExp(int exp) => experience.GetExperience(exp);
        
        private void Awake()
        {
            player = FindObjectOfType<PlayerMovement>().transform;
            PlayerTarget = player.GetComponent<ITarget>();
            experience = player.GetComponent<PlayerExperience>();
            playerRenderer = player.GetComponentInChildren<Renderer>();
            StatLoader = player.GetComponent<PlayerStatLoader>();
            health = player.GetComponent<Health>();
            health.DamageTaken += currentHealth => StartCoroutine(nameof(PlayerDamageTakenIndication));
        }

        private void Start()
        {
            var hand = player.Find("Hand");
            var weaponManager = ServiceLocator.Instance.Get<WeaponManager>();
            weaponManager.BindToPlayerHand(hand);
            
            var stats = StatLoader.Stats;
            stats.Health.MinValueReached += () => player.gameObject.SetActive(false);
        }

        private IEnumerator PlayerDamageTakenIndication()
        {
            var startTime = Time.time;
            var currentTimeDiff = 0f;
            
            while (currentTimeDiff < DamageTakenAnimTime)
            {
                playerRenderer.material.color = Color.Lerp(Color.red, Color.white, currentTimeDiff / DamageTakenAnimTime);
                yield return new WaitForEndOfFrame();
                currentTimeDiff = Time.time - startTime;
            } 
        }

        public PlayerData GetSaveData()
        {
            return new PlayerData (player.position.ToSerializable(), player.rotation.eulerAngles.ToSerializable(), StatLoader.Stats);
        }

        public void LoadData(PlayerData data)
        {
            player.position = data.position;
            Debug.Log($"{(Vector3)data.position} - {player.position}");
            player.rotation = Quaternion.Euler(data.rotation);
            StatLoader.LoadStats(data.stats);
        }
    }
}