using UnityEngine;

namespace Enemy
{

    public class MeleeEnemy : BaseEnemy
    {
        private float lastAttackTime;

        protected override void Move()
        {
            if (Player is null) return;

            Agent.destination = Player.Position;

            if (Agent.isStopped)
            {
                Agent.transform.LookAt(Player.Position);
            }
        }

        protected override void Attack()
        {
            if (Time.time - lastAttackTime < Stats.AttackTime.Value) return;
            if (!Player.GameObject.TryGetComponent(out Health health)) return;
            
            
            health.TakeDamage(Stats.Damage.Value, gameObject);
            lastAttackTime = Time.time;
        }
    }
}