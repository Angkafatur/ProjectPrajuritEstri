using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class AudioSetting : MonoBehaviour
{
    public static AudioSetting Instance;
    public Sound[] suaraSounds;
    public AudioSource suaraSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlaySuara("Thema");
    }

    public void PlaySuara(string name)
    {
        Sound s = Array.Find(suaraSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            suaraSource.clip = s.clip;
            suaraSource.Play();
        }
    }

    public void SuaraVolume(float volume)
    {
        suaraSource.volume = volume;
    }
}
