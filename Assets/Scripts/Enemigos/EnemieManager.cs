
using System.Collections.Generic;
using UnityEngine;

public class EnemieManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<GameObject> enemies;
    
    public static EnemieManager Instance { get; private set; }

    WaveManager mngr;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        mngr = WaveManager.Instance;
        enemies = mngr.getActiveEnemies();
    }

    private void Update()
    {
       
    }

    public List<GameObject> getEnemies() { 
        return enemies;
    }
}
