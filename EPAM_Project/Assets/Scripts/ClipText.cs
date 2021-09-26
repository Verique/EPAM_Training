using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClipText : MonoBehaviour
{
    private Text text;

    private void Start()
    {
        Weapon.RELOADING += Reloading;
        Weapon.BULLET_COUNT_CHANGED += DrawBulletCount;
        text = GetComponent<Text>();
    }

    private void Reloading(bool reloading)
    {
        if (reloading)
            text.color = Color.red;
        else
            text.color = Color.white;
    }

    private void DrawBulletCount(int bullets)
    {
        text.text = bullets.ToString();
    }
}
