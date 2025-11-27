using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class AttackComponent : MonoBehaviour
{
    // Start is called before the first frame update
    private float _nAttack;
    private float _nStrongAttack;

    [SerializeField]
    private GameObject fireBall;

    [SerializeField]
    private Transform nSpawn;

    private Animator _animator;

    private Rigidbody _rigidbody;

    private float elapsed;


    private void Start()
    {
        _nAttack = 0;
        _animator = GetComponent<Animator>();
        _rigidbody= GetComponent<Rigidbody>();
        elapsed = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_nAttack!=0)
        {
            _nAttack = 0;


            _animator.SetBool("Attack", true);

        }

        if (_animator.GetBool("Attack"))
        {
           elapsed+= Time.deltaTime;
            if (elapsed > 0.9)
            {
                _animator.SetBool("Attack", false);
                elapsed = 0;
            }
        }
    }

    public void Attack(InputAction.CallbackContext callback)
    {
        if (!_animator.GetBool("Attack"))
        {
            _nAttack = callback.ReadValue<float>();
        }
        
    }

    public void activeNormalAttack()
    {
        //SoundManager.Instance.PlaySound(SoundManager.Instance.magia);
        Instantiate(fireBall, nSpawn.position, Quaternion.identity);

        FMODManager.instance.PlayMageAttack();

        _animator.SetBool("Attack", false);
        
    }
  
}
