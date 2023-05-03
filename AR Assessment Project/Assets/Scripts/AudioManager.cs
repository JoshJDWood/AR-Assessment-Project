using UnityEngine.Audio;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] numberSounds;
    [SerializeField] private Sound[] speachSounds;

    private void Awake()
    {
        SetUpSounds(numberSounds);
        SetUpSounds(speachSounds);
    }

    private void SetUpSounds(Sound[] sounds)
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.mute = s.mute;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name, int ArrayId)
    {
        Sound s = null;
        if (ArrayId == 1)
        {
            s = Array.Find(numberSounds, sound => sound.name == name);
        }
        else if (ArrayId == 2)
        {
            s = Array.Find(speachSounds, sound => sound.name == name);
        }
        
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Play();
    }

    public void MuteSounds(bool mute)
    {
        foreach (Sound s in numberSounds)
        {
            s.source.mute = mute;
        }
        foreach (Sound s in speachSounds)
        {
            s.source.mute = mute;
        }
    }
}
