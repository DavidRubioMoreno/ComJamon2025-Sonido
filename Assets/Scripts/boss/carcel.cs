using UnityEngine;

public class carcel : MonoBehaviour
{
    public float duracion = 25f; 

    void Start()
    {
        Destroy(gameObject, duracion); 
    }
}
