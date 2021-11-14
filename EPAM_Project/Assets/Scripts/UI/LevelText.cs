using Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Text))]
    public class LevelText : UIText
    {
        private void UpdateText(int newLvl)
        {
            Text.text = $"Level : {newLvl}";
        }

        public override void Init(UIManager manager)
        {
            Text = GetComponent<Text>();
            manager.PlayerLevelUp += UpdateText;
        }
    }
}
