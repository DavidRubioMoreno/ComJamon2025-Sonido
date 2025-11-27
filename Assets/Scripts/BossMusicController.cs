using System.Data.SqlTypes;
using UnityEngine;

public class BossMusicController : MonoBehaviour
{
    [SerializeField]
    FMODUnity.EventReference mainThemeEvent;

    private FMOD.Studio.EventInstance instance;


    float elapsed = 0;

    float timeToSpawnBosses = 5f;

    bool ready = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = FMODManager.instance.CreateEventInstance(mainThemeEvent);

        instance.setVolume(0.8f);

        instance.setParameterByName("ActiveMainTheme", 0);
        instance.setParameterByName("GoToOutro", 0);

        instance.start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            instance.setParameterByName("ActiveMainTheme", 1);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            instance.setParameterByName("GoToOutro", 1);
        }

        if (GameManager.Instance.Player && !ready)
        {
            elapsed += Time.deltaTime;
            if(elapsed > timeToSpawnBosses)
            {
                ready = true;
            }
        }



        if(WaveManager.Instance.EnemiesAlive == 3 && ready)
        {
            //UnityEngine.Debug.Log("Enemigos" + WaveManager.Instance.EnemiesAlive);
            instance.setParameterByName("ActiveMainTheme", 1);
        }

        if (WaveManager.Instance.EnemiesAlive == 0 && ready)
        {
           // UnityEngine.Debug.Log("Enemigos" + WaveManager.Instance.EnemiesAlive);
            instance.setParameterByName("GoToOutro", 1);
        }
    }

    private void OnDestroy()
    {
        FMODManager.instance.StopEvent(instance);
    }
}
