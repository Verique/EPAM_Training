using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Text))]
    public class SettingsNumberText : MonoBehaviour
    {

        private Text text;
        private void Awake()
        {
            text = GetComponent<Text>();
        }

        public void ChangeText(float value)
        {
            text.text = value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
