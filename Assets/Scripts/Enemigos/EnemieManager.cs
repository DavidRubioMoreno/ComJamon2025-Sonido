using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<GameObject> enemies = new List<GameObject>();
    
    public static EnemieManager Instance { get; private set; }

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<GameObject> getEnemies()
    {
        return enemies;
    }
}
