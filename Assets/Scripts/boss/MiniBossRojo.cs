using System.Collections;
using UnityEngine;

public class MiniBossRojo : MonoBehaviour
{
    private Transform jugador;

    [SerializeField]
    private Transform mano;

    [SerializeField]
    private GameObject puno;

    public float rangoVision = 10f;
    public float rangoCorrer = 5f;
    public float rangoAtaque = 1f;
    public float velocidadAndar = 2f;
    public float velocidadCorrer = 5f;

    public GameObject meteoritoPrefab; 
    public GameObject ataqueDirigidoPrefab; 

    private bool atacando = false;
    private bool spell = false;
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
        //jugador = GameManager.Instance.Player?.transform;
        rb = GetComponent<Rigidbody>(); 

        StartCoroutine(LluviaDeMeteoritos());
        StartCoroutine(AtaqueDirigido());
        _animator = GetComponent<Animator>();
        _animationState = AnimationState.Idle;
    }

    void Update()
    {
        if (GameManager.Instance.Player == null) return;

        float distancia = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position);
        //Debug.Log(distancia);

        if (distancia <= rangoVision) 
        {
            Vector3 direccion = (GameManager.Instance.Player.transform.position - transform.position).normalized;

            if (distancia > rangoCorrer)
            {
                Mover(direccion, velocidadAndar); // Andar
                if (_animationState != AnimationState.Walk)
                {
                    _animator.SetTrigger("Walk");
                    _animationState = AnimationState.Walk;
                }
                
            }
            else if(distancia <= rangoCorrer &&  distancia > rangoAtaque)
            {
                Mover(direccion, velocidadCorrer); // Correr
                if (_animationState != AnimationState.Run)
                {
                    _animator.SetTrigger("Run");
                    _animationState = AnimationState.Run;
                }
            }
            else
            {
                if (!atacando)
                {
                    StartCoroutine(AtaqueBasico());
                    rb.linearVelocity = Vector3.zero;
                    if (_animationState != AnimationState.Attack)
                    {
                        _animator.SetTrigger("Attack");
                        _animationState = AnimationState.Attack;
                    }
                }
                    
            }
        }
        else
        {
            if (_animationState != AnimationState.Idle)
            {
                _animator.SetTrigger("Idle");
                _animationState = AnimationState.Idle;
            }
        }
        //if (spell)
        //{
        //    if (_animationState != AnimationState.Spell)
        //    {
        //        _animator.SetTrigger("Cast Spell");
        //        _animationState = AnimationState.Spell;
        //    }
        //}

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
        transform.LookAt(GameManager.Instance.Player.transform.position);
    }

    IEnumerator AtaqueBasico()
    {
        atacando = true;

        Instantiate(puno,mano.position, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);

        atacando = false;
    }

    IEnumerator LluviaDeMeteoritos()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            spell = true;

            //SoundManager.Instance.PlaySound(SoundManager.Instance.bosrojomet);
            for (int i = 0; i < 15; i++) 
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
            if (_animationState != AnimationState.Spell)
            {
                _animator.SetTrigger("Cast Spell");
                _animationState = AnimationState.Spell;
            }
            Debug.Log("Ataque dirigido al jugador!");

            if (GameManager.Instance.Player != null)
            {
                Instantiate(ataqueDirigidoPrefab, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), Quaternion.identity);
                //SoundManager.Instance.PlaySound(SoundManager.Instance.bossrojobolon);
            }
        }
    }

    public void ToIdle()
    {
        if (_animationState != AnimationState.Idle)
        {
            _animator.SetTrigger("Idle");
            _animationState = AnimationState.Idle;
        }
    }

    public void AntiSpell()
    {
        
    }
}
