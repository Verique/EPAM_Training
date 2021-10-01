using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    [SerializeField]
    private float invTime;

    private const int framesToLerp = 80;
    private const float colorLerpSpeed = 1 / (float)framesToLerp;

    [SerializeField]
    private Renderer playerRenderer;

    [SerializeField]
    private GameObject gameOverText;

    bool isDamageable = true;

    protected override void TakeDamage(int damage)
    {
        if (isDamageable)
        {
            StartCoroutine("PlayerDamageTakenCooldown");
            StartCoroutine("PlayerDamageTakenIndication");
            base.TakeDamage(damage);
        }
    }

    private IEnumerator PlayerDamageTakenCooldown()
    {
        isDamageable = false;
        yield return new WaitForSecondsRealtime(invTime);
        isDamageable = true;
    }

    private IEnumerator PlayerDamageTakenIndication()
    {
        int frameCount = 0;

        do
        {
            playerRenderer.material.color = Color.Lerp(Color.red, Color.white, frameCount * colorLerpSpeed);
            frameCount++;
            yield return new WaitForEndOfFrame();
        } while (frameCount < framesToLerp);
    }

    protected override void Kill()
    {
        base.Kill();
        gameOverText.SetActive(true);
    }
}

