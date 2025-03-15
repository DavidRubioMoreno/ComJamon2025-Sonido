using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int damage;

    private bool hit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            if (!hit)
            {
                Debug.Log("Tostao");
                LifeComponent l = other.GetComponent<LifeComponent>();
                if (l != null)
                {
                    l.LoseLife(damage);
                }
                if (GetComponent<Dest>() != null)
                {
                    Destroy(gameObject);
                }
                hit = true;
            }
        }

    }
}
