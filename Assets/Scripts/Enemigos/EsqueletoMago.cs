using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsqueletoMago : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject spawn;

    private float elapsedTime;

    [SerializeField]
    private float spawnTimeMax;

    [SerializeField]
    private float spawnTimeMin;

    private float spawnTime;

    private Animator animator;
    void Start()
    {
        animator= GetComponent<Animator>();
        spawnTime = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > spawnTime)
        {
            FMODManager.instance.PlayMageInvoking();
            Vector3 origen= transform.position+new Vector3(Random.Range(-10.5f, 10.5f),0, Random.Range(-10.5f, 10.5f));
            //SoundManager.Instance.PlaySound(SoundManager.Instance.bruja);
            Instantiate(spawn,origen,Quaternion.identity);
            elapsedTime = 0;
            spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
            //Instantiate(esq[Random.Range(0, 3)],origen,Quaternion.identity);
        }
        if(GameManager.Instance.Player)
        transform.LookAt(GameManager.Instance.Player.transform);
    }
}
