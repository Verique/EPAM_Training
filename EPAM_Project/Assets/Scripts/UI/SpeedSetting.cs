using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SpeedSetting : MonoBehaviour
    {
        private Slider slider;
        private void Start()
        {
            slider = GetComponent<Slider>();
            slider.value = PlayerPrefs.GetFloat("EnemySpeed");
        }

        public void ChangeSetting(float value)
        {
            PlayerPrefs.SetFloat("EnemySpeed", value);
        }
    }
}
