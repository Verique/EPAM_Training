namespace Enemy
{

    public class MeleeEnemy : BaseEnemy
    {
        protected override void Move()
        {
            if (Player is null) return;

            Agent.destination = Player.Position;
        }

        protected override void StartAttack()
        {
            base.StartAttack();
            if (!Player.GameObject.TryGetComponent(out Health health)) return;
            
            health.TakeDamage(Stats.Damage.Value, gameObject);
            
            StartMove();
        }
    }
}