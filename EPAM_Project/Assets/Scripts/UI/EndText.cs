using Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EndText : UIText
    {
        [SerializeField] private Text msgText;
        [SerializeField] private Text killsText;
        [SerializeField] private Text timeText;

        private void OnGameEnded(GameStats stats)
        {
            gameObject.SetActive(true);
            msgText.text = stats.Message;
            killsText.text = $"Kills : {stats.Kills}";
            timeText.text = $"Time Survived : {stats.TimeSurvived}";
        }

        public override void Init(UIManager manager)
        {
            manager.GameEnded += OnGameEnded;
        }
    }
}