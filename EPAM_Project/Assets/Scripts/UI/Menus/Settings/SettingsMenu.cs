using SaveData;
using Services;
using UI.Menus.MainMenu;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menus.Settings
{
    public class SettingsMenu : UIMenu<IHasSettingsMenu>
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private SliderSetting enemySpeed;
        [SerializeField] private VolumeSetting musicVolumeSetting;
        [SerializeField] private VolumeSetting effectsVolumeSetting;


        private GameAudioSettings audioSettings;
        private SaveManager saveManager;
        private void Awake()
        {
            saveManager = ServiceLocator.Instance.Get<SaveManager>();
            
            closeButton.onClick.AddListener(ApplySettings);
            closeButton.onClick.AddListener(Hide);
            
            audioSettings = saveManager.LoadAudioSettings();
            musicVolumeSetting.Init(audioSettings);
            effectsVolumeSetting.Init(audioSettings);
        }

        private void ApplySettings()
        {
            enemySpeed.Apply();
            musicVolumeSetting.Apply();
            effectsVolumeSetting.Apply();
            
            saveManager.SaveAudioSettings(audioSettings);
            ServiceLocator.Instance.Get<SoundManager>().ApplySettings(audioSettings);
        }

        public override void Init(IHasSettingsMenu manager)
        {
            manager.SettingsOpened += Show;
        }
    }
}