using System.Collections;
using UnityEngine;
using FMODUnity;

public class FMODManager : MonoBehaviour
{
    // Instancia única (singleton)
    public static FMODManager instance;

    // El sistema de audio FMOD
    private FMOD.Studio.System fmodSystem;

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

    public FMOD.Studio.EventInstance CreateEventInstance(EventReference eventRef)
    {
        Debug.Log("Creando instancia");
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