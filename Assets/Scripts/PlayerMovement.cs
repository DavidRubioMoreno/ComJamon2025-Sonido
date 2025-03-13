using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float _maxSpeed = 5f;
    public float _acceleration = 10f;
    public float _deceleration = 10f;
    public float _rotationSpeed = 10f;
    private Vector2 _inputMovement;
    private Vector2 _currentVelocity;
    private Rigidbody _myRB;

    [SerializeField]
    private Camera _mainCamera;
    private Vector3 _camForward;
    private Vector3 _camRight;
    private Vector3 _movePlayer;

    private Animator _animator;

    private void Start()
    {
        _myRB = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CamDir();

        Vector2 targetVelocity = _inputMovement * _maxSpeed;

        _currentVelocity = Vector2.MoveTowards(_currentVelocity, targetVelocity,
            (targetVelocity.magnitude > 0 ? _acceleration : _deceleration) * Time.deltaTime);

        _movePlayer = _currentVelocity.x * _camRight + _currentVelocity.y * _camForward;

        if (_movePlayer.magnitude < 0.01f)
        {
            _movePlayer = Vector3.zero;
            // Cambiar animacion
            _animator.SetTrigger("Idle");
        }
        else
        {
           // _animator.SetTrigger("Idle");
        }
    }

    private void FixedUpdate()
    {
        if (_movePlayer.magnitude > 0.01f)
        {
            _myRB.MovePosition(_myRB.position + _movePlayer * Time.fixedDeltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(_movePlayer, Vector3.up);
            _myRB.rotation = Quaternion.Slerp(_myRB.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
        }
    }

    public void Movement(InputAction.CallbackContext callback)
    {
        _inputMovement = callback.ReadValue<Vector2>();
    }

    private void CamDir()
    {
        _camForward = _mainCamera.transform.forward;
        _camRight = _mainCamera.transform.right;

        _camForward.y = 0;
        _camRight.y = 0;
        _camForward = _camForward.normalized;
        _camRight = _camRight.normalized;
    }
}
