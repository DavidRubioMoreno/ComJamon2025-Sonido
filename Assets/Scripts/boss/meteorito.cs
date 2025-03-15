using UnityEngine;

public class Meteorito : MonoBehaviour
{
    public float velocidadCaida = 10f;
    public float tiempoVida = 10f; // Tiempo antes de que se destruya automáticamente
    public int damage = 5;
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
            other.GetComponent<LifeComponent>().LoseLife(damage);
        }

        Destroy(gameObject); // Destruir el meteorito tras el impacto
    }
}
