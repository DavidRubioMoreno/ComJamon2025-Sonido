using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class LifeComponent : MonoBehaviour
{
    public int vida;
    public int maxVida;
    public event Action Die;

    [SerializeField]
    private GameObject lifePrefab;
    [SerializeField]
    private GameObject deathParticles;
    [SerializeField]
    private GameObject importantBranch;
    [SerializeField]
    private GameObject missile;
    [SerializeField]
    private bool dropBranch;
    [SerializeField]
    private bool spawnMissile;

    public void OnDeath()
    {
        //SoundManager.Instance.PlaySound(SoundManager.Instance.muerte, 0.5f);
        if (lifePrefab && UnityEngine.Random.Range(0, 3) == 0)
            Instantiate(lifePrefab, transform.position, Quaternion.identity);
        if(deathParticles)
            Instantiate(deathParticles, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        if (dropBranch && importantBranch)
            Instantiate(importantBranch, transform.position + new Vector3(0, 4, 0), Quaternion.identity);
        if (spawnMissile && missile)
            Instantiate(missile, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        if (GetComponent<PlayerMovement>())
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //if (GetComponent<PlayerMovement>())
        //    SceneManager.LoadScene("PRINCIPAL");
        Die?.Invoke(); // Notifica al WaveManager que este enemigo muri�
        Destroy(gameObject); // Destruye al enemigo
    }

    public void LoseLife(int damage)
    {
        if(GetComponent<BlockAttack>() != null && GetComponent<BlockAttack>().isBlocking)
        {
            Debug.Log("NO DAÑO");
            return;
        }

        if (UIManager.Instance && GetComponent<PlayerMovement>())
            UIManager.Instance.Hit();

        vida -= damage;

        if (!GetComponent<PlayerMovement>())
        {
            FMODManager.instance.PlayEnemyDamaged();
        }
        if (vida <= 0) OnDeath();
        Debug.Log(vida);
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
