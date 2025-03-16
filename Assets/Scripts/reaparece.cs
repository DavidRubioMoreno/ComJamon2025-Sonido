
using UnityEngine;


public class reaparece : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            collision.transform.position = SpawnPoint.Instance.Position.position;
        }
    }
}
