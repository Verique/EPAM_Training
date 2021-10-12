using Services;
using UnityEngine;

namespace Cam
{
    public class CameraController : IService
    {
        private UserInputController userInput;
        private CameraModel model;
        private Vector3 mPos;
        private Vector3 sDampVelocity = Vector3.zero;
        public UnityEngine.Camera Cam => model.Cam;

        public void Init(CameraModel camModel)
        {
            model = camModel;
            userInput = ServiceLocator.Instance.Get<UserInputController>();
            userInput.MouseMoved += AdjustCamera;
        }

        private void AdjustCamera(Vector3? mousePos)
        {
            var targetPos = model.TrackTarget.position;
            
            if (mousePos != null)
                mPos = mousePos.GetValueOrDefault();

            model.CamPos = Vector3.Lerp(targetPos, mPos, 0.2f) - model.Offset;
            model.CamPos = Vector3.SmoothDamp(Cam.transform.position, model.CamPos, ref sDampVelocity, model.SmoothTime);
        }
    }
}
