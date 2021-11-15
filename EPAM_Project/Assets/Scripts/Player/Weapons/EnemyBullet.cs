using UnityEngine;

namespace Player.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyBullet : AutoBullet
    {
        protected override string DamageTag => "enemy";

        private void OnEnable()
        {
            rgbd.velocity = Stats.Speed.Value * transform.forward;
        }
    }
}