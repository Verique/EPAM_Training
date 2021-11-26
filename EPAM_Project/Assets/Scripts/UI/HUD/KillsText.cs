using UnityEngine.UI;

namespace UI.HUD
{
    public class KillsText : UIText<HUDManager>
    {
        private void UpdateText(int kills, int goal)
        {
            Text.text = $"Kills : {kills} Goal : {goal}";
        }

        public override void Init(HUDManager manager)
        {
            Text = GetComponent<Text>();
            manager.PlayerKills += UpdateText;
        }
    }
}