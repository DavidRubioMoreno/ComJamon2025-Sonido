using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Enemy Configuration")]
    public GameObject[] enemyPrefabs; // Lista de enemigos disponibles
    public GameObject bossPrefab; // Prefab del jefe final
    public Transform[] spawnAreas; // Área donde aparecerán los enemigos
    private Transform spawnArea;
    public int areaRadius = 5;
    public int numberOfWaves = 3; // Número total de oleadas
    public int enemiesPerWave = 5; // Cantidad de enemigos por oleada

    [Header("Spawn Timing")]
    public float spawnDelay = 2f; // Tiempo entre la aparición de enemigos
    public float preSpawnEffectTime = 2f; // Tiempo de efecto antes del spawn

    [Header("Effects")]
    public GameObject[] spawnParticles; // Prefab de partículas antes de la aparición del enemigo

    private int currentWave = 0;
    public List<GameObject> activeEnemies = new List<GameObject>(); // Lista de enemigos vivos
    private bool spawning = false;

    [SerializeField]
    private float timeToBegin = 3;
    private float elapsed = 0;

    private bool started = false;

    private bool bossSpawned = false;
    public static WaveManager Instance { get; private set; }

    public int EnemiesAlive { get { return activeEnemies.Count; } }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
       
    }

    public List<GameObject> getActiveEnemies()
    {
        return activeEnemies;
    }

    void Update()
    {
        elapsed += Time.deltaTime;

        if (!started && elapsed > timeToBegin)
        {
            started = true;
            StartNextWave();
        }

        // Si no estamos generando enemigos y ya no hay enemigos vivos, pasamos a la siguiente oleada
        if (started && !spawning && activeEnemies.Count == 0)
        {
            if (currentWave < numberOfWaves)
            {
                StartNextWave();
            }
            else if (bossPrefab != null && !bossSpawned)
            {
                bossSpawned = true;
                SpawnBoss();
            }
        }
        //Debug.Log(activeEnemies.Count);
       

    }

    void StartNextWave()
    {
        if(currentWave < spawnAreas.Length) spawnArea = spawnAreas[currentWave];
        currentWave++;
        spawning = true;
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {

        for (int i = 0; i < enemiesPerWave; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();

            // Generar partículas en la posición exacta
            GameObject[] particles = new GameObject[spawnParticles.Length];
            for (int j = 0; j < spawnParticles.Length; j++)
            {
                if(spawnParticles[j] != null)
                {
                    particles[j] = Instantiate(spawnParticles[j], spawnPosition, Quaternion.identity);
                    if (j == 1) particles[j].transform.localScale *= 3;
                    SoundManager.Instance.PlaySound(SoundManager.Instance.invocar, 0.2f);
                }
            }
           

            // Esperar antes de generar el enemigo
            yield return new WaitForSeconds(preSpawnEffectTime);

            SpawnEnemy(spawnPosition);

            for (int k = 0; k < spawnParticles.Length; k++)
            {
                Destroy(particles[k]);
            }
            yield return new WaitForSeconds(spawnDelay);
        }

        spawning = false;
    }

    void SpawnEnemy(Vector3 position)
    {
        if (enemyPrefabs.Length == 0) return;

        GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        GameObject enemy = Instantiate(enemyToSpawn, position, Quaternion.identity);
        activeEnemies.Add(enemy);

        enemy.GetComponent<LifeComponent>().Die += () => activeEnemies.Remove(enemy);
    }
    public void Peruano(GameObject enemy)
    {
        activeEnemies.Add(enemy);

        enemy.GetComponent<LifeComponent>().Die += () => activeEnemies.Remove(enemy);
    }
    void SpawnBoss()
    {
        spawning = true;
        Vector3 bossPosition = spawnArea.position;

        if (spawnParticles != null)
        {
            GameObject particles = Instantiate(spawnParticles[0], bossPosition, Quaternion.identity);
            Destroy(particles, preSpawnEffectTime);
        }

        StartCoroutine(SpawnBossWithDelay(bossPosition));
    }

    IEnumerator SpawnBossWithDelay(Vector3 position)
    {
        yield return new WaitForSeconds(preSpawnEffectTime);
        GameObject boss = Instantiate(bossPrefab, position, Quaternion.identity);
        activeEnemies.Add(boss);
        spawning = false;
    }

    Vector3 GetRandomSpawnPosition()
    {
        return spawnArea.position + new Vector3(
            Random.Range(-areaRadius, areaRadius),
            0,
            Random.Range(-areaRadius, areaRadius)
        );
    }
}

