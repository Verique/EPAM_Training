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
        private EnemyStats stats;
        private float lastAttackTime;
        
        public ITarget Target { get; set; }

        private void Awake()
        {
            rgbd = GetComponent<Rigidbody>();
            eTransform = transform;
        }

        private void Start()
        {
            stats = GetComponent<EnemyStatLoader>().Stats;
            stats.Health.MinValueReached += () => gameObject.SetActive(false);
            stats.Health.MinValueReached += () => ServiceLocator.Instance.Get<PlayerManager>().Experience.GetExperience(1);
        }

        private void FixedUpdate()
        {
            if (Target is null) return;
        
            var lookPos = Target.Position;
            lookPos.z = Height;
            eTransform.LookAt(lookPos);
            rgbd.velocity = eTransform.forward * stats.Speed.Value;
        }
        
        private void OnCollisionStay(Collision other)
        {
            if (!other.gameObject.TryGetComponent(out Health health)) return;
            if (Time.time - lastAttackTime < stats.AttackTime.Value) return;
            
            health.TakeDamage(stats.Damage.Value, gameObject);
            lastAttackTime = Time.time;
        }
    }
}