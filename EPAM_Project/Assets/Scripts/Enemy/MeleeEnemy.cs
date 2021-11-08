using Services;
using Stats;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(Health), typeof(EnemyStatLoader))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    public class MeleeEnemy : MonoBehaviour
    {
        private NavMeshAgent agent;
        private Rigidbody rgbd;
        private EnemyStats stats;
        private float lastAttackTime;
        
        public ITarget Target { get; set; }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            rgbd = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            stats = GetComponent<EnemyStatLoader>().Stats;
            agent.speed = stats.Speed.Value;
            stats.Speed.ValueChanged += value => agent.speed = stats.Speed.Value; 
            stats.Health.MinValueReached += () => gameObject.SetActive(false);
            stats.Health.MinValueReached += () => ServiceLocator.Instance.Get<PlayerManager>().Experience.GetExperience(1);
        }

        private void FixedUpdate()
        {
            if (Target is null) return;
            
            agent.destination = Target.Position;
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