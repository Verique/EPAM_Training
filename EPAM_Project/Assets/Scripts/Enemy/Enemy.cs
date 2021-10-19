using System;
using Player;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody), typeof(Health))]
    public class Enemy : MonoBehaviour
    {
        private const float Height = -5f;

        private Rigidbody rgbd;
        private Transform eTransform;
        public Transform Target { get; set; }
        public Vector3 TargetPos => Target.position;

        [SerializeField] private float enemySpeed = 100f;

        private void Start()
        {
            GetComponent<Health>().IsDead += () => gameObject.SetActive(false);
            Target = FindObjectOfType<PlayerMovement>().transform;
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
            rgbd.velocity = eTransform.forward * enemySpeed;
        }

    }
}