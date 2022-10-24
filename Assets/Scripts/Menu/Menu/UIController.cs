using AudioUtility;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class UIController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField]
        private Slider backgroundVolumeSlider;
        [SerializeField]
        private Slider soundEffectsVolumeSlider;
        
        [Header("Settings")] [SerializeField]
        private string sceneToLoad;

        private void Start()
        {
            if (AudioManager.Instance == null) return;
            
            backgroundVolumeSlider.value = AudioManager.Instance.GetBackgroundVolumeLevel();
            soundEffectsVolumeSlider.value = AudioManager.Instance.GetSoundEffectsVolumeLevel();
        }

        public void StartGame()
        {
            SceneManager.LoadScene(sceneToLoad);
        }

        public void About()
        {
            MenuManager.Instance.OpenMenu("AboutMenu");
        }
        
        public void Settings()
        {
            MenuManager.Instance.OpenMenu("SettingsMenu");
        }

        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        public void Back()
        {
            MenuManager.Instance.OpenMenu("MainMenu");
        }

        public void OnChangedVolumeBackground()
        {
            AudioManager.Instance.ChangeVolumeAllOfType(backgroundVolumeSlider.value, TypeOfAudio.Background);
        }
        
        public void OnChangedVolumeSoundEffects()
        {
            AudioManager.Instance.ChangeVolumeAllOfType(soundEffectsVolumeSlider.value, TypeOfAudio.SoundEffects);
        }
    }
}

