using System;
using Enemy;
using SaveData;
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
        private PlayerStats stats;

        private void Awake()
        {
            rgbd = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            stats = GetComponent<PlayerStatLoader>().Stats;
            
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
            var newPos = rgbd.position + stats.Speed.Value * Time.fixedDeltaTime * (Vector3) dir;
            rgbd.MovePosition(newPos);
        }

        private void Rotate()
        {
            var dirToMouse = mousePos - rgbd.position;
            var angle = Mathf.Atan2(dirToMouse.y, dirToMouse.x) * Mathf.Rad2Deg - 90f;
            rgbd.rotation = Quaternion.Euler(0, 0, angle);
        }

        public Vector3 Position => rgbd.position;
    }
}