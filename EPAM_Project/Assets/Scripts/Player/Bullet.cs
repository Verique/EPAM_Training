using System;
using Stats;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(Health), typeof(StatLoader))]
    public class Bullet : MonoBehaviour
    {
        private float bulletSpeed;
        private Rigidbody rgbd;
        private Transform bTransform;
    
        private void Start()
        {
            GetComponent<Health>().IsDead += () => gameObject.SetActive(false);
        }

        private void OnEnable()
        {           
            bulletSpeed = GetComponent<StatLoader>().GetFloat("speed");
            rgbd = GetComponent<Rigidbody>();
            bTransform = GetComponent<Transform>();
            rgbd.velocity = bulletSpeed * bTransform.up;
        }
    }
}