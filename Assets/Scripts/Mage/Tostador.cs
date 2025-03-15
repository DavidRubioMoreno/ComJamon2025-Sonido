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

    private Animator _animator;

    private float _sAttack;
    void Start()
    {
        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if(_sAttack > 0)
        {
            _sAttack = 0;
            _animator.SetBool("Strong",true);
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
        _animator.SetBool("Strong", false);

        Debug.Log(EnemieManager.Instance.getEnemies().Count);
       
        if (WaveManager.Instance.EnemiesAlive > 0)
        {
            List<GameObject> objs = GetThreeClosestObjects(WaveManager.Instance.getActiveEnemies());
            for (int i = 0; i < WaveManager.Instance.EnemiesAlive||i<3; i++)
            {
                Instantiate(rayos, objs[i].transform.position, Quaternion.identity);
            }
        }
    }
    public void StrongAttack(InputAction.CallbackContext callback)
    {
        Debug.Log(_animator.GetBool("Strong"));
        if (!_animator.GetBool("Strong"))
        {
            _sAttack = callback.ReadValue<float>();

        }
    }

}
