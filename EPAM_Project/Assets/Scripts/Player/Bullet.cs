using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float bulletSpeed = 60f;

        private void Start()
        {
            var rgbd = GetComponent<Rigidbody>();
            var bTransform = GetComponent<Transform>();
            rgbd.velocity = bulletSpeed * bTransform.up;
        }
    }
}