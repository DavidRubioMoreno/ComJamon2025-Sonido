using UnityEngine;

public class ReboteToxico : MonoBehaviour
{
    public float velocidad = 10f;
    public int damage = 20;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Dispara en dirección aleatoria con fuerza inicial
        Vector3 direccion = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        rb.velocity = direccion * velocidad;

        Destroy(gameObject, 10f); // Se destruye después de 10 segundos si no impacta
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            Debug.Log("Jugador golpeado por el veneno!");
            collision.gameObject.GetComponent<LifeComponent>().LoseLife(damage);
            //Destroy(gameObject);
        }
    }
}
