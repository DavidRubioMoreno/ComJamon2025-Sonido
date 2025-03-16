using UnityEngine;

public class Meteorito : MonoBehaviour
{
    public float velocidadCaida = 10f;
    public float tiempoVida = 10f; 
    public int damage = 5;
    void Start()
    {
        Destroy(gameObject, tiempoVida); 
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
        Destroy(gameObject); 

    }
}
