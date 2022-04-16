using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    // public static AudioManager instance;

    void Awake()
    {
        /* if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject); */

        foreach (var item in sounds)
        {
            item.audioSource = gameObject.AddComponent<AudioSource>();
            item.audioSource.clip = item.clip;

            item.audioSource.volume = item.volume;
            item.audioSource.pitch = item.pitch;
            item.audioSource.loop = item.loop;
        }
    }

    void Start()
    {
        // Oyunun başlangıcında çalınması istenilen ses dosyası için...
        //Play("Another_Sound");
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        // Parametre(name) olarak yanlış isimde bir isim gönderirsek uyarı...
        if(s == null)
        {
            Debug.Log("Sound '" + name + "' not found!");
            return;
        }
        if (s.audioSource.isPlaying)
        {
            s.audioSource.Stop();
        }
        s.audioSource.Play();
    }
}
