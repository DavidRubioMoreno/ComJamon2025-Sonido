using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectFinal : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovement>())
            GameManager.Instance.enterFinalZone();
    }
}
