using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : Health
    {
        [SerializeField] private float invTime;

        private const int FramesToLerp = 80;
        private const float ColorLerpSpeed = 1 / (float) FramesToLerp;

        [SerializeField] private Renderer playerRenderer;

        [SerializeField] private GameObject gameOverText;

        private bool isDamageable = true;

        protected override void TakeDamage(int damage)
        {
            if (!isDamageable) return;
            StartCoroutine(nameof(PlayerDamageTakenCooldown));
            StartCoroutine(nameof(PlayerDamageTakenIndication));
            base.TakeDamage(damage);
        }

        private IEnumerator PlayerDamageTakenCooldown()
        {
            isDamageable = false;
            yield return new WaitForSecondsRealtime(invTime);
            isDamageable = true;
        }

        private IEnumerator PlayerDamageTakenIndication()
        {
            var frameCount = 0;

            do
            {
                playerRenderer.material.color = Color.Lerp(Color.red, Color.white, frameCount * ColorLerpSpeed);
                frameCount++;
                yield return new WaitForEndOfFrame();
            } while (frameCount < FramesToLerp);
        }

        protected override void Kill()
        {
            base.Kill();
            gameOverText.SetActive(true);
        }
    }
}