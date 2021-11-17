using UnityEngine;
using UnityEngine.UI;

namespace UI.Menus.Settings
{
    public class MuteButton : MonoBehaviour
    {
        private Button button;
        private Image image;
        public bool IsMuted { get; private set; }

        private void Awake()
        {
            button = GetComponent<Button>();
            image = GetComponent<Image>();
            button.onClick.AddListener(MuteToggle);
        }

        public void Mute(bool muted)
        {
            IsMuted = muted;
            image.color = IsMuted ? Color.red : Color.green;
        }

        private void MuteToggle() => Mute(!IsMuted);
    }
}