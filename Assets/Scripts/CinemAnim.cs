using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemAnim : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject puño;

    [SerializeField]
    private GameObject boss;

    [SerializeField]
    private Transform bossT;

    private Animator anim;
    private Rigidbody r;

    public float time;
    private float elapsed;
    void Start()
    {
        elapsed = 0;
        anim = GetComponent<Animator>();
        r = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsed+=Time.deltaTime;
        if (elapsed>time)
        {
            anim.SetBool("Cinem", true);
        }
    }

    public void portal()
    {
        Instantiate(puño,transform.position, Quaternion.identity);
        //SoundManager.Instance.PlaySound(SoundManager.Instance.manin, 4);

        //r.AddForce(new Vector3 (0,-100,0));
    }

    public void tp()
    {
        Instantiate(puño, bossT.position, Quaternion.identity);

        GameObject obj=Instantiate(boss, bossT.position, Quaternion.identity);
        WaveManager.Instance.Peruano(obj);
        WaveManager.Instance.EscenaFinal();
        //BackgroundMusic.Instance.bossfinal();

        Destroy(gameObject);

    }
}
