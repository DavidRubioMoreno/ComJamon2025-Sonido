using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LifeComponent : MonoBehaviour
{
    public int vida;
    public int maxVida;
    public event Action Die;

    [SerializeField]
    private GameObject lifePrefab;
    public void OnDeath()
    {

        if (lifePrefab && UnityEngine.Random.Range(0, 3) == 0)
            Instantiate(lifePrefab, transform.position, Quaternion.identity);
        Die?.Invoke(); // Notifica al WaveManager que este enemigo muri�
        Destroy(gameObject); // Destruye al enemigo
    }

    public void LoseLife(int damage)
    {
        if(GetComponent<BlockAttack>() != null && GetComponent<BlockAttack>().isBlocking)
        {
            Debug.Log("NO DA�O");
            return;
        }

        vida -= damage;
        if (vida <= 0) OnDeath();
        
    }

    public void AddLife(int hp)
    {
        vida = Mathf.Min(vida + hp, maxVida);
    }
    // Start is called before the first frame update
    void Start()
    {
        maxVida = vida;
    }
}
