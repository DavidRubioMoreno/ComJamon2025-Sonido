using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Dest : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float velocity;

    [SerializeField]
    private float rotateVelocity;

    private Transform obj;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        obj = getFirst();
        rb.AddForce(new Vector3(0, 1, 0) * 100, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        //rb.MovePosition(*Time.deltaTime);
        if (obj != null)
        {
            
            rb.velocity = (obj.position+new Vector3(0,1,0) - transform.position).normalized * Time.fixedDeltaTime * velocity * 100;

        }
        else
        {
            obj = getFirst();

        }


    }
    public Transform getFirst()
    {
        if (!WaveManager.Instance || WaveManager.Instance.EnemiesAlive == 0)
            return null;

        float minDist = 10000.0f;
        GameObject objetivo = null;
        foreach (GameObject g in WaveManager.Instance.getActiveEnemies()) {
            if ((g.transform.position - transform.position).magnitude <= minDist)
            {
                objetivo =(GameObject)g;
                minDist = (g.transform.position - transform.position).magnitude;
            }
        }
        
        return objetivo.transform;
    }

}
