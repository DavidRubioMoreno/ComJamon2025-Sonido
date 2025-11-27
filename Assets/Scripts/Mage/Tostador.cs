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
        if(objects.Count <= 3)
        {
            return objects;
        }
        else
        {
                return objects
            .OrderBy(obj => Vector3.Distance(transform.position, obj.transform.position))
            .Take(3)
            .ToList();
        }

    }
    public void storm()
    {
        _animator.SetBool("Strong", false);
        //SoundManager.Instance.PlaySound(SoundManager.Instance.masmagia);
        if (WaveManager.Instance && WaveManager.Instance.EnemiesAlive > 0)
        {
            List<GameObject> objs = GetThreeClosestObjects(WaveManager.Instance.getActiveEnemies());
            for (int i = 0; i < objs.Count; i++)
            {
                if(objs[i] == null)
                    Instantiate(rayos, transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)), Quaternion.identity);
                else
                Instantiate(rayos, objs[i].transform.position, Quaternion.identity);
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                Instantiate(rayos, transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)), Quaternion.identity);
            }
        }

        FMODManager.instance.PlayMageThunder();
    }
    public void StrongAttack(InputAction.CallbackContext callback)
    {
       
        if (!_animator.GetBool("Strong"))
        {
            _sAttack = callback.ReadValue<float>();
            FMODManager.instance.PlayMageInvoking();
        }
    }

}
