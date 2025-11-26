using FMODUnity;
using FMOD;
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

    [SerializeField]
    FMODUnity.EventReference walkingEvent;   // Seleccionado desde el inspector

    [SerializeField]
    FMODUnity.EventReference jumpingEvent;   // Seleccionado desde el inspector

    private FMOD.Studio.EventInstance walkingEventInstance;
    private FMOD.Studio.EventInstance jumpingEventInstance;

    private void Start()
    {
        _myRB = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _isJumping = false;
        _isMoving = false;


        walkingEventInstance = FMODManager.instance.CreateEventInstance(walkingEvent);
        jumpingEventInstance = FMODManager.instance.CreateEventInstance(jumpingEvent);

        RuntimeManager.AttachInstanceToGameObject(walkingEventInstance, gameObject);
        RuntimeManager.AttachInstanceToGameObject(jumpingEventInstance, gameObject);

        walkingEventInstance.start();
    }

    private void Update()
    {
        CamDir();
        //Debug.Log(_maxSpeed);
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
                _animator.SetBool("Block", false);
                _animator.SetBool("Blocking", false);
                _animator.SetBool("Blocked", false);
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

    [SerializeField]
    private float _extraGravityBase = 10f; // Gravedad base
    [SerializeField]
    private float _extraGravityScale = 0.8f; // Escala de la gravedad extra según el impulso
    [SerializeField]
    private float _currentJumpForce = 0f; // Almacena la fuerza del salto actual

    private void FixedUpdate()
    {
        if (_movePlayer.magnitude > 0.01f)
        {
            _myRB.MovePosition(_myRB.position + _movePlayer * Time.fixedDeltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(_movePlayer, Vector3.up);
            _myRB.rotation = Quaternion.Slerp(_myRB.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
        }


        bool isGrounded = IsGrounded();

        // Gestión de animaciones
        if (isGrounded && _isJumping)
        {
            _animator.SetBool("Jump", false);
            jumpingEventInstance.setParameterByName("JumpSound", 1);
            jumpingEventInstance.start();
            _isJumping = false;
        }
        else if (!isGrounded && !_isJumping)
        {
            _animator.SetBool("Jump", true);
        }
        else if (isGrounded)
        {
            _animator.SetBool("Jump", false);
        }

        if (_jump == 1 && isGrounded && !_isJumping)
        {
            _animator.SetBool("Jump", true);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, _rayLenght, 1 << _intLayerMask))
            {
                Vector3 surfaceNormal = hit.normal;
                float angle = Vector3.Angle(surfaceNormal, Vector3.up);

                float angleThreshold = 20f;
                float jumpMultiplier = Mathf.Clamp01(1f - (angle / angleThreshold));

                _currentJumpForce = _jumpForce * jumpMultiplier; // Guarda la fuerza de este salto

                Vector3 jumpDirection = Vector3.up * _currentJumpForce;
                _myRB.AddForce(jumpDirection, ForceMode.Impulse);
            }
            else
            {
                _currentJumpForce = _jumpForce; // Salto con fuerza máxima
                _myRB.AddForce(Vector3.up * _currentJumpForce, ForceMode.Impulse);
                UnityEngine.Debug.Log("Saltando");
            }

            _isJumping = true;

            jumpingEventInstance.setParameterByName("JumpSound", 0);
            jumpingEventInstance.start();
        }
        else if (!isGrounded)
        {
            // Ajusta la gravedad en función de la fuerza del salto
            float dynamicGravity = _extraGravityBase + (_currentJumpForce * _extraGravityScale);
            _myRB.AddForce(Vector3.down * dynamicGravity, ForceMode.Acceleration);
        }

        walkingEventInstance.setParameterByName("Walking", isGrounded && _isMoving ? 1 : 0);
    }
    public bool IsGrounded()
    {
        RaycastHit hitAbajo;

        Vector3 posX = new Vector3(transform.position.x + _offsetX, transform.position.y + 0.2f, transform.position.z);
        Vector3 posX2 = new Vector3(transform.position.x - _offsetX, transform.position.y + 0.2f, transform.position.z);
        Vector3 posY = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z + _offsetY);
        Vector3 posY2 = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z - _offsetY);


        //Debug.DrawRay(posX, Vector3.down * _rayLenght, Color.red);  
        //Debug.DrawRay(posX2, Vector3.down * _rayLenght, Color.green); 
        //Debug.DrawRay(posY, Vector3.down * _rayLenght, Color.blue); 
        //Debug.DrawRay(posY2, Vector3.down * _rayLenght, Color.yellow);

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

    private void OnDestroy()
    {
        FMODManager.instance.StopEvent(jumpingEventInstance);
        FMODManager.instance.StopEvent(walkingEventInstance);
    }

    private void OnCollisionEnter(Collision collision)
    {
        string surfaceTag = collision.gameObject.tag;

        switch (surfaceTag)
        {
            case "Grass":
                walkingEventInstance.setParameterByName("FloorType", 0);
                break;
            case "Rock":
                walkingEventInstance.setParameterByName("FloorType", 1);
                break;
            case "Sand":
                walkingEventInstance.setParameterByName("FloorType", 2);
                break;
            case "Wood":
                walkingEventInstance.setParameterByName("FloorType", 3);
                break;
            default:
                walkingEventInstance.setParameterByName("FloorType", 0);
                break;
        }
    }
}
