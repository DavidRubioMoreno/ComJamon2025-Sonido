using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource sfxSource; 
    public AudioClip mejora;
    public AudioClip espada;
    public AudioClip espada2;
    public AudioClip escudo;
    public AudioClip magia;
    public AudioClip masmagia;
    public AudioClip opciones;
    public AudioClip salir;
    public AudioClip Jugarnueva;
    public AudioClip jugarcargada;
    public AudioClip eligemeM;
    public AudioClip eligemeC;

    public AudioClip bosrojomet;
    public AudioClip bossrojobolon;
    public AudioClip portales;
    public AudioClip finmazmorra;
    public AudioClip muerte;
    public AudioClip dash;

    public AudioClip invocar;
    public AudioClip bruja;
    public AudioClip arco;
    public AudioClip espadaesqueleto;
    public AudioClip giro;

    public AudioClip carcel;
    public AudioClip bolasrota;
    public AudioClip toxico;
    public AudioClip pegao;

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

    public void PlaySound(AudioClip clip, float vol = 1f)
    {
        if (sfxSource.isPlaying && GameManager.Instance.menu)
        {
            sfxSource.Stop(); // Detener el sonido actual
        }
        sfxSource.PlayOneShot(clip,vol);
    }
}

