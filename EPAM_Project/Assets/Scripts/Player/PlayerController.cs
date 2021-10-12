using Services;
using UnityEngine;

namespace Player
{
    public class PlayerController : IService
    {
        private PlayerModel model;
        private UserInputController uinput;
        
        public void Init(PlayerModel player)
        {
            model = player;
            uinput = ServiceLocator.Instance.Get<UserInputController>();
            uinput.WasdInput += ProcessWasdInput;
            uinput.MouseMoved += ProcessMouseNewPos;
        }

        private void ProcessWasdInput(Vector2 input)
        {
            var dir = input.normalized;
            model.MovePos = model.Rgbd.position + model.Speed * Time.fixedDeltaTime * (Vector3) dir;
        }

        private void ProcessMouseNewPos(Vector3? obj)
        {
            if (UserInputController.GetPointerPositionInWorld == null)
                return;

            var mousePos = UserInputController.GetPointerPositionInWorld.GetValueOrDefault();
            var dirToMouse = mousePos - model.Rgbd.position;
            var angle = Mathf.Atan2(dirToMouse.y, dirToMouse.x) * Mathf.Rad2Deg - 90f;
            model.NewRotation = Quaternion.Euler(0, 0, angle);
        }
    }
}