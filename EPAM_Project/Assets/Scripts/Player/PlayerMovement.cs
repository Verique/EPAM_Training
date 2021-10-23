using System;
using SaveData;
using Services;
using Stats;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(PlayerDataLoader))]
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody rgbd;
        private PlayerData playerData;
        private InputManager inputManager;

        private Vector3 mousePos;
        private Vector2 input;

        private void Awake()
        {
            rgbd = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            playerData = GetComponent<PlayerDataLoader>().playerData;
            inputManager = ServiceLocator.Instance.Get<InputManager>();
            inputManager.MouseMoved += (newMousePos) => mousePos = newMousePos;
            inputManager.WasdInput += (newInput) => input = newInput;
        }

        private void FixedUpdate()
        {
            Move();
            Rotate();
        }

        private void Move()
        {
            var dir = input.normalized;
            var newPos = rgbd.position + playerData.Speed * Time.fixedDeltaTime * (Vector3) dir;
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