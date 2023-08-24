using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        foreach (var sound in sounds) 
        {
            sound.soundSource = gameObject.AddComponent<AudioSource>();
            sound.soundSource.clip = sound.clip;
            sound.soundSource.volume = sound.volume;
            sound.soundSource.pitch = sound.pitch;
            sound.soundSource.loop = sound.loop;
        }


        playSound("Main Music");

    }

    public void playSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null) 
        {
            return;
        }

        s.soundSource.Play();
    }


}
