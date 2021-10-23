using SaveData;
using Services;
using Stats;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(StatLoader))]
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody rgbd;
        private InputManager inputManager;
        private StatLoader statLoader;

        private Vector3 mousePos;
        private Vector2 input;
        private float speed;

        public float Speed => speed;    

        private void Start()
        {
            statLoader = GetComponent<StatLoader>();
            speed = statLoader.GetFloat("speed");
            
            rgbd = GetComponent<Rigidbody>();
            statLoader = GetComponent<StatLoader>();
            
            inputManager = ServiceLocator.Instance.Get<InputManager>();
            inputManager.MouseMoved += ChangeMousePos;
            inputManager.WasdInput += ChangePlayerPos;
        }

        private void ChangeMousePos(Vector3 mousePos)
        {
            this.mousePos = mousePos;
        }
        private void ChangePlayerPos(Vector2 input)
        {
            this.input = input;
        }
        
        private void FixedUpdate()
        {
            Move();
            Rotate();
        }

        private void Move()
        {
            var dir = input.normalized;
            var newPos = rgbd.position + speed * Time.fixedDeltaTime * (Vector3) dir;
            rgbd.MovePosition(newPos);
        }

        private void Rotate()
        {
            var dirToMouse = mousePos - rgbd.position;
            var angle = Mathf.Atan2(dirToMouse.y, dirToMouse.x) * Mathf.Rad2Deg - 90f;
            rgbd.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}