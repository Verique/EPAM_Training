using System;
using System.Collections;
using UnityEngine;

namespace Services
{
    public class PlayerManager : MonoBehaviour, IService
    {
        [SerializeField] private Transform player;
        
        private Renderer playerRenderer;
        public Health Health { get; private set; }
        public Transform Transform => player;
        private const int FramesToLerp = 80;
        private const float ColorLerpSpeed = 1 / (float) FramesToLerp;

        private void Start()
        {
            Health = player.GetComponent<Health>();
            Health.DamageTaken += currentHealth => StartCoroutine(nameof(PlayerDamageTakenIndication));
            Health.IsDead += () => player.gameObject.SetActive(false);
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
    }
}