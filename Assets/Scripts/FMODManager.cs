using FMOD.Studio;
using FMODUnity;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FMODManager : MonoBehaviour
{
    // Instancia única (singleton)
    public static FMODManager instance;

    // El sistema de audio FMOD
    private FMOD.Studio.System fmodSystem;

    [SerializeField]
    FMODUnity.EventReference enemyDamaged;   // Seleccionado desde el inspector

    [SerializeField]
    FMODUnity.EventReference bossDamaged;

    [SerializeField]
    FMODUnity.EventReference enemydead;

    [SerializeField]
    FMODUnity.EventReference mageDamage;

    [SerializeField]
    FMODUnity.EventReference mageThunder;

    [SerializeField]
    FMODUnity.EventReference mageInvoking;

    [SerializeField]
    FMODUnity.EventReference invokeEnemy;

    [SerializeField]
    FMODUnity.EventReference shoorArrowEnemy;

    [SerializeField]
    FMODUnity.EventReference enemySwordAttack;

    [SerializeField]
    FMODUnity.EventReference slowBoss;

    [SerializeField]
    FMODUnity.EventReference bigFireBalls;

    [SerializeField]
    FMODUnity.EventReference portal;

    [SerializeField]
    FMODUnity.EventReference damageBalls;

    [SerializeField]
    FMODUnity.EventReference carcel;

    [SerializeField]
    FMODUnity.EventReference heal;

    [SerializeField]
    FMODUnity.EventReference balls3;


    [SerializeField]
    FMODUnity.EventReference buttonHover;

    [SerializeField]
    FMODUnity.EventReference buttonPress;

    [SerializeField]
    FMODUnity.EventReference animPortalMusic;

    [SerializeField]
    FMODUnity.EventReference animPortalSpawn;

    [SerializeField]
    FMODUnity.EventReference menuMusic;
    public FMOD.Studio.EventInstance menuMusicInstance;

    private Bus masterBus, musicBus, sfxBus;
    // Asegurarse de que solo hay una instancia
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Mantener AudioManager entre escenas
        }
        else
        {
            Destroy(gameObject);  // Evitar duplicados
        }

        // Inicializar FMOD Studio
        FMOD.Studio.System.create(out fmodSystem);
        masterBus = RuntimeManager.GetBus("bus:/");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
        musicBus = RuntimeManager.GetBus("bus:/Music");
    }

    private void Start()
    {
        menuMusicInstance = FMODManager.instance.CreateEventInstance(menuMusic);

        menuMusicInstance.start();
    }

    public void ExitSelector()
    {
        menuMusicInstance.setParameterByName("MenuSong", 1);
    }

    //Cambiar el volumen general
    public void SetMasterVolume(float volume)
    {
        // Limita el valor para asegurar que esté entre 0 y 1.
        float clampedVolume = Mathf.Clamp01(volume);

        // Aplica el volumen al Bus Maestro.
        masterBus.setVolume(clampedVolume);
    }
    //Cambiar el volumen de la música
    public void SetMusicVolume(float volume)
    {
        // Limita el valor para asegurar que esté entre 0 y 1.
        float clampedVolume = Mathf.Clamp01(volume);

        // Aplica el volumen al Bus Maestro.
        musicBus.setVolume(clampedVolume);
    }
    //Cambiar el volumen de los efectos de sonido
    public void SetSFXVolume(float volume)
    {
        // Limita el valor para asegurar que esté entre 0 y 1.
        float clampedVolume = Mathf.Clamp01(volume);

        // Aplica el volumen al Bus Maestro.
        sfxBus.setVolume(clampedVolume);
    }
    public void PlayEnemyDamaged()
    {
        RuntimeManager.PlayOneShot(enemyDamaged);
    }

    public void PlayBossDamaged()
    {
        RuntimeManager.PlayOneShot(bossDamaged);
    }

    public void PlayEnemyDead()
    {
        RuntimeManager.PlayOneShot(enemydead);
    }

    public void PlayMageAttack()
    {
        RuntimeManager.PlayOneShot(mageDamage);
    }

    public void PlayMageThunder()
    {
        RuntimeManager.PlayOneShot(mageThunder);
    }

    public void PlayMageInvoking()
    {
        RuntimeManager.PlayOneShot(mageInvoking);
    }

    public void PlayInvokeEnemy()
    {
        RuntimeManager.PlayOneShot(invokeEnemy);
    }

    public void PlayHeal()
    {
        RuntimeManager.PlayOneShot(heal);
    }

    public void Play3Balls()
    {
        RuntimeManager.PlayOneShot(balls3);
    }

    public void PlayButtonHover()
    {
        RuntimeManager.PlayOneShot(buttonHover);
    }

    public void PlayButtonPress()
    {
        RuntimeManager.PlayOneShot(buttonPress);
    }

    public void PlayAnimPortal()
    {
        RuntimeManager.PlayOneShot(animPortalMusic);
    }

    public void PlatANimPortalSpawn()
    {
        RuntimeManager.PlayOneShot(animPortalSpawn);
    }



    public void PlayBigFireBalls()
    {
        RuntimeManager.PlayOneShot(bigFireBalls);
    }

    public void PlaySlowBoss()
    {
        RuntimeManager.PlayOneShot(slowBoss);
    }

    public void PlayPortalSound()
    {
        RuntimeManager.PlayOneShot(portal);
    }

    public void PlaydamageBalls()
    {
        RuntimeManager.PlayOneShot(damageBalls);
    }

    public void PlayShootArrow()
    {
        RuntimeManager.PlayOneShot(shoorArrowEnemy);
    }

    public void PlaySwordEnemy()
    {
        RuntimeManager.PlayOneShot(enemySwordAttack);
    }

    public void PlayCarcel()
    {
        RuntimeManager.PlayOneShot(carcel);
    }

    public FMOD.Studio.EventInstance CreateEventInstance(EventReference eventRef)
    {
        FMOD.Studio.EventInstance instance = RuntimeManager.CreateInstance(eventRef);
        return instance;
    }

    // Método para reproducir un evento FMOD
    public void PlayEvent(string eventPath)
    {
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        eventInstance.start();
        eventInstance.release(); // Release al terminar
    }

    // Método para reproducir un evento FMOD 2D (sin ubicación en el espacio)
    public void Play2DEvent(string eventPath)
    {
        FMODUnity.RuntimeManager.PlayOneShot(eventPath);
    }

    // Método para parar un evento
    public void StopEvent(FMOD.Studio.EventInstance eventInstance)
    {
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        eventInstance.release();
    }

    // Método para pausar un evento
    public void PauseEvent(FMOD.Studio.EventInstance eventInstance)
    {
        eventInstance.setPaused(true);
    }

    // Método para reanudar un evento
    public void ResumeEvent(FMOD.Studio.EventInstance eventInstance)
    {
        eventInstance.setPaused(false);
    }

    // Ejemplo de un método que se podría llamar desde otros scripts
    public void PlaySoundEffect(string eventName)
    {
        string eventPath = "event:/" + eventName;  // Convierte el nombre del evento en el path correcto
        PlayEvent(eventPath);
    }


}