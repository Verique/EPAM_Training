using Services;

namespace Player
{
    public class AutoRifle : BaseWeapon
    {
        private ObjectPool pool;

        protected override void Awake()
        {
            base.Awake();
            pool = ServiceLocator.Instance.Get<ObjectPool>();
        }

        protected override void FireShot()
        {
            base.FireShot();
            var wTransform = transform;
            pool.Spawn("bullet", wTransform.position, wTransform.rotation);
        }
    }
}