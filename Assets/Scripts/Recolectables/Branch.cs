using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Branch : Collectable
{
    public enum DungeonAssig
    {
        DUNGEON_1,
        DUNGEON_2,
        DUNGEON_3
    }

    public DungeonAssig currentDungeon;


    // Start is called before the first frame update

    // Update is called once per frame
    public override void onCollect() 
    {
        //SoundManager.Instance.PlaySound(SoundManager.Instance.mejora);
        GameManager.Instance.addBranch();
        switch(currentDungeon)
        {
            case DungeonAssig.DUNGEON_1:
                GameManager.Instance.portalsBool.dun1_finished = true;
               
                break;
            case DungeonAssig.DUNGEON_2:
                GameManager.Instance.portalsBool.dun2_finished = true;
                break;
            case DungeonAssig.DUNGEON_3:
                GameManager.Instance.portalsBool.dun3_finished = true;
                break;
        }
        SceneManager.LoadScene("PRINCIPAL");
        Destroy(transform.parent.gameObject);
    }
}
