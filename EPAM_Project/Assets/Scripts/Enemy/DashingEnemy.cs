using UnityEngine;

namespace Enemy
{
    public class DashingEnemy : MeleeEnemy
    {
        [SerializeField] private float dashSpeed = 100f;

        protected override void StartSkill()
        {
            base.StartSkill();
            Agent.speed = dashSpeed;
            Agent.destination = Player.Position;
            Agent.avoidancePriority = 49;
        }

        protected override void Skill()
        {
            if (!CheckDistance(VectorTo(Agent.destination), 10)) return;
            
            StartMove();
        }

        protected override void StartMove()
        {
            base.StartMove();
            Agent.avoidancePriority = 50;
            Agent.speed = Stats.Speed.Value;
            Agent.destination = Player.Position;
        }
    }
}