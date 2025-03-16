using System.Collections;
using UnityEngine;

public class MiniBossRojo : MonoBehaviour
{
    private Transform jugador;
    public float rangoVision = 10f;
    public float rangoCorrer = 5f;
    public float rangoAtaque = 1f;
    public float velocidadAndar = 2f;
    public float velocidadCorrer = 5f;

    public GameObject meteoritoPrefab; // Prefab del meteorito
    public GameObject ataqueDirigidoPrefab; // Prefab del ataque dirigido

    private bool atacando = false;
    private Rigidbody rb;

    void Start()
    {
        //jugador = GameManager.Instance.Player?.transform;
        rb = GetComponent<Rigidbody>(); // Obtener el Rigidbody si existe

        StartCoroutine(LluviaDeMeteoritos());
        StartCoroutine(AtaqueDirigido());
    }

    void Update()
    {
        if (GameManager.Instance.Player == null) return;

        float distancia = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position);
        Debug.Log(distancia);

        if (distancia <= rangoVision) // Si el jugador está en el área de visión
        {
            Vector3 direccion = (GameManager.Instance.Player.transform.position - transform.position).normalized;

            if (distancia > rangoCorrer)
            {
                Mover(direccion, velocidadAndar); // Andar
            }
            else
            {
                Mover(direccion, velocidadCorrer); // Correr
            }

            if (distancia <= rangoAtaque && !atacando)
            {
                StartCoroutine(AtaqueBasico());
            }
        }
    }

    void Mover(Vector3 direccion, float velocidad)
    {
        if (rb != null)
        {
            rb.MovePosition(transform.position + direccion * velocidad * Time.deltaTime);
        }
        else
        {
            transform.Translate(direccion * velocidad * Time.deltaTime, Space.World);
        }
    }

    IEnumerator AtaqueBasico()
    {
        atacando = true;
        Debug.Log("MiniBoss ataca!");

        yield return new WaitForSeconds(1.5f);

        atacando = false;
    }

    IEnumerator LluviaDeMeteoritos()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            Debug.Log("Lluvia de meteoritos!");

            for (int i = 0; i < 15; i++) // Número de meteoritos por lluvia
            {
                Vector3 posicionAleatoria = GenerarPosicionAleatoria();
                Instantiate(meteoritoPrefab, posicionAleatoria, Quaternion.identity);
            }
        }
    }

    Vector3 GenerarPosicionAleatoria()
    {
        Vector3 randomPos = Random.insideUnitSphere * (rangoVision - 20);
        randomPos.y = 8;
        randomPos += transform.position;

        return randomPos;
    }

    IEnumerator AtaqueDirigido()
    {
        while (true)
        {
            yield return new WaitForSeconds(20f);
            Debug.Log("Ataque dirigido al jugador!");

            if (GameManager.Instance.Player != null)
            {
                Instantiate(ataqueDirigidoPrefab, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), Quaternion.identity);
            }
        }
    }
}
