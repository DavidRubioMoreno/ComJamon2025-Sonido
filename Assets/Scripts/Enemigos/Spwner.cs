using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spwner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject[] esk;

    private float elapsed;
    void Start()
    {
        elapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        elapsed+=Time.deltaTime;
        if (elapsed > 2.5)
        {
            GameObject duro=Instantiate(esk[Random.RandomRange(0,3)],transform.position,Quaternion.identity);
            WaveManager.Instance.Peruano(duro);  
            Destroy(gameObject);
        }
    }
}
