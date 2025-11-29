using FMODUnity;
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

    [SerializeField]
    private bool setRandomInitialRotation = false;

    [SerializeField]
    private bool setRandomRotationForce = false;

    [SerializeField]
    private bool setRandomScale = false;

    [SerializeField]
    private float minRotationForce = 0;

    [SerializeField]
    private float maxRotationForce = 5;

    [SerializeField]
    private float minScale = 0.5f;

    [SerializeField]
    private float maxScale = 2;

    private float nextGenerationtime;

    [SerializeField]
    FMODUnity.EventReference branchesEvent;   // Seleccionado desde el inspector

    private FMOD.Studio.EventInstance branchesEventInstance;

    float elapsedTime = 0;

    bool init = false;
    // Start is called before the first frame update
    void Start()
    {
        nextGenerationtime = 0;

       branchesEventInstance = FMODManager.instance.CreateEventInstance(branchesEvent);

    }

    private void OnDestroy()
    {
        FMODManager.instance.StopEvent(branchesEventInstance);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > nextGenerationtime && !GameManager.Instance.Pause)
        {
            nextGenerationtime = Random.Range(minGenerationTime, maxGenerationTime);
            elapsedTime = 0;

            Vector3 finalPosition = transform.position;
            finalPosition.z += getRandomInterval(-radius, radius);
            finalPosition.x += getRandomInterval(-radius, radius);

            Quaternion finalRotation = Quaternion.identity;

            if (setRandomInitialRotation)
            {
                finalRotation = getRandomRotation();
            }

            GameObject objectSpawned = Instantiate(objectToGen, finalPosition, finalRotation);

            if (setRandomScale)
            {
                objectSpawned.transform.localScale *= getRandomInterval(minScale * 10, maxScale * 10) / 10;
            }

            if (setRandomRotationForce)
            {
                if (objectSpawned.GetComponent<Rigidbody>())
                {
                    Vector3 angularSpeed;
                    angularSpeed.x = getRandomInterval(minRotationForce, maxRotationForce);
                    angularSpeed.y = getRandomInterval(minRotationForce, maxRotationForce);
                    angularSpeed.z = getRandomInterval(minRotationForce, maxRotationForce);
                    objectSpawned.GetComponent<Rigidbody>().angularVelocity = angularSpeed;
                    //Debug.Log(angularSpeed);
                }
            }

            if (init)
            {
                //branchesEventInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(objectSpawned));
            }          
        }
    }

    

    private void FixedUpdate()
    {
        if(!init && GameManager.Instance.Player && !GameManager.Instance.Pause)
        {
            
            branchesEventInstance.start();

            init = true;
        }
        if (init && !GameManager.Instance.Pause)
        {
            branchesEventInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(GameManager.Instance.Player));
        }
    }

    Quaternion getRandomRotation()
    {
        Quaternion finalRotation = Quaternion.identity;
        finalRotation.x = getRandomInterval(0, 10) / 10;
        finalRotation.y = getRandomInterval(0, 10) / 10;
        finalRotation.z = getRandomInterval(0, 10) / 10;
        finalRotation.w = getRandomInterval(0, 10) / 10;
        return finalRotation;
    }

    float getRandomInterval(float min, float max)
    {
        return Random.Range(min, max);
    }
}
