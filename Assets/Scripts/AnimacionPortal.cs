using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AnimacionPortal : MonoBehaviour
{
    public Camera cameraA;
    public Camera cameraB;

    public Transform cameraFin;
    public float delayBeforeReturningCamera = 3f;
    public float camSpeed = 4f;

    void Start()
    {
    }
    public IEnumerator MoveCameraAndOpenPortal(GameObject portal, Transform portalTr)
    {
        cameraA.gameObject.SetActive(false);
        cameraB.gameObject.SetActive(true);
        yield return StartCoroutine(MoveCameraPortal());
        
        Instantiate(portal, portalTr.position, portalTr.rotation);

        yield return new WaitForSeconds(delayBeforeReturningCamera);

        cameraA.gameObject.SetActive(true);
        cameraB.gameObject.SetActive(false);
    } 

    private IEnumerator MoveCameraPortal()
    {
        while (Vector3.Distance(cameraB.transform.position, cameraFin.position) > 0.1f)
        {
            cameraB.transform.position = Vector3.MoveTowards(cameraB.transform.position, cameraFin.position, camSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
