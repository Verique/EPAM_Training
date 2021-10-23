using Stats;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SpeedSetting : MonoBehaviour
    {
        //this is terrible
        private Slider slider;

        [SerializeField] private BaseStats stats;
        private void Start()
        {
            slider = GetComponent<Slider>();
            slider.value = stats.floatStats.Get("speed");
        }

        public void ChangeSetting(float value)
        {
            stats.floatStats.Set("speed", value);
        }
    }
}
