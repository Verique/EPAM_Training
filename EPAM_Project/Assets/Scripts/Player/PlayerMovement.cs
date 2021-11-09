using Extensions;
using Services;
using Stats;
using UnityEngine;
using UnityEngine.AI;

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
            rgbd.velocity = speedStat.Value * dir.ToVector3();
        }

        private void Rotate()
        {
            var dirToMouse = mousePos - rgbd.position;
            rgbd.rotation = dirToMouse.ToRotation();
        }

        public Vector3 Position => transform.position;
    }
}