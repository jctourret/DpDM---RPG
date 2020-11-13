using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        foreach (Sound element in sounds)
        {
            element.source = gameObject.AddComponent<AudioSource>();
            element.source.clip = element.clip;
            element.source.volume = element.volume;
            element.source.pitch = element.pitch;
            element.source.loop = element.loop;
            element.source.playOnAwake = element.OnAwake;
        }
        DontDestroyOnLoad(gameObject);
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.Log("Wrong Audio Name");
            return;
        }
        s.source.Play(); 
    }
}
