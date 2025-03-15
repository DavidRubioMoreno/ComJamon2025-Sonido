using UnityEngine;

public class Meteorito : MonoBehaviour
{
    public float velocidadCaida = 10f;
    public int daño = 20;
    public float tiempoVida = 10f; // Tiempo antes de que se destruya automáticamente

    void Start()
    {
        Destroy(gameObject, tiempoVida); // Autodestruirse tras 10 segundos
    }

    void Update()
    {
        transform.position += Vector3.down * velocidadCaida * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            // Aquí puedes restarle vida al jugador
            Debug.Log("Jugador impactado por meteorito!");
        }

        Destroy(gameObject); // Destruir el meteorito tras el impacto
    }
}
