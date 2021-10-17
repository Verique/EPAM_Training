using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Systems
{
    public class CameraManager : MonoBehaviour, IService
    {
        [SerializeField] private float smoothTime = 0.2f;
        [SerializeField] private Vector3 offset;
        [SerializeField] private Camera cam;
        
        public Camera Cam => cam;

        private Transform cameraTransform;
        public Transform Target { get; set; }
        
        private Vector3 camPos;
        private Vector3 mousePos;
        private Vector3 sDampVelocity = Vector3.zero;

        private void Start()
        {
            cameraTransform = cam.transform;
            Services.Instance.Get<InputManager>().MouseMoved += ChangeMousePos;
        }

        private void ChangeMousePos(Vector3 mousePos)
        {
            this.mousePos = mousePos;
        }

        private void FixedUpdate()
        {
            camPos = Vector3.Lerp(Target.position, mousePos, 0.2f) - offset;
            camPos = Vector3.SmoothDamp(cameraTransform.position, camPos, ref sDampVelocity, smoothTime);
        }

        private void LateUpdate()
        {
            cameraTransform.position = camPos;
        }

        public void Register() 
        {
            Services.Instance.Add(this);
        }
    }
}