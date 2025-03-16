using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    void Awake()
    {
        // Asignamos la instancia y evitamos duplicados
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantener la música en toda la escena
        }
        else
        {
            Destroy(gameObject); // Evitar duplicados si hay otra instancia
        }
    }
    public static BackgroundMusic Instance { get; private set; }
    public AudioSource musicSource; // Arrastra el AudioSource aquí
    public AudioClip musicClip;    // Arrastra la música aquí
    public AudioClip boss;
    void Start()
    {
        SetVolume(0.3f);
        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.Play();
    }
   public void bossfinal()
    {
        musicSource.Stop();
        musicSource.clip = boss;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void SetVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp(volume, 0f, 1f); // Asegurar que esté entre 0 y 1
    }
}
