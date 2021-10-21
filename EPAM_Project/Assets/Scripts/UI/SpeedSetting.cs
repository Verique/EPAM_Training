using Stats;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SpeedSetting : MonoBehaviour
    {
        //this is terrible
        private Slider slider;

        [SerializeField] private StatsScriptable stats;
        private void Start()
        {
            slider = GetComponent<Slider>();
            slider.value = stats.floatStats.Find((stat => (stat.name == "speed"))).value;
        }

        public void ChangeSetting(float value)
        {
            stats.floatStats[stats.floatStats.FindIndex((stat => (stat.name == "speed")))] = new Stat<float>("speed", value);
        }
    }
}
