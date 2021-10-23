using Stats;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SpeedSetting : MonoBehaviour
    {
        //this is terrible
        private Slider slider;

        [SerializeField] private EnemyDataLoader enemyData;

        private void Start()
        {
            slider = GetComponent<Slider>();
            slider.value = enemyData.baseSpeed;
        }

        public void ChangeSetting(float value)
        {
            enemyData.baseSpeed = value;
        }
    }
}
