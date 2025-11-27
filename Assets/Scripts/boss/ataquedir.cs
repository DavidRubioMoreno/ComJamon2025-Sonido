using UnityEngine;

public class AtaqueDirigido : MonoBehaviour
{
    public float velocidad = 20f;
    public int damage = 30;
    public float tiempoDeVida = 10f;
    private Vector3 direccion; 

    void Start()
    {
       

        if (GameManager.Instance.Player != null)
        {
            direccion = (new Vector3(GameManager.Instance.Player.transform.position.x, GameManager.Instance.Player.transform.position.y +2 , GameManager.Instance.Player.transform.position.z ) - transform.position).normalized;
        }
        else
        {
            Debug.LogWarning("Jugador no encontrado, destruyendo ataque dirigido.");
            Destroy(gameObject);
        }

        Destroy(gameObject, tiempoDeVida);
    }

    void Update()
    {
        transform.position += direccion * velocidad * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            other.GetComponent<LifeComponent>().LoseLife(damage);
            Destroy(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
