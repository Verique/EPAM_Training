using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 60f;
    private Transform bTransform;

    void Start()
    {
        bTransform = transform;
    }

    void FixedUpdate()
    {
        bTransform.position += bTransform.up * bulletSpeed * Time.fixedDeltaTime;
    }
}
