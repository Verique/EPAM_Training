using System;
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
        
        private EnemyState state;
        
        private float squaredAttackDistance;
        private float squaredSkillDistance;
        private float lastAttackTime;
        private float lastSkillTime;

        public event Action<int> EnemyKilledExp;

        public ITarget Player { get; set; }
        public abstract string PoolTag { get; }

        protected virtual void Init()
        {
            Stats = GetComponent<EnemyStatLoader>().Stats;
            Agent = GetComponent<NavMeshAgent>();
            var healthComp = GetComponent<Health>();
            
            var primDistance = Stats.AttackDistance.Value;
            var scndDistance = Stats.SkillDistance.Value;
            squaredAttackDistance = primDistance * primDistance;
            squaredSkillDistance = scndDistance * scndDistance;
            Agent.speed = Stats.Speed.Value;
            
            Stats.Speed.ValueChanged += newSpeed => Agent.speed = newSpeed;
            healthComp.KilledBy += OnKill;
        }

        protected virtual void OnKill(string dmgTag)
        {
            if (dmgTag != "player" && dmgTag != "explosion") return;
            
            EnemyKilledExp?.Invoke(Stats.Experience.Value);
            gameObject.SetActive(false);
        }
        
        protected virtual void Move()
        { }

        protected virtual void Attack()
        { }

        protected virtual void Skill()
        { }

        protected virtual void StartSkill()
        {
            state = EnemyState.UsingSkill;
            lastSkillTime = Time.time;
        }

        protected virtual void StartAttack()
        {
            state = EnemyState.Attacking;
            lastAttackTime = Time.time;
        }

        protected virtual void StartMove() => state = EnemyState.Moving;

        protected Vector2 VectorTo(Vector3 position) =>
            (position - transform.position).ToVector2();

        protected static bool CheckDistance(Vector2 current, float maxDistance) =>
            current.sqrMagnitude <= maxDistance;

        private static bool CheckTime(ref float lastTime, float coolDownTime)
        {
            if (lastTime == 0) return true;
            var timePossible = Time.time - lastTime >= coolDownTime;
            if (timePossible) lastTime = Time.time;
            return timePossible;
        }

        private bool CheckAttack =>
            CheckTime(ref lastAttackTime, Stats.AttackTime.Value) &&
            AttackIsPossible;

        private bool CheckSkill =>
            CheckTime(ref lastSkillTime, Stats.SkillCooldown.Value) &&
            SkillIsPossible;
        
        private bool AttackIsPossible =>
            !Agent.Raycast(Player.Position, out _) &&
            CheckDistance(VectorTo(Player.Position), squaredAttackDistance);
        
        private bool SkillIsPossible => 
            !Agent.Raycast(Player.Position, out _) &&
            CheckDistance(VectorTo(Player.Position), squaredSkillDistance);

        private void Start()
        {
            Init();
        }

        private void Update()
        {
            switch (state)
            {
                case EnemyState.Moving:
                    Move();
                    if (CheckSkill) StartSkill();
                    else if (CheckAttack) StartAttack();
                    break;
                case EnemyState.Attacking:
                    Attack();
                    break;
                case EnemyState.UsingSkill:
                    Skill();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
