using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Esqueleto : MonoBehaviour
{
    public float _maxSpeed = 5f;
    public float _acceleration = 10f;
    public float _deceleration = 10f;
    public float _rotationSpeed = 10f;
    public float rObjetivo;
    public float rRalentizado;
    public float fRalentizado;
    public Vector3 _espadaOffset;
    private Vector3 _currentVelocity;
    private Rigidbody _myRB;

    private Animator _animator;

    private GameObject _player;

    [SerializeField]
    private GameObject _espada;

    private GameObject _espadaInstance;

    private bool _onAttack = false;
    /// <summary>
    /// Obtiene la dirección
    /// </summary>
    /// <returns></returns>
    /// 

    private void Start()
    {
        _myRB = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _player = GameManager.Instance.Player;
        _espadaOffset = new Vector3();
        _espadaOffset.x = -0.64f;
        _espadaOffset.y = 1.5f;
        _espadaOffset.z = -0.4f;
    }
    public void OnAttack()
    {
        _espadaInstance = Instantiate(_espada, transform.position + _espadaOffset, Quaternion.identity);
    }

    public void OnAttackExit()
    {
        _espadaInstance.SetActive(false);
    }

    private void Update()
    {
        Vector3 v = (_player.transform.position - transform.position);
        Vector3 targetVelocity =  v.normalized * _maxSpeed;

        if (v.magnitude < rObjetivo)
        {
            targetVelocity = Vector3.zero;
            _onAttack = true;
            _animator.SetBool("attack", _onAttack);
        }
        else if(v.magnitude < rRalentizado)
        {
            targetVelocity *= fRalentizado;
        }

        _currentVelocity = Vector3.MoveTowards(_currentVelocity, targetVelocity,
                (targetVelocity.magnitude > 0 ? _acceleration : _deceleration) * Time.deltaTime);
        _animator.SetFloat("velocity", _currentVelocity.magnitude);
    }

    private void FixedUpdate()
    {
        
        Vector3 v = (_player.transform.position - transform.position);
        if (v.magnitude > rObjetivo)
        {
            Vector3 newPosition = _myRB.position + _currentVelocity * Time.fixedDeltaTime;
            _myRB.MovePosition(newPosition);

            if (_currentVelocity.magnitude > 0.1f) // Para evitar rotaciones raras
            {
                Quaternion targetRotation = Quaternion.LookRotation(_currentVelocity, Vector3.up);
                _myRB.rotation = Quaternion.Slerp(_myRB.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
            }
        }        
    }

}