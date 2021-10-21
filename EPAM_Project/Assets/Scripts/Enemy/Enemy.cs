using System;
using Player;
using Stats;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody), typeof(Health), typeof(StatLoader))]
    public class Enemy : MonoBehaviour
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