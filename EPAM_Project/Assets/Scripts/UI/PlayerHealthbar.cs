using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthBar : UIBar
{
    [SerializeField]
    private Health health;

    protected override void SetupBar()
    {
        base.SetupBar();
        maxValue = health.MaxHealth;
        health.HEALTH_CHANGED += UpdateBar;
    }
}
