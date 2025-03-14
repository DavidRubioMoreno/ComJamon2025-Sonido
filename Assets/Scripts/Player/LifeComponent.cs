using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LifeComponent : MonoBehaviour
{
    public int vida;
    public event Action Die;
    public void OnDeath()
    {
        Die?.Invoke(); // Notifica al WaveManager que este enemigo murió
        Destroy(gameObject); // Destruye al enemigo
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
