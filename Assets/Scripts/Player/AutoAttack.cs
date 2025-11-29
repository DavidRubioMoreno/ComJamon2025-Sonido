using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AutoAttack : MonoBehaviour
{
    [Header("Collider")]
    [SerializeField]
    private GameObject _collider;

    [Header("Attack properties")]
    [SerializeField]
    private int _damage;
    [SerializeField]
    private float _reloadTime;

    private Animator _animator;
    private int _random;
    private bool _canAttack;
    public bool isAttacking;
    private PlayerMovement _myPM;

    [SerializeField]
    FMODUnity.EventReference attackEvent;   // Seleccionado desde el inspector

    private FMOD.Studio.EventInstance attackEventInstance;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _canAttack = true;
        _myPM = GetComponent<PlayerMovement>();
        isAttacking = false;

        attackEventInstance = FMODManager.instance.CreateEventInstance(attackEvent);

        RuntimeManager.AttachInstanceToGameObject(attackEventInstance, gameObject);
    }

    private void OnDestroy()
    {
        FMODManager.instance.StopEvent(attackEventInstance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<LifeComponent>() &&  _collider.gameObject.activeSelf && !GameManager.Instance.Pause)
        {
            other.gameObject.GetComponent<LifeComponent>().LoseLife(_damage);
        }
    }

    public void Attack(InputAction.CallbackContext callback)
    {
        if (_canAttack && _myPM.IsGrounded())
        {
            _random = Random.Range(0, 2);

            if (_random == 0)
                _animator.SetBool("Attack", true);
            else
                _animator.SetBool("Attack2", true);
            _canAttack = false;
            if (_random == 0)
            {
                attackEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                attackEventInstance.setParameterByName("AttackType", 0);
                attackEventInstance.setParameterByName("SwingPitch", 10);
                attackEventInstance.start();
            }
            else
            {
                attackEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                attackEventInstance.setParameterByName("AttackType", 0);
                attackEventInstance.setParameterByName("SwingPitch", -10);
                attackEventInstance.start();
            }
            isAttacking = true;
        }
    }

    public void Activar()
    {
        _collider.gameObject.SetActive(true);
    }
    public void Desactivar()
    {
        _collider.gameObject.SetActive(false);
    }

    public void AcabaAnimacion()
    {
        _animator.SetBool("Attack", false);
        _animator.SetBool("Attack2", false);
        isAttacking = false;
        StartCoroutine(WaitForReloadTime());
    }
    IEnumerator WaitForReloadTime()
    {
        yield return new WaitForSeconds(_reloadTime);
        _canAttack = true;
    }
}
