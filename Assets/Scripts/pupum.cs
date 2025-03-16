using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pupum : MonoBehaviour
{
    public float velocidadCaida = 10f;
    public float tiempoVida = 10f; // Tiempo antes de que se destruya automáticamente
    public int damage = 5;
    void Start()
    {
        Destroy(gameObject, tiempoVida); // Autodestruirse tras 10 segundos
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            collision.gameObject.GetComponent<LifeComponent>().LoseLife(damage);
            Destroy(gameObject);
        }

    }
}
