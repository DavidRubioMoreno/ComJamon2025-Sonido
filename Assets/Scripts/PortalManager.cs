using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _portal1_prefab;
    [SerializeField]
    private GameObject _portal2_prefab;
    [SerializeField]
    private GameObject _portal3_prefab;

    public Transform portal1_transform;
    public Transform portal2_transform;
    public Transform portal3_transform;

    public GameObject portal1;
    public GameObject portal2;
    public GameObject portal3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void unlockDun2()
    {
        StartCoroutine(GetComponent<AnimacionPortal>().MoveCameraAndOpenPortal(_portal2_prefab, portal2_transform));

        GameManager.Instance.portalsBool.dungeon_2 = true;
    }

    public void unlockDun3()
    {
        StartCoroutine(GetComponent<AnimacionPortal>().MoveCameraAndOpenPortal(_portal3_prefab, portal3_transform));
        GameManager.Instance.portalsBool.dungeon_3 = true;
    }
}
