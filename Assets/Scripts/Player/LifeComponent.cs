using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeComponent : MonoBehaviour
{
    public int vida;

    public void OnDeath()
    {

    }

    public void LoseLife(int damage)
    {
        vida-= damage;
        if (vida <= 0) OnDeath();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
