using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioUtility
{
    [System.Serializable]
    public class SoundSource
    {
        public string name;

        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume;
        [Range(.1f, 3f)]
        public float pitch;

        public bool loop;

        public TypeOfAudio audioType;

        [HideInInspector]
        public AudioSource source;
    }
    public enum TypeOfAudio
    {
        SoundEffects,
        Background
    }
}

