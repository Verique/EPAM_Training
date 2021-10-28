using System;
using Stats;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(BulletStatLoader))]
    public class Bullet : MonoBehaviour
    {
        private Rigidbody rgbd;
        private Transform bTransform;
        private Stat<float> speedStat;

        private void Awake()
        {
            speedStat = GetComponent<BulletStatLoader>().Stats.Speed;
            rgbd = GetComponent<Rigidbody>();
            bTransform = GetComponent<Transform>();
        }

        private void Start()
        {
            GetComponent<BulletStatLoader>().Stats.Health.MinValueReached += () => gameObject.SetActive(false);
        }

        private void OnEnable()
        {           
            rgbd.velocity = speedStat.Value * bTransform.up;
        }
    }
}