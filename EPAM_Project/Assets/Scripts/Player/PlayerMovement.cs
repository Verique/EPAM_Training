using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody rgbd;

        [SerializeField] private float speed = 5;

        public UnityAction<Vector3> PlayerMoved;
        public UnityAction<Vector3> MouseMoved;

        private void Start()
        {
            rgbd = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            UserInputMove();
            UserInputRotate();
        }

        private void UserInputMove()
        {
            var input = UserInput.WasdInput;
            var dir = input.normalized;
            var newPos = rgbd.position + speed * Time.fixedDeltaTime * (Vector3) dir;
            rgbd.MovePosition(newPos);

            PlayerMoved?.Invoke(rgbd.position);
        }

        private void UserInputRotate()
        {
            if (UserInput.GetPointerPositionInWorld == null)
                return;

            var mousePos = UserInput.GetPointerPositionInWorld.GetValueOrDefault();
            var dirToMouse = mousePos - rgbd.position;
            var angle = Mathf.Atan2(dirToMouse.y, dirToMouse.x) * Mathf.Rad2Deg - 90f;
            rgbd.rotation = Quaternion.Euler(0, 0, angle);

            MouseMoved?.Invoke(mousePos);
        }
    }
}