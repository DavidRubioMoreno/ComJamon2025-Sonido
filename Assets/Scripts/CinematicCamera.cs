using UnityEngine;
using System.Collections;

public class CinematicCamera : MonoBehaviour
{
    public Transform arbolObjetivo; // Árbol al que se acercará la cámara
    public Camera mainCamera; // Cámara del jugador (Main Camera)

    public float duracionMovimiento = 3f; // Tiempo para moverse al árbol
    public float duracionRotacion = 3f; // Tiempo para girar y subir
    public Transform posicionInicial; // Posición inicial
    public float alturaExtra = 10f; // Altura extra después de girar
    public float tiempoAntesDeCambio = 2f; // Tiempo antes de cambiar a la Main Camera
    public float angleGiro = 30.0f;

    private Camera cinematicCamera;

    void Start()
    {
        cinematicCamera = GetComponent<Camera>();

        // Activar esta cámara y desactivar la principal al inicio
        cinematicCamera.enabled = true;
        if (mainCamera != null) mainCamera.enabled = false;

        transform.position = posicionInicial.position;

        StartCoroutine(SecuenciaCinematica());
    }

    IEnumerator SecuenciaCinematica()
    {
        UIManager.Instance.gameObject.SetActive(false);

        // ?? Primera fase: Movimiento hacia el árbol
        Vector3 inicio = transform.position;
        Vector3 destino = arbolObjetivo.position;

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
        Quaternion rotacionFinal = Quaternion.Euler(-angleGiro, transform.eulerAngles.y, transform.eulerAngles.z); // Mirar hacia arriba
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

        UIManager.Instance.gameObject.SetActive(true);
    }
}
