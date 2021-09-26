using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    private const float height = -5f;

    private Transform target;

    private Rigidbody rgbd;
    private Transform eTransform;

    [SerializeField]
    private float enemySpeed = 100f;

    private void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
        rgbd = GetComponent<Rigidbody>();
        eTransform = transform;
    }

    private void FixedUpdate()
    {
        Vector3 lookPos = target.position;
        lookPos.z = height;
        eTransform.LookAt(lookPos);
        rgbd.velocity = eTransform.forward * enemySpeed;
    }

}
