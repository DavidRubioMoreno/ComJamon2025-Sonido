using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cansado : MonoBehaviour
{
    public float velocidadCaida = 10f;
    public float tiempoVida = 10f; // Tiempo antes de que se destruya automáticamente
    public int damage = 5;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            collision.gameObject.GetComponent<LifeComponent>().LoseLife(damage);
            Destroy(gameObject);
        }

    }
}
