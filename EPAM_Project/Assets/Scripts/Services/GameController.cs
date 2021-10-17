using UnityEngine;

namespace Services
{
    public class GameController : MonoBehaviour
    {
        private void Start()
        {
            var player = ServiceLocator.Instance.Get<PlayerManager>();

            ServiceLocator.Instance.Get<CameraManager>().Target = player.Transform;
        }
    }
}