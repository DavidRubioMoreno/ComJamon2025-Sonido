using System.Collections;
using UnityEngine;

public class MiniBossMorao : MonoBehaviour
{
    public float rangoVision = 10f;
    public float rangoAndar = 5f;
    public float velocidadAndar = 2f;
    public float velocidadCorrer = 5f;
    public float rangoCerca = 1f;

    public GameObject carcelPrefab; // Prefab de la cárcel
    public GameObject charcoPrefab; // Prefab del charco
    public GameObject misilPrefab; // Prefab del misil

    private Rigidbody rb;
    private Animator _animator;
    private LifeComponent _lifeComponent;

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
        StartCoroutine(CarcelYCharco());
        StartCoroutine(MisilesOrbitales());
        _animator = GetComponent<Animator>();
        _lifeComponent = GetComponent<LifeComponent>();
        _animationState = AnimationState.Idle;
    }

    void Update()
    {
        if (GameManager.Instance.Player == null) return;

        float distancia = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position);
        Vector3 direccion = (GameManager.Instance.Player.transform.position - transform.position).normalized;

        if (distancia <= rangoVision && distancia > rangoCerca)
        {
            if (distancia > rangoAndar)
            {
                Mover(direccion, velocidadCorrer);
                if(_animationState != AnimationState.Run)
                {
                    _animator.SetTrigger("Run");
                    _animationState = AnimationState.Run;
                }
            }
            else
            {
                Mover(direccion, velocidadAndar);
                if (_animationState != AnimationState.Walk)
                {
                    _animator.SetTrigger("Walk");
                    _animationState = AnimationState.Walk;
                }
            }
        }
        else // QUIETOOO
        {
            if (_animationState != AnimationState.Idle)
            {
                _animator.SetTrigger("Idle");
                _animationState = AnimationState.Idle;
            }
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

    IEnumerator CarcelYCharco()
    {
        while (true)
        {
            yield return new WaitForSeconds(20f);

            // Posición inicial elevada de la cárcel (encima del jugador)
            Vector3 posicionInicial = GameManager.Instance.Player.transform.position + Vector3.up * 6f;

            // Rotación de la cárcel (-90° en X)
            Quaternion rotacionCarcel = Quaternion.Euler(-90f, 0f, 0f);

            // Instanciar la cárcel en el aire con la rotación correcta
            GameObject carcel = Instantiate(carcelPrefab, posicionInicial, rotacionCarcel);
            Debug.Log("Cárcel creada arriba del jugador!");

            yield return new WaitForSeconds(1f); // Espera 1 segundo antes de generar el charco

            // Crear el charco en la posición ORIGINAL donde apareció la cárcel
            Vector3 posicionInicial2 = posicionInicial + Vector3.up * 15f;
            Instantiate(charcoPrefab, posicionInicial2, Quaternion.identity);
            SoundManager.Instance.PlaySound(SoundManager.Instance.carcel);
            
        }
    }



    IEnumerator MisilesOrbitales()
    {
        while (true)
        {
            yield return new WaitForSeconds(11f);
            Debug.Log("Misiles activados!");

            for (int i = 0; i < 3; i++)
            {
                GameObject misil = Instantiate(misilPrefab, transform.position + Vector3.up * 2, Quaternion.Euler(-90f, 0f, 0f));
                misil.GetComponent<misiles>().Configurar(transform, i * 120);
                SoundManager.Instance.PlaySound(SoundManager.Instance.bolasrota);
            }
        }
    }
}
