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
            ServiceLocator.Instance.Get<GameManager>().EnemyKilled += UpdateText;
        }

        private void UpdateText(int kills, int goal)
        {
            text.text = $"Kills : {kills} Goal : {goal}";
        }
    }
}