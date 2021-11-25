using UnityEngine;

namespace Enemy
{
    public class DashingEnemy : MeleeEnemy
    {
        private const int UsualAgentPriority = 50;
        private const int DashAgentPriority = 49;
        
        public override string PoolTag => "dashEnemy";

        [SerializeField] private float dashSpeed = 100f;

        protected override void StartSkill()
        {
            base.StartSkill();
            SetAgent(DashAgentPriority, dashSpeed, Player.Position);
        }

        protected override void Skill()
        {
            if (!CheckDistance(VectorTo(Agent.destination), 10)) return;
            
            StartMove();
        }

        protected override void StartMove()
        {
            base.StartMove();
            SetAgent(UsualAgentPriority, Stats.Speed.Value, Player.Position);
        }

        private void SetAgent(int prio, float speed, Vector3 dest)
        {
            Agent.avoidancePriority = prio;
            Agent.speed = speed;
            Agent.destination = dest;
        }
    }
}