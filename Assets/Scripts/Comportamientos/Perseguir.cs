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

    [SerializeField]
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
    }
    private void Update()
    {
        Vector3 v = (_player.transform.position - transform.position);
        Vector3 targetVelocity =  v.normalized * _maxSpeed;

        if (v.magnitude < rObjetivo)
        {
            targetVelocity = Vector3.zero;
        }else if(v.magnitude < rRalentizado)
        {
            targetVelocity *= fRalentizado;
        }

        _currentVelocity = Vector3.MoveTowards(_currentVelocity, targetVelocity,
                (targetVelocity.magnitude > 0 ? _acceleration : _deceleration) * Time.deltaTime);
        _animator.SetFloat("velocity", _myRB.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        Vector3 v = (_player.transform.position - transform.position);
        if (v.magnitude > rObjetivo)
        {
            _myRB.MovePosition(_myRB.position + _currentVelocity * Time.fixedDeltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(_currentVelocity, Vector3.up);
            _myRB.rotation = Quaternion.Slerp(_myRB.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
        }        
    }

}