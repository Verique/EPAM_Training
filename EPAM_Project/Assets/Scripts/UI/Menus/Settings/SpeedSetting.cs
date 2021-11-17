using Stats;
using UnityEngine;

namespace UI.Menus.Settings
{
    public class SpeedSetting : SliderSetting
    {
        [SerializeField] private EnemyStats baseStats;

        protected override void Init()
        {
            slider.maxValue = baseStats.Speed.maxValue;
            slider.minValue = baseStats.Speed.minValue;
            slider.value = baseStats.Speed.Value;
        }

        public override void Apply()
        {
            baseStats.Speed.Value = slider.value;
        }
    }
}
