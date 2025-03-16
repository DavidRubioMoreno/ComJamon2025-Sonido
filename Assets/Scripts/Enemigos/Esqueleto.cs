using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Esqueleto: MonoBehaviour
{
    public float _maxSpeed = 5f;
    public float _acceleration = 10f;
    public float _deceleration = 10f;
    public float _rotationSpeed = 10f;
    public float rObjetivo;
    public float rRalentizado;
    public float fRalentizado;
    private Vector3 _currentVelocity;
    private Rigidbody _myRB;

    private Animator _animator;

    private GameObject _player;

    [SerializeField]
    private GameObject _espada;

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
    }
    public void OnAttack()
    {

        if (_espada != null)
        {

            _espada.SetActive(true);
            SoundManager.Instance.PlaySound(SoundManager.Instance.espadaesqueleto, 0.2f);
        }
    }

    public void OnAttackExit()
    {
        _espada.SetActive(false);
        _onAttack = false;
        _animator.SetBool("attack", false);
    }

    private void Update()
    {
        _player = GameManager.Instance.Player;
        if (_player == null) return;
        Vector3 v = (_player.transform.position - transform.position);
        Vector3 targetVelocity =  v.normalized * _maxSpeed;

        if (v.magnitude < rObjetivo)
        {
            targetVelocity = Vector3.zero;
            _onAttack = true;
            _animator.SetBool("attack", true);
            OnAttack();
        }
        else if(v.magnitude < rRalentizado)
        {
            targetVelocity *= fRalentizado;
        }
        

        _currentVelocity = Vector3.MoveTowards(_currentVelocity, targetVelocity,(targetVelocity.magnitude > 0 ? _acceleration : _deceleration) * Time.deltaTime);

        if (_currentVelocity == Vector3.zero)
        {
            _animator.SetBool("vel", false);
            _animator.SetBool("idle", true);
        }
        if (!_animator.GetBool("attack"))
        {
            _animator.SetBool("vel", true);
            _animator.SetBool("idle", false);
        }
    }

    private void FixedUpdate()
    {
        _player = GameManager.Instance.Player;
        if (_player == null) return;
        Vector3 v = (_player.transform.position - transform.position);
        Debug.Log("EEEEEE");
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