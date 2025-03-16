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
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _canAttack = true;
        _myPM = GetComponent<PlayerMovement>();
        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<LifeComponent>() &&  _collider.gameObject.activeSelf)
        {
            other.gameObject.GetComponent<LifeComponent>().LoseLife(_damage);
            Debug.Log("DAÑO");
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
                SoundManager.Instance.PlaySound(SoundManager.Instance.espada);
            }
            else
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.espada2,0.2f);
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
