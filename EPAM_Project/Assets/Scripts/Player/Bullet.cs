using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float bulletSpeed = 60f;
        private Rigidbody rgbd;
        private Transform bTransform;

        private void Start()
        {
            rgbd = GetComponent<Rigidbody>();
            bTransform = GetComponent<Transform>();
        }

        private void FixedUpdate()
        {
            rgbd.velocity = bulletSpeed * bTransform.up;
        }
    }
}