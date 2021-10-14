using System;
using UnityEngine;

namespace Systems
{
    public class InputManager : MonoBehaviour, IService
    {
        private Camera mainCam;
        
        public event Action<Vector2> WasdInput;
        public event Action<Vector3?> MouseMoved;

        private void Start()
        {
            mainCam = Services.Instance.Get<CameraManager>().Cam;
        }

        private Vector3? GetPointerPositionInWorld =>
            Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out var hit)
                ? hit.point
                : (Vector3?) null;

        private static Vector2 Wasd => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        private void Update()
        {
            MouseMoved?.Invoke(GetPointerPositionInWorld);
            WasdInput?.Invoke(Wasd);
        }
        
        public void Register() 
        {
            Services.Instance.Add(this);
        }
    }
}
