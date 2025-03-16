using System.Collections;
using UnityEngine;

public class MiniBossVerde : MonoBehaviour
{
    private Transform jugador;
    public float rangoVision = 10f;
    public float rangoCorrer = 5f;
    public float rangoAtaque = 1f;
    public float velocidadAndar = 2f;
    public float velocidadCorrer = 5f;

    public GameObject zonaPegajosaPrefab;
    public GameObject reboteToxicoPrefab;

    private bool atacando = false;
    private Rigidbody rb;
    private Animator _animator;

    private enum AnimationState
    {
        Walk,
        Run,
        Attack,
        Hit,
        Death,
        Spell,
        Idle
    }
    AnimationState _animationState;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(LanzarZonaPegajosa());
        StartCoroutine(DispararReboteToxico());
        _animator = GetComponent<Animator>();
        _animationState = AnimationState.Idle;
    }

    void Update()
    {
        if (GameManager.Instance.Player == null) return;

        float distancia = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position);
        Vector3 direccion = (GameManager.Instance.Player.transform.position - transform.position).normalized;

        if (distancia <= rangoVision)
        {
            if (distancia > rangoCorrer)
            {
                Mover(direccion, velocidadAndar);
                CambiarAnimacion(AnimationState.Walk, "Walk");
            }
            else if (distancia <= rangoCorrer && distancia > rangoAtaque)
            {
                Mover(direccion, velocidadCorrer);
                CambiarAnimacion(AnimationState.Run, "Run");
            }
        }
        else
        {
            CambiarAnimacion(AnimationState.Idle, "Idle");
        }
    }

    void Mover(Vector3 direccion, float velocidad)
    {
        if (rb != null)
            rb.MovePosition(transform.position + direccion * velocidad * Time.deltaTime);
        else
            transform.Translate(direccion * velocidad * Time.deltaTime, Space.World);

        transform.LookAt(GameManager.Instance.Player.transform.position);
    }

    IEnumerator LanzarZonaPegajosa()
    {
        while (true)
        {
            yield return new WaitForSeconds(15f);
            Debug.Log("MiniBoss Verde lanza zona pegajosa!");
            Vector3 posicion = GameManager.Instance.Player.transform.position + Vector3.up * 2;
            Instantiate(zonaPegajosaPrefab, posicion, Quaternion.identity);
        }
    }

    IEnumerator DispararReboteToxico()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            Debug.Log("MiniBoss Verde lanza rebote tóxico!");
            Vector3 spawnPos = transform.position + transform.forward * 2;
            GameObject bola = Instantiate(reboteToxicoPrefab, spawnPos, Quaternion.identity);
            bola.GetComponent<Rigidbody>().velocity = (GameManager.Instance.Player.transform.position - transform.position).normalized * 10f;
        }
    }

    void CambiarAnimacion(AnimationState nuevoEstado, string trigger)
    {
        if (_animationState != nuevoEstado)
        {
            _animator.SetTrigger(trigger);
            _animationState = nuevoEstado;
        }
    }
}
