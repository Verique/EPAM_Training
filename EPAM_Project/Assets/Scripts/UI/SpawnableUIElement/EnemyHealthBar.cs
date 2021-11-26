using Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI.SpawnableUIElement
{
    [RequireComponent(typeof(Image), typeof(RectTransform))]
    public class EnemyHealthBar : SpawnableUIElement
    {
        public override void EventHandler<T>(T param)
        {
            if (!(param is int health)) return;
            
            var width = initialBarSize.x * health / Prefs.MaxValue;
            barTransform.sizeDelta = new Vector2(width, initialBarSize.y);
        }

        private CameraManager cameraManager;
        private Vector2 initialBarSize;
        private RectTransform barTransform;

        private void Start()
        {
            cameraManager = ServiceLocator.Instance.Get<CameraManager>();
        }

        private void Awake()
        {
            barTransform = GetComponent<RectTransform>();
            initialBarSize = barTransform.sizeDelta;
        }

        private void OnEnable()
        {
            barTransform.sizeDelta = initialBarSize;
        }

        private void LateUpdate()
        {
            barTransform.position = cameraManager.WorldPosToScreen(Prefs.Target.position) + Prefs.Offset;
        }
    }
}