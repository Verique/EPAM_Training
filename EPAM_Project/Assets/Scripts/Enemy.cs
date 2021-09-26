using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const float height = -5f;
    private Transform target;
    private Transform eTransform;
    [SerializeField]
    private float enemySpeed = 100f;

    private void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
        eTransform = transform;
    }

    private void Update()
    {
        Vector3 lookPos = target.position;
        lookPos.z = height;
        eTransform.LookAt(lookPos);
        eTransform.position += eTransform.forward * enemySpeed * Time.deltaTime;
    }
}
