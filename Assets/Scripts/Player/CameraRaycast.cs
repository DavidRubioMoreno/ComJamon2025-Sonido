using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    public float maxDistance = 100f; // Distancia máxima del Raycast

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            shootRay();
    }

    void shootRay()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red, 1.0f);

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            GameManager.Instance.objectDetectedRaycast(hit.collider.gameObject);
            Debug.Log("Objeto golpeado: " + hit.collider.gameObject.name);
        }
    }
}

