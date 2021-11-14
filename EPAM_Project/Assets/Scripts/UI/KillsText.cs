using Services;
using UnityEngine.UI;

namespace UI
{
    public class KillsText : UIText
    {
        private void UpdateText(int kills, int goal)
        {
            Text.text = $"Kills : {kills} Goal : {goal}";
        }

        public override void Init(UIManager manager)
        {
            Text = GetComponent<Text>();
            manager.PlayerKills += UpdateText;
        }
    }
}