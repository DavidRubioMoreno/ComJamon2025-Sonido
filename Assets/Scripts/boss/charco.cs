using UnityEngine;
using System.Collections;

public class Charco : MonoBehaviour
{
    public float duracion = 3f; // Tiempo de vida del charco
    public int dañoPorSegundo = 5; // Daño que inflige
    public float velocidadCaida = 10f; // Velocidad de caída
    private bool activo = false; // Solo activa el daño cuando toca el suelo
    private bool cayendo = true; // Controla si sigue cayendo


   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor")) // Verifica si toca el suelo
        {
            GetComponent<Rigidbody>().isKinematic = true;
            StartCoroutine(ActivarCharco());
        }
    }
    IEnumerator ActivarCharco()
    {
        yield return new WaitForSeconds(1f); // Espera antes de activarse
        activo = true;
        yield return new WaitForSeconds(duracion); // Espera el tiempo de vida
        Destroy(gameObject); // Se destruye
    }

    private void OnTriggerStay(Collider other)
    {
        if (activo && other.GetComponent<PlayerMovement>())
        {
            Debug.Log("Jugador recibiendo daño del charco!");
            // Aplica daño aquí
        }
    }
}
