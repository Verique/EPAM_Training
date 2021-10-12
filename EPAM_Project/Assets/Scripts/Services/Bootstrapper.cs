using Cam;
using Player;
using UnityEngine;

namespace Services
{
    public static class Bootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            ServiceLocator.Instance.Add(new UserInputController());
            ServiceLocator.Instance.Add(new PlayerController());
            ServiceLocator.Instance.Add(new CameraController());
        }
    }
}