using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AnimacionPortal : MonoBehaviour
{
    public Camera cameraMain;
    public float delayBeforeReturningCamera = 3f;
    public float camSpeed = 20f;

    void Start()
    {
    }
    public IEnumerator MoveCameraAndOpenPortal(GameObject portal, Camera camera, Transform cameraFin)
    {
        UIManager.Instance.gameObject.SetActive(false);
 
        cameraMain.gameObject.SetActive(false);
        camera.gameObject.SetActive(true);
        yield return StartCoroutine(MoveCameraPortal(camera, cameraFin));
        
        portal.SetActive(true);
        StartCoroutine(portal.GetComponent<Scaler>().ScaleObject()); 

        yield return new WaitForSeconds(delayBeforeReturningCamera);

        cameraMain.gameObject.SetActive(true);
        camera.gameObject.SetActive(false);

        UIManager.Instance.gameObject.SetActive(true);
    } 

    private IEnumerator MoveCameraPortal(Camera camera, Transform cameraFin)
    {
        while (Vector3.Distance(camera.transform.position, cameraFin.position) > 0.1f)
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, cameraFin.position, camSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
