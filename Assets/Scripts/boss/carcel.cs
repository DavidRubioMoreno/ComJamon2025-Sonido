using UnityEngine;

public class carcel : MonoBehaviour
{
    public float duracion = 25f; // Duración antes de destruir la cárcel

    void Start()
    {
        Destroy(gameObject, duracion); // Se destruye automáticamente tras la duración
    }
}
