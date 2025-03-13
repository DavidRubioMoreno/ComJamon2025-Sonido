using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Dest : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float velocity;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        //rb.MovePosition(*Time.deltaTime);
        rb.velocity= (getFirst().position - transform.position).normalized*Time.fixedDeltaTime*velocity*100;
    }
    private Transform getFirst()
    {
        float minDist = 10000.0f;
        GameObject objetivo = null;
        foreach (GameObject g in EnemieManager.Instance.getEnemies()) {
            if ((g.transform.position - transform.position).magnitude <= minDist)
            {
                objetivo =(GameObject)g;
                minDist = (g.transform.position - transform.position).magnitude;
            }
        }
        return objetivo.transform;
    }

}
