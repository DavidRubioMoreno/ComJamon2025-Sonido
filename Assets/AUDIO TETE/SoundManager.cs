using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource sfxSource; 
    public AudioClip mejora;
    public AudioClip opciones;
    public AudioClip salir;
    public AudioClip Jugarnueva;
    public AudioClip jugarcargada;
    public AudioClip eligemeM;
    public AudioClip eligemeC;
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

    public void PlaySound(AudioClip clip)
    {
        if (sfxSource.isPlaying && GameManager.Instance.menu)
        {
            sfxSource.Stop(); // Detener el sonido actual
        }
        sfxSource.PlayOneShot(clip);
    }
}

