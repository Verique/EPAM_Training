using System.Collections;
using Services;
using UnityEngine;

namespace Player.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class Grenade : BaseShot
    {
        private const int SquaredDistanceToExplode = 10;
        [SerializeField] private float explosionDelay;
        
        private Rigidbody rgbd;
        private ObjectPool pool;
        private const int BufferSize = 100;

        private Vector3 landingPoint;
        private bool landingPointCalculated;

        protected override void Awake()
        {
            base.Awake();
            rgbd = GetComponent<Rigidbody>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            CalculateLandingPoint(Destination);
            pool = ServiceLocator.Instance.Get<ObjectPool>();
        }

        private void CalculateLandingPoint(Vector3 desiredPoint)
        {
            var position = transform.position;
            desiredPoint.y = position.y;
            var desiredDistance = desiredPoint - position;
            var dir = desiredDistance.normalized;
            
            var maxLandingDistance = desiredDistance.normalized * Stats.ShotLength.Value;
            var maxLandingPoint = position + maxLandingDistance;

            if (Physics.Raycast(position, dir, out var hit, Stats.ShotLength.Value, ~LayerMask.NameToLayer("Terrain")))
            {
                maxLandingPoint = hit.point;
                maxLandingDistance = maxLandingPoint - position;
            }
            
            landingPoint = desiredDistance.sqrMagnitude < maxLandingDistance.sqrMagnitude ? desiredPoint : maxLandingPoint;
            landingPoint.y = position.y;
            
            landingPointCalculated = true;
        }

        private void FixedUpdate()
        {
            if (!landingPointCalculated) return;
            
            if ((rgbd.position - landingPoint).sqrMagnitude < SquaredDistanceToExplode)
            {
                rgbd.velocity = Vector3.zero;
                StartCoroutine(Explode());
            }
            else
            {
                rgbd.velocity = Stats.Speed.Value * transform.up;
            }
        }

        private IEnumerator Explode()
        {
            yield return new WaitForSeconds(explosionDelay);

            var t = transform;
            
            var pSystem = pool.Spawn<ParticleSystem>("effect", t.position, t.rotation);
            var sh = pSystem.shape;
            sh.radius = Stats.ShotRadius.Value;
            pSystem.Play();
            
            var results = new Collider[BufferSize];
            var hitCount = Physics.OverlapSphereNonAlloc(STransform.position, Stats.ShotRadius.Value, results);
            
            for (var i = 0; i < hitCount; i++)
            {
                var hit = results[i];
                if (!hit.transform.TryGetComponent(out Health enemyHealth)) continue;

                enemyHealth.TakeDamage(Stats.Damage.Value, "explosion");
            }
            
            gameObject.SetActive(false);
        }
    }
}