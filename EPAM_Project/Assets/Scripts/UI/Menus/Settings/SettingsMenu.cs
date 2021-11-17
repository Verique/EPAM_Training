using Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menus.Settings
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private Button closeButton;
        
        [SerializeField] private SliderSetting enemySpeed;
        [SerializeField] private SliderSetting musicVolumeSetting;
        [SerializeField] private SliderSetting effectsVolumeSetting;

        private void Awake()
        {
            closeButton.onClick.AddListener(ApplySettings);
            closeButton.onClick.AddListener(Hide);
        }

        private void ApplySettings()
        {
            enemySpeed.Apply();
            musicVolumeSetting.Apply();
            effectsVolumeSetting.Apply();
            
            ServiceLocator.Instance.Get<SoundManager>().ApplySettings();
        }

        private void Hide() => SetActive(false);
        public void Show() => SetActive(true);

        private void SetActive(bool isActive)
        {
            settingsPanel.SetActive(isActive);   
            closeButton.gameObject.SetActive(isActive);
        }
    }
}