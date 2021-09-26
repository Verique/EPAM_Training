using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField]
    private int damage;
    public int Damage { get { return damage; } }
}
