using UnityEngine;

namespace UI.Menus
{
    public class PauseScreen : MonoBehaviour
    {
        public void SetActive(bool active) => gameObject.SetActive(active);

        private void OnDisable()
        {
            Time.timeScale = 1;
        }

        private void OnEnable()
        {
            Time.timeScale = 0;
        }
    }
}
