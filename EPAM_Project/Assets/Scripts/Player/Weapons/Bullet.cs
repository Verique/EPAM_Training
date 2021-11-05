using UnityEngine;

namespace Player.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : BaseShot
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
            if (Stats == null) return;
            
            rgbd.velocity = Stats.Speed.Value * STransform.up;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.TryGetComponent(out Health health)) return;
            health.TakeDamage(Stats.Damage.Value, gameObject);
            gameObject.SetActive(false);
        }
    }
}