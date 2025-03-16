using Unity.VisualScripting;
using UnityEngine;

public class ZonaPegajosa : MonoBehaviour
{
    public float duracion = 5f;
    private GameObject player;
    private void Start()
    {
        player = GameManager.Instance.Player;
        Destroy(gameObject, duracion); // Se destruye después de X segundos
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player._maxSpeed = 3;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player._maxSpeed = 7;
            }
        }
    }
    private void OnDestroy()
    {
        
        player.GetComponent<PlayerMovement>()._maxSpeed = 7;
        
    }
}
