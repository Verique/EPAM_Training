using Extensions;
using Services;
using Stats;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(EnemyStatLoader))]
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class BaseEnemy : MonoBehaviour
    {
        protected NavMeshAgent Agent;
        protected EnemyStats Stats;
        private float squaredAttackDistance;
        
        public ITarget Player { get; set; }
        
        protected virtual void Init()
        {
            Stats = GetComponent<EnemyStatLoader>().Stats;
            Agent = GetComponent<NavMeshAgent>();
            Agent.speed = Stats.Speed.Value;
            var stopDistance = Stats.AttackDistance.Value;
            squaredAttackDistance = stopDistance * stopDistance;
            Stats.Speed.ValueChanged += newSpeed => Agent.speed = newSpeed;
            Stats.Health.MinValueReached += () => gameObject.SetActive(false);
            Stats.Health.MinValueReached += () => ServiceLocator.Instance.Get<PlayerManager>().Experience.GetExperience(1);
        }

        protected abstract void Move();
        protected abstract void Attack();

        protected virtual bool AttackIsPossible => 
            (Player.Position.ToVector2() - transform.position.ToVector2()).sqrMagnitude <= squaredAttackDistance;

        private void Start()
        {
            Init();
        }

        private void FixedUpdate()
        {
            Move();
            if (AttackIsPossible)
            {
                Attack();
            }

            Agent.isStopped = AttackIsPossible;
        }
    }
}
