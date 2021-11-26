using Enemy;
using Services;
using Stats;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace UI.SpawnableUIElement
{
    [RequireComponent(typeof(Image), typeof(RectTransform))]
    public class EnemyHealthBar : SpawnableUIIntStatView
    {
        private readonly Vector2 hpOffset = new Vector3(-30, 20);
        private CameraManager cameraManager;
        private Vector2 initialBarSize;
        private RectTransform barTransform;
        private Vector2 position;

        public EnemyHasHpBar Enemy { get; set; }

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
            barTransform.position = position;
        }

        public override void OnValueChanged(Stat<int> healthStat)
        {
            var width = initialBarSize.x * healthStat.Value / healthStat.maxValue;
            barTransform.sizeDelta = new Vector2(width, initialBarSize.y);
        }

        public override void OnTargetMoved(Vector3 newPos)
        {
            position = cameraManager.WorldPosToScreen(newPos) + hpOffset;
        }
    }
}