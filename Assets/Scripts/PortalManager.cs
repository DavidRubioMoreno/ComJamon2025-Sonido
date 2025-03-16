using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    [Header("PORTAL 1")]
    [SerializeField]
    private GameObject _portal1;

    [Header("PORTAL 2")]
    [SerializeField]
    private GameObject _portal2;
    [SerializeField]
    private Camera cameraDun2;
    [SerializeField]
    private Transform _tr2;

    [Header("PORTAL 3")]
    [SerializeField]
    private GameObject _portal3;
    [SerializeField]
    private Camera cameraDun3;
    [SerializeField]
    private Transform _tr3;

    [Header("DETECTIONS")]
    [SerializeField]
    BoxCollider _triggerAnimation;

    // Start is called before the first frame update

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            Debug.Log("DEBERIA COLISIONAR ;P");
            GameManager.PortalsBool pb = GameManager.Instance.portalsBool;

            if (pb.dun1_finished && !pb.dungeon_2)
            {
                unlockDun2();
            }
            else if (pb.dun2_finished && !pb.dungeon_3)
            {
                unlockDun3();
            }
        }

    }
  

    private void Start()
    {
        _portal2.SetActive(false);
        _portal3.SetActive(false);
    }
    public void unlockDun2()
    {
        StartCoroutine(GetComponent<AnimacionPortal>().MoveCameraAndOpenPortal(_portal2, cameraDun2, _tr2));

        GameManager.Instance.portalsBool.dungeon_2 = true;
    }
            
    public void unlockDun3()
    {
        StartCoroutine(GetComponent<AnimacionPortal>().MoveCameraAndOpenPortal(_portal3, cameraDun3, _tr3));
        GameManager.Instance.portalsBool.dungeon_3 = true;
    }
}
