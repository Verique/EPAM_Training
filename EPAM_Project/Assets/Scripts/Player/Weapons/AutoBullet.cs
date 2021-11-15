using System;
using UnityEngine;

namespace Player.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class AutoBullet : BaseShot
    {
        protected Rigidbody rgbd;
        protected virtual string DamageTag => "player";

        protected override void Awake()
        {
            base.Awake();
            rgbd = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            rgbd.velocity = Stats.Speed.Value * STransform.up;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Terrain")) gameObject.SetActive(false);
            if (!other.TryGetComponent(out Health health)) return;
            var hit = health.TakeDamage(Stats.Damage.Value, DamageTag);
            gameObject.SetActive(!hit);
        }
    }
}