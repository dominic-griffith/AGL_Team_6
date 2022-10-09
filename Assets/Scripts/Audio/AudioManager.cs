using System;
using UnityEngine;

namespace AudioUtility
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        
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
    }
}

