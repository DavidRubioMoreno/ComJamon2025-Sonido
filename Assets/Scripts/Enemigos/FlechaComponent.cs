
using UnityEngine;


public class FlechaComponent : MonoBehaviour
{
    private float _speed = 10.0f;
    [SerializeField]
    private int damage = 1;
    private Vector3 _currentVelocity;
    public Vector3 _target;
    private Rigidbody _myRB;

    // Start is called before the first frame update
    void Start()
    {
        _myRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = (_target - transform.position);
        Vector3 targetVelocity = v.normalized * _speed;

        _currentVelocity = Vector3.MoveTowards(_currentVelocity, targetVelocity,
                _speed* Time.deltaTime);
    }

    private void FixedUpdate()
    {
        _myRB.MovePosition(_myRB.position + _currentVelocity * Time.fixedDeltaTime * _speed);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<PlayerMovement>())
        {
            Debug.Log("Hit");
            collider.GetComponent<LifeComponent>().LoseLife(damage);
        }
            
        Destroy(gameObject);
    }
}
