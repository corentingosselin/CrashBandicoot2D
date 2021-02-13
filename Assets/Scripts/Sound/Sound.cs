using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class Sound
{
    public String name;
    public AudioClip audioClip;
    [Range(0,1F)]
    public float volume = 1;
    [Range(0.1F,3F)]
    public float pitch = 1;

    public bool loop = false;
    public bool startAwake = false;

    [HideInInspector]
    public AudioSource source;
}
