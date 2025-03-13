using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int branchesCollected = 0;
    public GameObject[] characters;
    [SerializeField]
    private ObjectGenerator branchGenerator; //cuando suena la musica caen ramas

    [SerializeField]
    private float timeToStartDancing = 120;

    [SerializeField]
    private float dancingTime = 10;

    //[SerializeField]
    private GameObject player;
    [SerializeField]
    public GameObject camara;
    private float elapsedTime = 0; 

    public enum State { DANCING, NORMAL }

    private State state;
    public State CurrentState { get { return state; } }

    public GameObject Player { get { return player; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            state = State.NORMAL;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //branchGenerator.enabled = false;
        createPlayer();
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        updateState(CurrentState);
    }
    public void createPlayer()
    {
        int selectedIndex = PlayerPrefs.GetInt("SelectedCharacter", 0); // Carga el personaje elegido
        player = Instantiate(characters[selectedIndex], Vector3.up, Quaternion.identity);
        camara.GetComponent<ThirdPersonCamera>().target = player.transform;
    }
    private void updateState(State currentState)
    {
        if (currentState == State.DANCING)
        {
            Debug.Log("Bailando");

            if (elapsedTime > dancingTime)
            {
                elapsedTime = 0;
                exitState(currentState);
                currentState = State.NORMAL;
                enterState(currentState);
            }
        }
        else if (currentState == State.NORMAL)
        {
            //Debug.Log("No bailo");

            if (elapsedTime > timeToStartDancing)
            {
                elapsedTime = 0;
                exitState(currentState);
                currentState = State.DANCING;
                enterState(currentState);
            }
        }
    }

    private void enterState(State currentState) 
    { 
        //if(currentState == State.DANCING)
            //branchGenerator.enabled = true;

    }
    private void exitState(State currentState)
    {
        //if(currentState == State.DANCING)
           // branchGenerator.enabled = false;
    }

    public void OnDeath()
    {

    }

    private void FixedUpdate()
    {
       

        //Debug.Log(branchesCollected);
       
    }

    public void addBranch()
    {
        branchesCollected++;
    }

    public void branchUsed()
    {
        branchesCollected = Mathf.Max(0, branchesCollected - 1);
    }

    public void objectDetectedRaycast(GameObject gameObject)
    {
        if (gameObject.GetComponent<Collectable>())
            gameObject.GetComponent<Collectable>().onCollect();
    }
}