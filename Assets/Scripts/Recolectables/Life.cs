using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : Collectable
{
    public int HP = 1;
    // Start is called before the first frame update
    public override void onCollect()
    {
        //SoundManager.Instance.PlaySound(SoundManager.Instance.mejora);
        GameManager.Instance.Player.GetComponent<LifeComponent>().AddLife(HP);
        Destroy(transform.parent.gameObject);
    }
}
