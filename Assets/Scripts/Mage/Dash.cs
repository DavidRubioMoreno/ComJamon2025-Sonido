using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float force;

    private float _dash;

    private int nDash;

    private float elapsedTime;

    [SerializeField]
    private float reloadTime;

    private Rigidbody _rb;
    private Animator _an;
    private PlayerMovement _pm;
    void Start()
    {
        _rb=GetComponent<Rigidbody>();
        _an=GetComponent<Animator>();
        _pm=GetComponent<PlayerMovement>();
        nDash = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_pm.IsGrounded())
        {
            nDash = 0;
        }

        elapsedTime += Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if (_dash > 0)
        {
            _dash = 0;
            _rb.velocity=Vector3.zero;
            _rb.AddForce(transform.forward.normalized *  force,ForceMode.Impulse);
            _an.SetBool("Dash", true);
            nDash += 1;
        }
        
    }
    public void EndDash()
    {
        _an.SetBool("Dash", false);
    }
    public void Dashito(InputAction.CallbackContext callback)
    {
        Debug.Log("Dash");
        if (!_an.GetBool("Dash")&&nDash<1&&elapsedTime>reloadTime)
        {
            _dash = callback.ReadValue<float>();
            elapsedTime = 0;
        }
        
    }
}
