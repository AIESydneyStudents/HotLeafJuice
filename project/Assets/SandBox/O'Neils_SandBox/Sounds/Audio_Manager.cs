using System.Collections;
using System.Collections.Generic;
using System.Media;
using UnityEngine;
using System;
using UnityEngine.Audio;


public class Audio_Manager : MonoBehaviour
{
    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

    }

    void Start()
    {
        Play("Player_Walk");
    }

    public void Play (string name)
    {
       Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }
            
        s.source.Play();
    }
}
