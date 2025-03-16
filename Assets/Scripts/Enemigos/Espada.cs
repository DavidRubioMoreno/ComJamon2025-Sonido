using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada : MonoBehaviour
{
    public int _damage;

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("hitPlayer");
        if (collision.GetComponent<PlayerMovement>())
        {
            collision.GetComponent<LifeComponent>().LoseLife(_damage);
        }
    }
}
