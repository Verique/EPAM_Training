using System;
using SaveData;
using Services;
using Stats;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody), typeof(Health), typeof(EnemyDataLoader))]
    public class Enemy : MonoBehaviour
    {
        private const float Height = -5f;

        private Rigidbody rgbd;
        private Transform eTransform;
        private EnemyData enemyData;
        
        public Transform Target { get; set; }

        private void Awake()
        {
            rgbd = GetComponent<Rigidbody>();
            eTransform = transform;
        }

        private void Start()
        {
            enemyData = GetComponent<EnemyDataLoader>().Data;
            enemyData.HealthData.IsDead += () => gameObject.SetActive(false);
            enemyData.HealthData.IsDead += () => ServiceLocator.Instance.Get<PlayerManager>().Experience.GetExperience(1);
        }

        private void FixedUpdate()
        {
            if (Target is null)
            {
                return;
            }
        
            var lookPos = Target.position;
            lookPos.z = Height;
            eTransform.LookAt(lookPos);
            rgbd.velocity = eTransform.forward * enemyData.Speed;
        }

    }
}