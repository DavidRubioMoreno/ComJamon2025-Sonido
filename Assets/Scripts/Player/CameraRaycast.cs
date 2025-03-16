
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    public float maxDistance = 100f; // Distancia máxima del Raycast

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            shootRay();

        shootInfoRay();
    }

    void shootRay()//Metodo para recoger el recolectable
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

    
    void shootInfoRay()//Metodo para marcar la mira como que se ha detectado un recolectable
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            Debug.Log("Objeto golpeado: " + hit.collider.gameObject.name);
        }
    }
}

