using UnityEngine;
using System.Collections;

public class MusicMainIsland : MonoBehaviour
{

    [SerializeField]
    FMODUnity.EventReference islandMusic;
    public FMOD.Studio.EventInstance islandMusicInstance;

    private void Start()
    {
        if (!FMODManager.instance) return;

        FMODManager.instance.ExitSelector();

        if(islandMusicInstance.isValid())
        {
            islandMusicInstance.release();
        }

        StartCoroutine(StartIslandMusic());
    }

    IEnumerator StartIslandMusic()
    {
        FMOD.Studio.PLAYBACK_STATE state;

        do
        {
            FMODManager.instance.menuMusicInstance.getPlaybackState(out state);
            yield return null;
        }
        while (state != FMOD.Studio.PLAYBACK_STATE.STOPPED);

        islandMusicInstance = FMODUnity.RuntimeManager.CreateInstance(islandMusic);
        islandMusicInstance.start();
    }

    private void OnDestroy()
    {
        StopIslandMusic();
    }

    void StopIslandMusic()
    {
        if (islandMusicInstance.isValid())
        {
            islandMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        if(FMODManager.instance.menuMusicInstance.isValid())
        {
            FMODManager.instance.menuMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            FMODManager.instance.menuMusicInstance.release();
        }
    }
}


