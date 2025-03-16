using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    [SerializeField]
    private float lifetime = 2;

    float elapsed = 0;
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        elapsed += Time.deltaTime;
        if (elapsed > lifetime)
            Destroy(gameObject);
    }
}
