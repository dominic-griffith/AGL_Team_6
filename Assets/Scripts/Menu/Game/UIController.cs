using AudioUtility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gameplay
{
    public class UIController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField]
        private Slider backgroundVolumeSlider;
        [SerializeField]
        private Slider soundEffectsVolumeSlider;
        
        [Header("Menu Settings")] [SerializeField]
        private string sceneToLoad;
        
        [Header("Pause Settings")]
        [SerializeField]
        private bool paused;
        
        [Header("Event subscribers")][Tooltip("Add functions that will be called when timer reaches 0")] 
        [SerializeField] private UnityEvent OnGamePauseEvent;
        [SerializeField] private UnityEvent OnGameUnPauseEvent;
        
        
        //inner methods
        private bool _isDead;

        private void Awake()
        {
            _isDead = false;
            paused = false;
        }
        
        private void Start()
        {
            if (AudioManager.Instance == null) return;
            
            backgroundVolumeSlider.value = AudioManager.Instance.GetBackgroundVolumeLevel();
            soundEffectsVolumeSlider.value = AudioManager.Instance.GetSoundEffectsVolumeLevel();
        }

        public void Resume()
        {
            OnLockCursor();
        }

        public void Settings()
        {
            MenuManager.Instance.OpenMenu("SettingsMenu");
        }

        public void Back()
        {
            MenuManager.Instance.OpenMenu("PauseMenu");
        }

        public void BackToMainMenu()
        {
            SceneManager.LoadScene(sceneToLoad);
        }

        public void TryAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void Update()
        {
            if (_isDead) return;

            UpdatePauseInput();
        }

        private void UpdatePauseInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                if (paused)
                    ResumeGame();
                else 
                    PauseGame();
            }
        }

        private void ResumeGame()
        {
            paused = false;
            MenuManager.Instance.CloseMenu("PauseMenu");
            OnLockCursor();
        }   

        private void PauseGame()
        {
            paused = true;
            MenuManager.Instance.OpenMenu("PauseMenu");
            OnUnlockCursor();
        }
        
        public void OnLockCursor()
        {
            OnGameUnPauseEvent?.Invoke();
            MenuManager.Instance.CloseAllOpenMenus();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void OnUnlockCursor()
        {
            OnGamePauseEvent?.Invoke();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        
        // Set this to true to prevent player from opening pause menu
        public void SetPausedDead(bool isPlayerDead)
        {
            _isDead = isPlayerDead;
        }
        
        public void OnChangedVolumeBackground()
        {
            AudioManager.Instance.ChangeVolumeAllOfType(backgroundVolumeSlider.value, TypeOfAudio.Background);
        }
        
        public void OnChangedVolumeSoundEffects()
        {
            AudioManager.Instance.ChangeVolumeAllOfType(soundEffectsVolumeSlider.value, TypeOfAudio.SoundEffects);
        }
        
        // winning behavior here
        public void OnPlayerWon()
        {
            
        }
    }
    
    
}

