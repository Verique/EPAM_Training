using System;
using System.Collections;
using Extensions;
using Player;
using Stats;
using UnityEngine;

namespace Services
{
    public class PlayerManager : MonoBehaviour, IService
    {
        [SerializeField] private Transform player;
        public Transform Transform => player;

        public PlayerWeapon Weapon {get; private set; }
        public PlayerExperience Experience { get; private set; }
        public PlayerDataLoader Data { get; private set; }
        
        private void Awake()
        {
            Data = player.GetComponent<PlayerDataLoader>();
            Weapon = player.GetComponent<PlayerWeapon>();
            Experience = player.GetComponent<PlayerExperience>();
            InitHealth();
        }

        #region Health
        
        private Renderer playerRenderer;
        private const int FramesToLerp = 80;
        private const float ColorLerpSpeed = 1 / (float) FramesToLerp;

        private void InitHealth()
        {
            var playerHealthData = ServiceLocator.Instance.Get<PlayerManager>().Data.GetHealthData;
            playerHealthData.HealthChanged += currentHealth => StartCoroutine(nameof(PlayerDamageTakenIndication));
            playerHealthData.IsDead += () => player.gameObject.SetActive(false);
            playerRenderer = player.GetComponentInChildren<Renderer>();
        }
        
        private IEnumerator PlayerDamageTakenIndication()
        {
            var frameCount = 0;

            while (frameCount < FramesToLerp)
            {
                playerRenderer.material.color = Color.Lerp(Color.red, Color.white, frameCount * ColorLerpSpeed);
                frameCount++;
                yield return new WaitForEndOfFrame();
            } 
        }
        
        #endregion
    }
}