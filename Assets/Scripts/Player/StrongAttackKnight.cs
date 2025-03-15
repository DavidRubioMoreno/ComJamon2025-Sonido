using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StrongAttackKnight : MonoBehaviour
{
    [Header("Collider")]
    [SerializeField]
    private GameObject _collider;

    [Header("Attack properties")]
    [SerializeField]
    private int _damage;
    [SerializeField]
    private float _reloadTime;
    [SerializeField]
    private float _damageTime;
    [SerializeField]
    private float _time;

    private Animator _animator;
    private bool _canAttack;
    private bool _coroutineRunning;
    public bool isAttacking;
    private PlayerMovement _myPM;

    private HashSet<GameObject> _enemiesInsideTrigger = new HashSet<GameObject>();
    private HashSet<GameObject> _damagedEnemies = new HashSet<GameObject>();

    void Start()
    {
        _animator = GetComponent<Animator>();
        _canAttack = true;
        _myPM = GetComponent<PlayerMovement>();
        isAttacking = false;
        _coroutineRunning = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<LifeComponent>())
        {
            _enemiesInsideTrigger.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_enemiesInsideTrigger.Contains(other.gameObject))
        {
            _enemiesInsideTrigger.Remove(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_collider.gameObject.activeSelf) return;

        if (_enemiesInsideTrigger.Contains(other.gameObject) && !_damagedEnemies.Contains(other.gameObject))
        {
            other.gameObject.GetComponent<LifeComponent>().LoseLife(_damage);
            _damagedEnemies.Add(other.gameObject);
        }

        if (!_coroutineRunning)
        {
            StartCoroutine(WaitForDamageTime());
        }
    }

    public void StrongAttack(InputAction.CallbackContext callback)
    {
        if (_canAttack && _myPM.IsGrounded())
        {
            _animator.SetBool("StrongAttack", true);
            _canAttack = false;
            isAttacking = true;
            _collider.SetActive(true);
            StartCoroutine(WaitForDeactivateTime());
        }
    }

    public void AcabaAnimacion()
    {
        _animator.SetBool("StrongAttack", false);
        isAttacking = false;
        _collider.SetActive(false);
        StartCoroutine(WaitForReloadTime());
    }

    IEnumerator WaitForReloadTime()
    {
        yield return new WaitForSeconds(_reloadTime);
        _canAttack = true;
    }

    IEnumerator WaitForDeactivateTime()
    {
        yield return new WaitForSeconds(_time);
        AcabaAnimacion();
    }

    IEnumerator WaitForDamageTime()
    {
        _coroutineRunning = true;
        yield return new WaitForSeconds(_damageTime);
        _damagedEnemies.Clear(); // Permite volver a hacer daño después del intervalo
        _coroutineRunning = false;
    }
}
