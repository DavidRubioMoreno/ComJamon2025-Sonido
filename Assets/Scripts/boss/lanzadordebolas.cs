using System.Collections;
using UnityEngine;

public class lanzadordebolas : MonoBehaviour
{

    [Header("PARAMETROS")]
    [SerializeField]
    float timeBetweenShots = 1.0f;

    [SerializeField]
    float speedBalls = 10f;

    [Header("RECURSOS")]
    [SerializeField]
    GameObject ball;

    void Start()
    {
        StartCoroutine(DispararBolas());
    }

    IEnumerator DispararBolas()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenShots);
            LanzarBola();
        }
    }

    void LanzarBola()
    {        // Crear la bola en la posición del lanzador
        Instantiate(ball, transform.position, Quaternion.identity);
    }
}
