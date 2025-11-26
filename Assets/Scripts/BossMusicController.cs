using UnityEngine;

public class BossMusicController : MonoBehaviour
{
    [SerializeField]
    FMODUnity.EventReference mainThemeEvent;

    private FMOD.Studio.EventInstance instance;

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
    }

    private void OnDestroy()
    {
        FMODManager.instance.StopEvent(instance);
    }
}
