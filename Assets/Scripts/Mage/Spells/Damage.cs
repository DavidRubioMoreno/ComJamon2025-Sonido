using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int damage;
    [SerializeField]
    private GameObject explosion;

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
                LifeComponent l = other.GetComponent<LifeComponent>();
                if (l != null)
                {
                    l.LoseLife(damage);
                }
                if (GetComponent<Dest>() != null)
                {
                    if (explosion)
                        Instantiate(explosion, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                hit = true;
            }
        }

    }
}
