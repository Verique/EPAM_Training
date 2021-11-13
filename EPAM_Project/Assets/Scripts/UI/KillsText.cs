using Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class KillsText : MonoBehaviour
    {
        private Text text;

        private void Start()
        {
            text = GetComponent<Text>();
            var gManager = ServiceLocator.Instance.Get<GameManager>();
            gManager.EnemyKilled += UpdateText;
            UpdateText(0, gManager.KillGoal);
        }

        private void UpdateText(int kills, int goal)
        {
            text.text = $"Kills : {kills} Goal : {goal}";
        }
    }
}