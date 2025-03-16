using UnityEngine;

public class misiles : MonoBehaviour
{
    public float velocidadRotacion = 100f; // Velocidad de giro
    public float radio = 2f; // Distancia al MiniBoss
    public int daño = 10;
    public int tiempoVida = 10;
    private Transform miniboss;
    private float angulo = 0f;

    public void Configurar(Transform boss, float offsetAngulo)
    {
        miniboss = boss;
        angulo = offsetAngulo; // Ángulo inicial de cada misil
    }
    void Start()
    {
        Destroy(gameObject, tiempoVida); // Autodestruirse tras 10 segundos
    }
    void Update()
    {
        if (miniboss == null) return;

        angulo += velocidadRotacion * Time.deltaTime; // Rotar alrededor del boss
        float rad = Mathf.Deg2Rad * angulo;
        Vector3 posicionOrbita = miniboss.position + new Vector3(Mathf.Cos(rad) * radio, 1, Mathf.Sin(rad) * radio);
        transform.position = posicionOrbita;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            Debug.Log("Jugador impactado por misil orbital!");
            // Aquí puedes aplicar daño al jugador
        }
    }
}
