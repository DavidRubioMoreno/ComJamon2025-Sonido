
using FMODUnity;
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

    [SerializeField]
    private GameObject d;

    [SerializeField]
    FMODUnity.EventReference dashEvent;

    private FMOD.Studio.EventInstance dashEventInstance;

    private Rigidbody _rb;
    private Animator _an;
    private PlayerMovement _pm;
    void Start()
    {
        _rb=GetComponent<Rigidbody>();
        _an=GetComponent<Animator>();
        _pm=GetComponent<PlayerMovement>();
        nDash = 0;

        dashEventInstance = FMODManager.instance.CreateEventInstance(dashEvent);

        RuntimeManager.AttachInstanceToGameObject(dashEventInstance, gameObject);
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
            _rb.linearVelocity=Vector3.zero;
            GameObject ds=Instantiate(d, transform.position, Quaternion.identity);
            ds.transform.parent=transform;
            _rb.AddForce(transform.forward.normalized *  force,ForceMode.Impulse);
            _an.SetBool("Dash", true);
            nDash += 1;
            dashEventInstance.start();
            //SoundManager.Instance.PlaySound(SoundManager.Instance.dash);
        }
        
    }
    public void EndDash()
    {
        _an.SetBool("Dash", false);
    }
    public void Dashito(InputAction.CallbackContext callback)
    {

        if (!_an.GetBool("Dash")&&nDash<1&&elapsedTime>reloadTime)
        {

            _dash = callback.ReadValue<float>();
            elapsedTime = 0;
        }
        
    }
}
