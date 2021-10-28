using Enemy;
using UnityEngine;

namespace Services
{
    public class CameraManager : MonoBehaviour, IService
    {
        [SerializeField] private float smoothTime = 0.2f;
        [SerializeField] private Vector3 offset;
        [SerializeField] private Camera cam;
        

        private Transform cameraTransform;
        public ITarget Target { get; set; }
        
        private Vector3 camPos;
        private Vector3 mousePos;
        private Vector3 sDampVelocity = Vector3.zero;

        public bool TryGetPointerPosInWorld(Vector2 pointerPos, out Vector3 worldPos)
        {
            var isPointerInWorld = Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out var hit);

            worldPos = isPointerInWorld ? hit.point : Vector3.zero;

            return isPointerInWorld;
        }

        private void Start()
        {
            cameraTransform = cam.transform;
            ServiceLocator.Instance.Get<InputManager>().MouseMoved += ChangeMousePos;
        }

        private void ChangeMousePos(Vector3 mousePos)
        {
            this.mousePos = mousePos;
        }

        private void FixedUpdate()
        {
            camPos = Vector3.Lerp(Target.Position, mousePos, 0.2f) - offset;
            camPos = Vector3.SmoothDamp(cameraTransform.position, camPos, ref sDampVelocity, smoothTime);
        }

        private void LateUpdate()
        {
            cameraTransform.position = camPos;
        }

        public Vector2 WorldPosToScreen(Vector3 worldPos) => cam.WorldToScreenPoint(worldPos);
    }
}