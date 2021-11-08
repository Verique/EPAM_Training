using Extensions;
using Services;
using Stats;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(PlayerStatLoader))]
    public class PlayerMovement : MonoBehaviour, ITarget
    {
        private Rigidbody rgbd;
        private InputManager inputManager;

        private Vector3 mousePos;
        private Vector2 input;
        private Stat<float> speedStat;

        private void Awake()
        {
            rgbd = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            speedStat = GetComponent<PlayerStatLoader>().Stats.Speed;
            
            inputManager = ServiceLocator.Instance.Get<InputManager>();
            inputManager.MouseMoved += ChangeMousePos;
            inputManager.WasdInput += ChangePlayerPos;
        }

        private void ChangeMousePos(Vector3 newMousePos)
        {
            mousePos = newMousePos;
        }
        private void ChangePlayerPos(Vector2 newInput)
        {
            input = newInput;
        }
        
        private void FixedUpdate()
        {
            Move();
            Rotate();
        }

        private void Move()
        {
            var dir = input.normalized;
            var newPos = rgbd.position + speedStat.Value * Time.fixedDeltaTime * dir.ToVector3();
            rgbd.MovePosition(newPos);
        }

        private void Rotate()
        {
            var dirToMouse = mousePos - rgbd.position;
            var angle = Mathf.Atan2(dirToMouse.x, dirToMouse.z) * Mathf.Rad2Deg;
            rgbd.rotation = Quaternion.Euler(0, angle, 0);
        }

        public Vector3 Position => transform.position;
    }
}