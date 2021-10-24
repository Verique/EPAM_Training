using System;
using Player;
using Services;
using Stats;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody), typeof(Health), typeof(StatLoader))]
    public class EnemyBehaviour : MonoBehaviour
    {
        private const float Height = -5f;

        private Rigidbody rgbd;
        private Transform eTransform;
        
        public Transform Target { get; set; }

        private float speed;

        private void Awake()
        {
            speed = GetComponent<StatLoader>().GetFloat("speed");
            GetComponent<Health>().IsDead += () => gameObject.SetActive(false);
            GetComponent<Health>().IsDead += () => ServiceLocator.Instance.Get<PlayerManager>().Experience.GetExperience(1);
            rgbd = GetComponent<Rigidbody>();
            eTransform = transform;
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
            rgbd.velocity = eTransform.forward * speed;
        }

    }
}