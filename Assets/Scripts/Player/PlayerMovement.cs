using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Configuración del Movimiento")]
    public float _maxSpeed = 5f;
    public float _acceleration = 10f;
    public float _deceleration = 10f;
    public float _rotationSpeed = 10f;
    public float _jumpForce = 10f;
    private Vector2 _inputMovement;
    private Vector2 _currentVelocity;
    private Rigidbody _myRB;
    private bool _isJumping;
    private bool _isMoving;

    private Vector3 _movePlayer;

    [Header("Camara")]
    [SerializeField]
    public Camera _mainCamera;
    private Vector3 _camForward;
    private Vector3 _camRight;

    private Animator _animator;
    private float _jump;

    [Header("Offsets rayos")]
    [SerializeField]
    private int _intLayerMask;
    [SerializeField]
    private float _rayLenght;
    [SerializeField]
    private float _offsetX;
    [SerializeField]
    private float _offsetY;

    private void Start()
    {
        _myRB = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _isJumping = false;
        _isMoving = false;
    }

    private void Update()
    {
        CamDir();

        Vector2 targetVelocity = _inputMovement * _maxSpeed;

        _currentVelocity = Vector2.MoveTowards(_currentVelocity, targetVelocity,
            (targetVelocity.magnitude > 0 ? _acceleration : _deceleration) * Time.deltaTime);

        _movePlayer = _currentVelocity.x * _camRight + _currentVelocity.y * _camForward;
        //_movePlayer = new Vector3(_currentVelocity.x,0,_currentVelocity.y);
        if (_movePlayer.magnitude < 0.1f)
        {
            _movePlayer = Vector3.zero;
            _isMoving = false;
            // Cambiar animacion
            if (!_animator.GetBool("Idle"))
            {
                _animator.SetBool("Idle", true);
                _animator.SetBool("Run", false);
            }
        }
        else 
        {
            if (!_animator.GetBool("Run"))
            {
                _animator.SetBool("Run", true);
                _animator.SetBool("Idle", false);
            }
            _isMoving = true;
        }
        
        
    }

    public bool IsMoving()
    {
        return _isMoving;
    }

    public void StopMovement()
    {
        _inputMovement = Vector2.zero;
        _movePlayer = Vector2.zero;
    }
    private void FixedUpdate()
    {
        if (_movePlayer.magnitude > 0.01f)
        {
            _myRB.MovePosition(_myRB.position + _movePlayer * Time.fixedDeltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(_movePlayer, Vector3.up);
            _myRB.rotation = Quaternion.Slerp(_myRB.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
        }
        if (_jump == 1 && IsGrounded() && !_isJumping)
        {
            _animator.SetBool("Jump", true);  
            _myRB.AddForce(0, _jumpForce, 0, ForceMode.Impulse);  
            _isJumping = true;  
        }
        else if (IsGrounded() && _isJumping)
        {
            _animator.SetBool("Jump", false);  
            _isJumping = false;  
        }
        else if (!IsGrounded() && !_isJumping)
        {
            _animator.SetBool("Jump", true);  
        }
        else if(IsGrounded()) _animator.SetBool("Jump", false);

    }
    public bool IsGrounded()
    {
        RaycastHit hitAbajo;

        Vector3 posX = new Vector3(transform.position.x + _offsetX, transform.position.y + 0.2f, transform.position.z);
        Vector3 posX2 = new Vector3(transform.position.x - _offsetX, transform.position.y + 0.2f, transform.position.z);
        Vector3 posY = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z + _offsetY);
        Vector3 posY2 = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z - _offsetY);


        Debug.DrawRay(posX, Vector3.down * _rayLenght, Color.red);  
        Debug.DrawRay(posX2, Vector3.down * _rayLenght, Color.green); 
        Debug.DrawRay(posY, Vector3.down * _rayLenght, Color.blue); 
        Debug.DrawRay(posY2, Vector3.down * _rayLenght, Color.yellow);

        if (Physics.Raycast(posX, Vector3.down, out hitAbajo, _rayLenght, 1 << _intLayerMask))
        {
            return true;
        }
        if (Physics.Raycast(posX2, Vector3.down, out hitAbajo, _rayLenght, 1 << _intLayerMask))
        {
            return true;
        }
        if (Physics.Raycast(posY, Vector3.down, out hitAbajo, _rayLenght, 1 << _intLayerMask))
        {
            return true;
        }
        if (Physics.Raycast(posY2, Vector3.down, out hitAbajo, _rayLenght, 1 << _intLayerMask))
        {
            return true;
        }

        return false;
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

    public void Movement(InputAction.CallbackContext callback)
    {
        _inputMovement = callback.ReadValue<Vector2>();
    }
    public void Jump(InputAction.CallbackContext callback) 
    {
        _jump = callback.ReadValue<float>();
    }
}
