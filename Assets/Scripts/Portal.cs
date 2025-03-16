using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField]
    public string cargar;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.portales);
            SceneManager.LoadScene(cargar);
        }
    }
}
