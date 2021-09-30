using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    [SerializeField]
    private float invTime;

    [SerializeField]
    private GameObject gameOverText;

    bool isDamageable = true;

    protected override void TakeDamage(int damage)
    {
        if (isDamageable)
        {
            StartCoroutine("PlayerDamageTaken");
            base.TakeDamage(damage);
        }
    }

    private IEnumerator PlayerDamageTaken()
    {
        isDamageable = false;
        yield return new WaitForSecondsRealtime(invTime);
        isDamageable = true;
    }

    protected override void Kill()
    {
        base.Kill();
        gameOverText.SetActive(true);
    }
}

