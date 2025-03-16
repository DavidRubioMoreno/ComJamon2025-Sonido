using UnityEngine;

public class misiles : MonoBehaviour
{
    public float velocidadRotacion = 100f; 
    public float radio = 2f; 
    public int daño = 10;
    public int tiempoVida = 10;
    private Transform miniboss;
    private float angulo = 0f;

    public void Configurar(Transform boss, float offsetAngulo)
    {
        miniboss = boss;
        angulo = offsetAngulo; 
    }
    void Start()
    {
        Destroy(gameObject, tiempoVida); 
    }
    void Update()
    {
        if (miniboss == null) return;

        angulo += velocidadRotacion * Time.deltaTime; 
        float rad = Mathf.Deg2Rad * angulo;
        Vector3 posicionOrbita = miniboss.position + new Vector3(Mathf.Cos(rad) * radio, 1, Mathf.Sin(rad) * radio);
        transform.position = posicionOrbita;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            other.gameObject.GetComponent<LifeComponent>().LoseLife(1);
        }
    }
}
