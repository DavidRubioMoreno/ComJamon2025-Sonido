using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tostador : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject rayos;

    private float _sAttack;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_sAttack > 0)
        {
            _sAttack = 0;
            storm();
        }
    }

    List<GameObject> GetThreeClosestObjects(List<GameObject> objects)
    {
        return objects
            .OrderBy(obj => Vector3.Distance(transform.position, obj.transform.position))
            .Take(3)
            .ToList();
    }
    public void storm()
    {
        Debug.Log(EnemieManager.Instance.getEnemies().Count);
        List<GameObject> objs = GetThreeClosestObjects(EnemieManager.Instance.getEnemies());
        if (EnemieManager.Instance.getEnemies().Count >= 3)
        {
            for (int i = 0; i < 3; i++)
            {
                Instantiate(rayos, objs[i].transform.position, Quaternion.identity);
            }
        }
        else
        {
            for (int i = 0; i < EnemieManager.Instance.getEnemies().Count; i++)
            {
                Instantiate(rayos, objs[i].transform.position, Quaternion.identity);
            }
        }


    }
    public void StrongAttack(InputAction.CallbackContext callback)
    {
        _sAttack = callback.ReadValue<float>();
    }

}
