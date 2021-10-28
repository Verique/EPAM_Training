using System;
using Player;
using Services;
using Stats;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody), typeof(Health), typeof(EnemyStatLoader))]
    public class EnemyBehaviour : MonoBehaviour
    {
        private const float Height = -5f;

        private Rigidbody rgbd;
        private Transform eTransform;
        private EnemyStatLoader statLoader;
        
        public ITarget Target { get; set; }

        private void Awake()
        {
            rgbd = GetComponent<Rigidbody>();
            eTransform = transform;
        }

        private void Start()
        {
            statLoader = GetComponent<EnemyStatLoader>();
            statLoader.Stats.Health.MinValueReached += () => gameObject.SetActive(false);
            statLoader.Stats.Health.MinValueReached += () => ServiceLocator.Instance.Get<PlayerManager>().Experience.GetExperience(5);
        }

        private void FixedUpdate()
        {
            if (Target is null) return;
        
            var lookPos = Target.Position;
            lookPos.z = Height;
            eTransform.LookAt(lookPos);
            rgbd.velocity = eTransform.forward * statLoader.Stats.Speed.Value;
        }

    }
}