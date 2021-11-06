using System;
using System.Collections;
using Services;
using UnityEngine;

namespace Player.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class Grenade : BaseShot
    {
        private const int SquaredDistanceToExplode = 50;
        [SerializeField] private float explosionDelay;
        
        private Rigidbody rgbd;
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
        }

        private void CalculateLandingPoint(Vector3 destination)
        {
            var position = transform.position;
            var direction = (destination - position);
            
            var maxLandingPoint = direction.normalized * Stats.ShotLength.Value;
            
            landingPoint = direction.sqrMagnitude < maxLandingPoint.sqrMagnitude ? destination : position + maxLandingPoint;
            landingPoint.z = -5f;

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
            
            var pSystem = ServiceLocator.Instance.Get<ObjectPool>().Spawn<ParticleSystem>("effect", t.position, t.rotation);
            var sh = pSystem.shape;
            sh.radius = Stats.ShotRadius.Value;
            pSystem.Play();
            
            var results = new Collider[BufferSize];
            var hitCount = Physics.OverlapSphereNonAlloc(STransform.position, Stats.ShotRadius.Value, results);
            
            for (var i = 0; i < hitCount; i++)
            {
                var hit = results[i];
                if (!hit.transform.TryGetComponent(out Health enemyHealth)) continue;

                enemyHealth.TakeDamage(Stats.Damage.Value, gameObject);
            }
            
            gameObject.SetActive(false);
        }
    }
}