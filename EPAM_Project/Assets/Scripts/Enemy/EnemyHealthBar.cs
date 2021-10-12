using UI;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealthBar : MonoBehaviour
    {
        private readonly Vector3 offset = new Vector3(-30, 20, 0);
        private UIBar bar;
        private Camera cam;
        private Transform eTransform;
        private Transform bTransform;
        private Health health;
    
        private void Start()
        {
            health = GetComponent<Health>();
            bar = ObjectPooler.Instance.Spawn("hbar", Vector3.zero, Quaternion.identity).GetComponent<UIBar>();
            eTransform = transform;
            bTransform = bar.transform;
            bar.MaxValue = health.MaxHealth;
            health.HealthChanged += bar.UpdateBarWidth;
            cam = Camera.main;
        }

        private void OnEnable()
        {
            if (bar != null)
                bar.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            if (bar != null)
                bar.gameObject.SetActive(false);
        }

        private void LateUpdate()
        {
            bTransform.position = cam.WorldToScreenPoint(eTransform.position) + offset;
        }

    }
}
