using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource musicSource; // Arrastra el AudioSource aquí
    public AudioClip musicClip;    // Arrastra la música aquí
    public AudioClip boss;
    void Start()
    {
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
}
