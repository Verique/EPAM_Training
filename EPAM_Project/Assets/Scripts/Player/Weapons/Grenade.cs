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
        private SoundManager soundManager;
        private const int BufferSize = 100;

        private Vector3 landingPoint;
        private bool landingPointCalculated;

        public override string SoungEffectShotTag => "grenadeSfx";

        protected override void Awake()
        {
            base.Awake();
            rgbd = GetComponent<Rigidbody>();
            soundManager = ServiceLocator.Instance.Get<SoundManager>();
            pool = ServiceLocator.Instance.Get<ObjectPool>();
        }

        protected void OnEnable()
        {
            CalculateLandingPoint(Destination);
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
            var position = t.position;
            
            var pSystem = pool.Spawn<ParticleSystem>("explosionFx", position, t.rotation);
            var sh = pSystem.shape;
            sh.radius = Stats.ShotRadius.Value;
            pSystem.Play();

            soundManager.PlayAt("explosionSfx", position);
            
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