using Stats;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SpeedSetting : MonoBehaviour
    {
        private Slider slider;

        [SerializeField] private EnemyStats baseStats;
        private void Start()
        {
            slider = GetComponent<Slider>();
            slider.value = baseStats.Speed.Value;
        }

        public void ChangeSetting(float value)
        {
            baseStats.Speed.Value = value;
        }
    }
}
