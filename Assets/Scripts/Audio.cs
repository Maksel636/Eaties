using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
    private AudioClip _soundTrack;
    public Sound[] Sounds;
    public enum SoundType
    {
        BladeEnemyContact,
        BladePlayerContact,
        BladeReflection,
        Reload,
        BladeCharge,
        BladeLaunch,
        PlayerHitByEnemy
    }
    public static Audio Instance;
    [System.Serializable]
    public class Sound
    {
        public SoundType Type;
        public AudioResource Clip;
        public float duration;
        [Range(0, 1)] public float Volume;
        [HideInInspector] AudioSource Source;
    }
    private Dictionary<SoundType, Sound> _soundDictionary = new Dictionary<SoundType, Sound>();

    private void Awake()
    {
        Instance = this;
        foreach (var sound in Sounds)
        {
            _soundDictionary[sound.Type] = sound;
        }
    }

    public void InitiateSound(SoundType type)
    {
        if (!_soundDictionary.TryGetValue(type, out Sound sound))
        {
            Debug.LogWarning("SoundNotFound");
            return;
        }
        GameObject soundObject = new GameObject(type.ToString());
        AudioSource source = soundObject.AddComponent<AudioSource>();
        source.resource = sound.Clip;
        source.volume = sound.Volume;
        source.Play();
        Destroy(soundObject, sound.duration);
    }
}
