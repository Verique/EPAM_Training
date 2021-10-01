using UnityEngine;

namespace Player
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float bulletSpeed = 60f;
        private Transform bTransform;

        private void Start()
        {
            bTransform = transform;
        }

        private void FixedUpdate()
        {
            bTransform.position += bulletSpeed * Time.fixedDeltaTime * bTransform.up;
        }
    }
}