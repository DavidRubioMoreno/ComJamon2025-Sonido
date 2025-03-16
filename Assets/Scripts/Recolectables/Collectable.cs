
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Yey");
        if (collision.GetComponent<PlayerMovement>())
            onCollect();
    }

    // Update is called once per frame
    public virtual void onCollect()
    {

    } 
}
