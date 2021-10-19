using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(Health))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float bulletSpeed = 60f;
        private Rigidbody rgbd;
        private Transform bTransform;

        private void Start()
        {
            GetComponent<Health>().IsDead += () => gameObject.SetActive(false);
        }

        private void OnEnable()
        {           
            rgbd = GetComponent<Rigidbody>();
            bTransform = GetComponent<Transform>();
            rgbd.velocity = bulletSpeed * bTransform.up;
        }
    }
}