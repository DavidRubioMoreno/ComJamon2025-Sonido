using UnityEngine;
using System.Collections;

public class CinematicCamera : MonoBehaviour
{
    public Transform arbolObjetivo; // Árbol al que se acercará la cámara
    public Camera mainCamera; // Cámara del jugador (Main Camera)

    public float duracionMovimiento = 5f; // Tiempo para moverse al árbol
    public float duracionRotacion = 3f; // Tiempo para girar y subir
    public Vector3 posicionInicial = new Vector3(0, 50, -100); // Posición inicial
    public float alturaExtra = 10f; // Altura extra después de girar
    public float tiempoAntesDeCambio = 2f; // Tiempo antes de cambiar a la Main Camera

    private Camera cinematicCamera;

    void Start()
    {
        cinematicCamera = GetComponent<Camera>();

        // Activar esta cámara y desactivar la principal al inicio
        cinematicCamera.enabled = true;
        if (mainCamera != null) mainCamera.enabled = false;

        transform.position = posicionInicial;

        StartCoroutine(SecuenciaCinematica());
    }

    IEnumerator SecuenciaCinematica()
    {
        // ?? Primera fase: Movimiento hacia el árbol
        Vector3 inicio = transform.position;
        Vector3 destino = arbolObjetivo.position + new Vector3(0, 5, -10);

        float tiempo = 0;
        while (tiempo < duracionMovimiento)
        {
            tiempo += Time.deltaTime;
            transform.position = Vector3.Lerp(inicio, destino, tiempo / duracionMovimiento);
            transform.LookAt(arbolObjetivo); // Mirar al árbol durante el acercamiento
            yield return null;
        }

        //yield return new WaitForSeconds(0f); // Pausa antes de rotar

        // ?? Segunda fase: Girar 90° hacia arriba y subir
        Quaternion rotacionInicial = transform.rotation;
        Quaternion rotacionFinal = Quaternion.Euler(-90, transform.eulerAngles.y, transform.eulerAngles.z); // Mirar hacia arriba
        Vector3 posicionFinal = transform.position + new Vector3(0, alturaExtra, 0);

        tiempo = 0;
        while (tiempo < duracionRotacion)
        {
            tiempo += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(rotacionInicial, rotacionFinal, tiempo / duracionRotacion);
            transform.position = Vector3.Lerp(destino, posicionFinal, tiempo / duracionRotacion);
            yield return null;
        }

        yield return new WaitForSeconds(tiempoAntesDeCambio);

        // ?? Cambiar a la cámara del jugador
        GameManager.Instance.cinematica = true;
        cinematicCamera.enabled = false;
        if (mainCamera != null) mainCamera.enabled = true;
    }
}
