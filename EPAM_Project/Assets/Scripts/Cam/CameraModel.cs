using Services;
using UnityEngine;

namespace Cam
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraModel : MonoBehaviour
    {
        public Vector3 CamPos { get; set; }
        [SerializeField] private float smoothTime = 0.2f;
        [SerializeField] private Vector3 offset;
        [SerializeField] private Transform trackTarget;
        public Transform TrackTarget => trackTarget;
        public Vector3 Offset => offset;
        public Camera Cam { get; private set; }

        public float SmoothTime => smoothTime;

        private void Start()
        {
            Cam = GetComponent<Camera>();
            ServiceLocator.Instance.Get<CameraController>().Init(this);
        }

        private void LateUpdate()
        {
            transform.position = CamPos;
        }
    }
}
