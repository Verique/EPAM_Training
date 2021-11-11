using UnityEngine;

namespace Player.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class AutoBullet : BaseShot
    {
        private Rigidbody rgbd;

        protected override void Awake()
        {
            base.Awake();
            rgbd = GetComponent<Rigidbody>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            rgbd.velocity = Stats.Speed.Value * STransform.up;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Terrain")) gameObject.SetActive(false);
            if (!other.gameObject.TryGetComponent(out Health health)) return;
            var hit = health.TakeDamage(Stats.Damage.Value, "player");
            gameObject.SetActive(!hit);
        }
    }
}