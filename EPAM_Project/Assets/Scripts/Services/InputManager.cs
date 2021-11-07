using System;
using UnityEngine;

namespace Services
{
    public class InputManager : MonoBehaviour, IService
    {
        private CameraManager cameraManager;
        
        public event Action<Vector2> WasdInput;
        public event Action<Vector3> MouseMoved;
        public event Action<float> MouseScrolled; 
        public event Action<Vector3> PrimaryFire; 
        public event Action ReloadKeyUp;
        public event Action PauseKeyUp;
        public event Action WeaponOne;
        public event Action WeaponTwo;
        public event Action WeaponThree;

        private void Awake()
        {
            cameraManager = ServiceLocator.Instance.Get<CameraManager>();
        }

        private static Vector2 Wasd => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        private void Update()
        {
            WasdInput?.Invoke(Wasd);
            
            if (Input.mouseScrollDelta != Vector2.zero) MouseScrolled?.Invoke(Input.mouseScrollDelta.y);
            
            if (cameraManager.TryGetPointerPosInWorld(Input.mousePosition, out var worldMousePos))
            {
                MouseMoved?.Invoke(worldMousePos);
            }
           
            if (Input.GetKeyUp(KeyCode.Alpha1)) WeaponOne?.Invoke();
            if (Input.GetKeyUp(KeyCode.Alpha2)) WeaponTwo?.Invoke();
            if (Input.GetKeyUp(KeyCode.Alpha3)) WeaponThree?.Invoke();
            
            if (Input.GetKeyUp(KeyCode.R)) ReloadKeyUp?.Invoke();
            if (Input.GetMouseButton(0)) PrimaryFire?.Invoke(worldMousePos);
            if (Input.GetKeyUp(KeyCode.Escape)) PauseKeyUp?.Invoke();
        }
    }
}
