using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada : MonoBehaviour
{
    public int _damage;

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("dado");
        LifeComponent life = collision.gameObject.GetComponent<LifeComponent>();
        if (life != null)
        {
            life.LoseLife(_damage);
        }
    }
}
