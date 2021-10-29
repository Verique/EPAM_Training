using Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Text))]
    public class LevelText : MonoBehaviour
    {
        private Text text;

        private void Start()
        {
            text = GetComponent<Text>();
            ServiceLocator.Instance.Get<PlayerManager>().StatLoader.Stats.Level.ValueChanged += UpdateText;
        }

        private void UpdateText(int newLvl)
        {
            text.text = $"Level : {newLvl}";
        }
    }
}
