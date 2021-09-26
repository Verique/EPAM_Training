using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private const int LMB_CODE = 0;
    private Transform pTransform;
    [SerializeField]
    private Weapon weapon;

    private void Start()
    {
        pTransform = transform;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            weapon.Fire();
    }
}
