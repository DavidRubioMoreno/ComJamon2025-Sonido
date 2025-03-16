using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : Collectable
{
    // Start is called before the first frame update

    // Update is called once per frame
    public override void onCollect() 
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.mejora);
        GameManager.Instance.addBranch();
        Destroy(transform.parent.gameObject);
    }
}
