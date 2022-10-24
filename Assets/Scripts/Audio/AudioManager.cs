using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AudioUtility
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        [Header("Level Music")] 
        [SerializeField]
        private string menuSceneThemeSong;
        [SerializeField]
        private string gameplaySceneLevel01;
        
        [Header("All audio sources go here")]
        [SerializeField]
        private SoundSource[] audioSources;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        
            foreach (var s in audioSources)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
    
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }
        
        public void Play(string name)
        {
            SoundSource s = Array.Find(audioSources, sound => sound.name == name);
    
            if (s == null)
            {
                Debug.LogWarning("sound not found check spelling for: " + name);
                return;
            }
    
            s.source.Play();
        }
        
        public void StopSound(string name)
        {
            SoundSource s = Array.Find(audioSources, sound => sound.name == name);
    
            if (s == null)
            {
                Debug.LogWarning("sound not found check spelling for: " + name);
                return;
            }
            s.source.Stop();
        }
        
        public void StopAllSound()
        {
            foreach (SoundSource s in audioSources)
            {
                if (s == null)
                {
                    Debug.LogWarning("sound not found check spelling for: " + name);
                    return; 
                }
                s.source.Stop();
            }
        }
    
    
    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        StopAllSound();
        if (scene.name == menuSceneThemeSong)
        {
            Play("MenuMusic");
        }
        else if (scene.name == gameplaySceneLevel01)
        {
            Play("Level1Music");
        }
    }
        
    public void ChangeVolumeAllOfType(float volumeAmount, TypeOfAudio audioType)
    {
        foreach (SoundSource s in audioSources)
        {
            if (s == null)
            {
                Debug.LogWarning("sound not found check spelling for: " + name);
                return; 
            }
                
            if(s.audioType == audioType)
                s.source.volume = volumeAmount;
        }
    }

    public float GetBackgroundVolumeLevel()
    {
        SoundSource s = Array.Find(audioSources, sound => sound.audioType == TypeOfAudio.Background);
        if (s == null)
        {
            Debug.LogWarning("sound not found check spelling for: " + name);
            return 0;
        }
        return s.source.volume;
    }
        
    public float GetSoundEffectsVolumeLevel()
    {
        SoundSource s = Array.Find(audioSources, sound => sound.audioType == TypeOfAudio.SoundEffects);
        if (s == null)
        {
            Debug.LogWarning("sound not found check spelling for: " + name);
            return 0;
        }
        return s.source.volume;
    }
}
}

