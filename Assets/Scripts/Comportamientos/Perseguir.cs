using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Perseguir : MonoBehaviour
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
    private void OnArrive()
    {
        if(!_animator.GetBool("attack"))_animator.SetBool("attack", true);        
    }

    private void Update()
    {
        Vector3 v = (_player.transform.position - transform.position);
        Vector3 targetVelocity =  v.normalized * _maxSpeed;

        if (v.magnitude < rObjetivo)
        {
            targetVelocity = Vector3.zero;
            OnArrive();          
        }else if(v.magnitude < rRalentizado)
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