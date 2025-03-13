using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : Collectable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void onCollect() 
    {
        GameManager.Instance.addBranch();
        Destroy(gameObject);
    }
}
