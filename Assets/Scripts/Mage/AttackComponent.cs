using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class AttackComponent : MonoBehaviour
{
    // Start is called before the first frame update
    private float _nAttack;

    [SerializeField]
    private GameObject fireBall;

    [SerializeField]
    private Transform nSpawn;

    private Animator _animator;

    private void Start()
    {
        _nAttack = 0;
        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    private void Update()
    {
        if (_nAttack!=0)
        {
            _nAttack = 0;


            _animator.SetBool("Attack", true);

        }

    }

    public void Attack(InputAction.CallbackContext callback)
    {
        _nAttack = callback.ReadValue<float>();
    }
    public void activeNormalAttack()
    {
        Instantiate(fireBall, nSpawn.position, Quaternion.identity);
        _animator.SetBool("Attack", false);
        
    }
}
