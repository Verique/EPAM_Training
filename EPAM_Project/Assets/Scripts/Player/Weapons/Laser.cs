using System.Collections;
using UnityEngine;

namespace Player.Weapons
{
    public class Laser : BaseShot
    {
        private const int BufferSize = 100;
        
        [SerializeField] private float laserLength = 100f; 
        [SerializeField] private float laserWidth = 5f;
        [SerializeField] private float laserFadeOutTime = .2f;

        protected override void OnEnable()
        {
            base.OnEnable();
            if (Stats == null) return;
            
            var results = new RaycastHit[BufferSize];
            var hitCount = Physics.SphereCastNonAlloc(STransform.position, laserWidth, STransform.up, results, laserLength);
            STransform.localScale = new Vector3(laserWidth, laserLength, laserWidth);
            STransform.transform.position += STransform.up * laserLength / 2;

            for (var i = 0; i < hitCount; i++)
            {
                var hit = results[i];
                if (!hit.transform.TryGetComponent(out Health enemyHealth)) continue;

                enemyHealth.TakeDamage(Stats.Damage.Value, gameObject);
            }

            StartCoroutine(FadeOut());
        }

        private IEnumerator FadeOut()
        {
            var startTime = Time.time;
            var diffTime = 0f;
            var scale = STransform.localScale;
            var startWidth = scale.x;

            while (diffTime < laserFadeOutTime)
            {
                scale.x = Mathf.Lerp(startWidth, 0f, diffTime / laserFadeOutTime);
                STransform.localScale = scale;

                yield return new WaitForEndOfFrame();
                diffTime = Time.time - startTime;
            }
            
            gameObject.SetActive(false);
        }
    }
}