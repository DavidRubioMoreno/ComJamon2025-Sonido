using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;

public class BlockAttack : MonoBehaviour
{
    [Header("Block Properties")]
    [SerializeField]
    private float _reloadTime;
    private LifeComponent _myLifeComponent;
    private bool _canBlock;
    private bool _wasMoving;
    private PlayerMovement _myPM;
    private Animator _animator;
    public bool isBlocking;

    // Start is called before the first frame update
    void Start()
    {
        _myLifeComponent = GetComponent<LifeComponent>();
        _myPM = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        _canBlock = true;
        _wasMoving = false;
        isBlocking = false;
    }

    private void Update()
    {
        if (_myPM.IsMoving() && !_canBlock && !_wasMoving)
        {
            StopBlocking();
        }
        /* 
         if(si es atacado){
            ToBlocked();
        }
         */
    }

    public void Block(InputAction.CallbackContext callback)
    {
        if (_canBlock && _myPM.IsGrounded())
        {
            if (_myPM.IsMoving())
            {
                _myPM.StopMovement();
                _wasMoving = true;
            }
            SoundManager.Instance.PlaySound(SoundManager.Instance.escudo);
            _animator.SetBool("Block", true);
            _canBlock = false;
            isBlocking = true;
        }
    }

    public void BlockToBlocking()
    {
        _animator.SetBool("Blocking", true);
    }

    public void WasMoving()
    {
        _wasMoving = false;
    }
    public void ToBlocked()
    {
        _animator.SetBool("Blocked", true);
    }
    public void StopBlocking()
    {
        _animator.SetBool("Block", false);
        _animator.SetBool("Blocking", false);
        _animator.SetBool("Blocked", false);
        isBlocking = false;
        AcabaAnimacion();
    }

    public void AcabaAnimacion()
    {
        StartCoroutine(WaitForReloadTime());
    }

    IEnumerator WaitForReloadTime()
    {
        yield return new WaitForSeconds(_reloadTime);
        _canBlock = true;
    }
}
