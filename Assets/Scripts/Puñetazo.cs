
using UnityEngine;

public class Puñetazo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int dam;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            other.gameObject.GetComponent<LifeComponent>().LoseLife(dam);
            Destroy(gameObject);
            Debug.Log("Puññetazo");
        }
        
    }
}
