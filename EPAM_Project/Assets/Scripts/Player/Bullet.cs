using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float bulletSpeed = 60f;
        private Rigidbody rgbd;
        private Transform bTransform;

        private void OnEnable()
        {           
            rgbd = GetComponent<Rigidbody>();
            bTransform = GetComponent<Transform>();
            rgbd.velocity = bulletSpeed * bTransform.up;
        }
    }
}