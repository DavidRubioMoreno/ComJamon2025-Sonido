using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBranches : MonoBehaviour
{
    public Camera cameraA;
    public Camera cameraB;
    public Transform player;
    public Transform playerTargetPosition;
    public GameObject projectilePrefab;
    public Transform[] targets;
    public float projectileSpeed = 5f;
    public Transform prefabSpawnPosition;
    public GameObject prefabToInstantiate;
    public float delayBeforeReturningCamera = 3f;
    bool begin = false;


    private void Update()
    {
        if(!begin && GameManager.Instance.ReadyToANim())
        {
            begin = true;
            StartCoroutine(SwitchCameraAndExecute());
        }
    }

    IEnumerator SwitchCameraAndExecute()
    {
        // Cambiar de cámara
        cameraA.gameObject.SetActive(false);
        cameraB.gameObject.SetActive(true);

        // Mover jugador
        GameManager.Instance.Player.transform.position = playerTargetPosition.position;

        // Instanciar y mover los objetos uno por uno
        foreach (Transform target in targets)
        {
            GameObject projectile = Instantiate(projectilePrefab, GameManager.Instance.Player.transform.position, Quaternion.identity);
            yield return StartCoroutine(MoveProjectile(projectile, target.position));
        }

        // Instanciar prefab en otra posición
        Instantiate(prefabToInstantiate, prefabSpawnPosition.position, prefabSpawnPosition.rotation);

        yield return new WaitForSeconds(delayBeforeReturningCamera);
        cameraA.gameObject.SetActive(true);
        cameraB.gameObject.SetActive(false);
    }

    IEnumerator MoveProjectile(GameObject projectile, Vector3 targetPosition)
    {
        while (Vector3.Distance(projectile.transform.position, targetPosition) > 0.1f)
        {
            projectile.transform.position = Vector3.MoveTowards(projectile.transform.position, targetPosition, projectileSpeed * Time.deltaTime);
            yield return null;
        }
        //Destroy(projectile);
    }
}
