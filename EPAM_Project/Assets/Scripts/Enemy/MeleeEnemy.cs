namespace Enemy
{

    public class MeleeEnemy : BaseEnemy
    {
        public override string PoolTag => "meleeEnemy";

        protected override void Move()
        {
            if (Player is null) return;

            Agent.destination = Player.Position;
        }

        protected override void StartAttack()
        {
            base.StartAttack();
            if (!Player.GameObject.TryGetComponent(out Health health)) return;
            
            health.TakeDamage(Stats.Damage.Value, "enemy");
            
            StartMove();
        }
    }
}