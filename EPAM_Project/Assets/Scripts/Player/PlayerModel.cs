using Services;
using UnityEngine;

namespace Player
{
    public class PlayerModel : MonoBehaviour
    {
        public Rigidbody Rgbd { get; private set; }
        public float Speed => speed;

        public Vector3 MovePos { get; set; }
        public Quaternion NewRotation { get; set; } = Quaternion.identity;

        [SerializeField] private float speed;

        private void Start()
        {
            Rgbd = GetComponent<Rigidbody>();
            ServiceLocator.Instance.Get<PlayerController>().Init(this);
        }

        private void FixedUpdate()
        {
            Rgbd.MovePosition(MovePos);
            Rgbd.rotation = NewRotation; 
        }
    }
}