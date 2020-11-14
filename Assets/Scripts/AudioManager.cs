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
        foreach (Sound element in sounds)
        {
            if (element != null)
            {
                if (element.name == name)
                {
                    element.source.Play();
                }
            }
            else
            {
                Debug.Log("Audio Null");
            }
        }
    }
    public void Stop(string name)
    {
        foreach (Sound element in sounds)
        {
            if (element != null)
            {
                if (string.Equals(element.name.Trim(),name.Trim()))
                {
                    element.source.Stop();
                }
                else
                {
                    Debug.Log("Wrong Audio name");
                }
            }
            else
            {
                Debug.Log("Audio Null");
            }
        }
    }
}
