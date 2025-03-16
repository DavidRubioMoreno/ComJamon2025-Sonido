using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    public Vector3 maxScale = new Vector3(2f, 2f, 2f); // Escala máxima
    public float scaleSpeed = 1f; // Velocidad de escalado


    public IEnumerator ScaleObject()
    {
        while (transform.localScale.x < maxScale.x ||
               transform.localScale.y < maxScale.y ||
               transform.localScale.z < maxScale.z)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, maxScale, scaleSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
