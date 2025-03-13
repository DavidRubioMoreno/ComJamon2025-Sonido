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

    void Start()
    {
        _nAttack = 0;
        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (_nAttack!=0)
        {
            
            Instantiate(fireBall,nSpawn.position,Quaternion.identity);
            _nAttack =0;
            _animator.SetBool("Attack", true);

        }
        else
        {
            _animator.SetBool("Attack", false);

        }
    }

    public void Attack(InputAction.CallbackContext callback)
    {
        _nAttack = callback.ReadValue<float>();
    }
}
