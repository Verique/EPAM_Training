using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    [RequireComponent(typeof(Text))]
    public class LevelText : UIText<HUDManager>
    {
        private void UpdateText(int newLvl)
        {
            Text.text = $"Level : {newLvl}";
        }

        public override void Init(HUDManager manager)
        {
            Text = GetComponent<Text>();
            manager.PlayerLevelUp += UpdateText;
        }
    }
}
