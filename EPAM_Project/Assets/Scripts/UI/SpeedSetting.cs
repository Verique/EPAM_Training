using Stats;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SpeedSetting : MonoBehaviour
    {
        private Slider slider;

        [SerializeField] private StatsScriptable stats;
        private void Start()
        {
            slider = GetComponent<Slider>();
            slider.value = stats.GetFloat("speed");
        }

        public void ChangeSetting(float value)
        {
            stats.SetFloat("speed", value);
        }
    }
}
