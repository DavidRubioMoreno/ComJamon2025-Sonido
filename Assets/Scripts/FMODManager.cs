using System.Collections;
using UnityEngine;
using FMODUnity;

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