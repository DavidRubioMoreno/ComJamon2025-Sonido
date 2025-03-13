using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int branchesCollected = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Debug.Log(branchesCollected);
    }

    public void addBranch()
    {
        branchesCollected++;
    }

    public void branchUsed()
    {
        branchesCollected = Mathf.Max(0, branchesCollected - 1);
    }

    public void objectDetectedRaycast(GameObject gameObject)
    {
        if (gameObject.GetComponent<Collectable>())
            gameObject.GetComponent<Collectable>().onCollect();
    }
}