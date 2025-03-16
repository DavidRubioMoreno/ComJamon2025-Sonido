
using UnityEngine;


public class ChangeSkybox : MonoBehaviour
{
    public Material newSkybox; // Arrastra aquí el nuevo Skybox desde el Inspector

    void Start()
    {
        RenderSettings.skybox = newSkybox;
        DynamicGI.UpdateEnvironment(); // Actualiza la iluminación global si es necesario
    }
}
