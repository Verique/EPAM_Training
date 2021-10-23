using System;
using SaveData;
using Stats;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(BulletDataLoader))]
    public class Bullet : MonoBehaviour
    {
        private BulletData bData;
        private Rigidbody rgbd;
        private Transform bTransform;
        
        private void Awake()
        {
            rgbd = GetComponent<Rigidbody>();
            bTransform = GetComponent<Transform>();
            bData = GetComponent<BulletDataLoader>().Data;
            bData.HealthData.IsDead += () => gameObject.SetActive(false);
        }

        private void OnEnable()
        {          
            rgbd.velocity = bData.Speed * bTransform.up;
        }
    }
}