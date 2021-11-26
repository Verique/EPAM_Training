using System;
using UnityEngine;

namespace Services
{
    public class CameraManager : MonoBehaviour, IService
    {
        private const float SmoothTime = 0.2f;
        
        [SerializeField] private Vector3 offset;
        [SerializeField] private Camera cam;

        private Vector3 camPos;
        private Vector3 mousePos;
        private Vector3 sDampVelocity = Vector3.zero;
        
        public event Action<Vector2> TargetMovedOnScreen;
        public ITarget Target { get; set; }

        public bool TryGetPointerPosInWorld(Vector2 pointerPos, out Vector3 worldPos)
        {
            var isPointerInWorld = Physics.Raycast(cam.ScreenPointToRay(pointerPos), out var hit);
            worldPos = isPointerInWorld ? hit.point : Vector3.zero;
            return isPointerInWorld;
        }

        private void Start()
        {
            ServiceLocator.Instance.Get<InputManager>().MouseMoved += ChangeMousePos;
            ServiceLocator.Instance.Get<GameManager>().GameEnded += stats => enabled = false;
        }

        private void ChangeMousePos(Vector3 newMousePos)
        {
            mousePos = newMousePos;
        }

        private void Update()
        {
            camPos = Vector3.Lerp(Target.Position, mousePos, 0.2f) - offset;
            camPos = Vector3.SmoothDamp(cam.transform.position, camPos, ref sDampVelocity, SmoothTime);
            TargetMovedOnScreen?.Invoke(WorldPosToScreen(Target.Position));
        }

        private void LateUpdate()
        {
            cam.transform.position = camPos;
        }

        public Vector2 WorldPosToScreen(Vector3 worldPos) => cam.WorldToScreenPoint(worldPos);
    }
}