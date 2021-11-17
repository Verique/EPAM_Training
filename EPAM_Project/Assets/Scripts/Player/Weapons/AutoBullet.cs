using UnityEngine;

namespace Player.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class AutoBullet : BaseShot
    {
        protected Rigidbody Rgbd;
        protected virtual string DamageTag => "player";

        public override string SoungEffectShotTag => "autoBulletSfx";

        protected override void Awake()
        {
            base.Awake();
            Rgbd = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            Rgbd.velocity = Stats.Speed.Value * STransform.up;
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