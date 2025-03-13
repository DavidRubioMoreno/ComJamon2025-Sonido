using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{

    [SerializeField]
    private GameObject objectToGen;

    [SerializeField]
    private float radius;

    [SerializeField]
    private float minGenerationTime;

    [SerializeField]
    private float maxGenerationTime;

    private float nextGenerationtime;

    float elapsedTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        nextGenerationtime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > nextGenerationtime)
        {
            nextGenerationtime = Random.Range(minGenerationTime, maxGenerationTime);
            elapsedTime = 0;

            Vector3 finalPosition = transform.position;
            finalPosition.z += Random.Range(-radius, radius);
            finalPosition.x += Random.Range(-radius, radius);

            Quaternion finalRotation = Quaternion.identity;
            finalRotation.x = Random.Range(0, 2);
            finalRotation.y = Random.Range(0, 2);
            finalRotation.z = Random.Range(0, 2);
            finalRotation.w = Random.Range(0, 2);

            Instantiate(objectToGen, finalPosition, finalRotation);
        }
    }
}
