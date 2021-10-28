using System;
using Stats;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(BulletStatLoader))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float bulletSpeed;
        private Rigidbody rgbd;
        private Transform bTransform;
    
        private void Start()
        {
            GetComponent<BulletStatLoader>().Stats.Health.MinValueReached += () => gameObject.SetActive(false);
        }

        private void OnEnable()
        {           
            rgbd = GetComponent<Rigidbody>();
            bTransform = GetComponent<Transform>();
            rgbd.velocity = bulletSpeed * bTransform.up;
        }
    }
}