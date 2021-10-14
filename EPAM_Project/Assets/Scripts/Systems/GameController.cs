using System;
using UnityEngine;

namespace Systems
{
    public class GameController : MonoBehaviour
    {
        private void Start()
        {
            var player = Services.Instance.Get<PlayerManager>();

            Services.Instance.Get<CameraManager>().Target = player.Transform;
        }
    }
}