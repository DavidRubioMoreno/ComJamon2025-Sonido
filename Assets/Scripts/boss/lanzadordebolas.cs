using System.Collections;
using UnityEngine;

public class lanzadordebolas : MonoBehaviour
{
    GameObject player;

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
        player = GameManager.Instance.Player;
        StartCoroutine(DispararBolas());
    }

    IEnumerator DispararBolas()
    {
        Debug.Log("Deberia Disparar");
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
