using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Dest : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float velocity;

    private Transform obj;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        obj = getFirst();
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
            rb.velocity = (obj.position - transform.position).normalized * Time.fixedDeltaTime * velocity * 100;
        }
        else
        {
            obj = getFirst();

        }
    }
    private Transform getFirst()
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
