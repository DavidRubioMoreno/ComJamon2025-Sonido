using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField]
    public bool menu;
    private bool nocreado;
    public bool cinematica; 
    private int branchesCollected = 0;
    public GameObject[] characters;
    [SerializeField]
    private ObjectGenerator branchGenerator; //cuando suena la musica caen ramas

    [SerializeField]
    private float timeToStartDancing = 120;

    [SerializeField]
    private float dancingTime = 10;

    private Cargado c;
    private bool _cargarPartida = false;

    //[SerializeField]
    private GameObject player;
    [SerializeField]
    public Camera camara;
    private float elapsedTime = 0;
    private Scene _currentScene;
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
        c = GetComponent<Cargado>();
        _currentScene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        if(_currentScene != SceneManager.GetActiveScene())
        {
            if (_cargarPartida)
            {
                Cargar();
            }
            _currentScene = SceneManager.GetActiveScene();
        }
        elapsedTime += Time.deltaTime;
        updateState(CurrentState);
        if (!menu && !nocreado && cinematica)
        {
            createPlayer();

        }
        if (UIManager.Instance && nocreado)
        {
            UIManager.Instance.UpdateHealthBar(player.GetComponent<LifeComponent>().maxVida, player.GetComponent<LifeComponent>().vida);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        nocreado = false;
    }
    //public void addPlayer()
    //{
    //    nocreado = false;
    //}
    public void createPlayer()
    {
        //camara = Camera.main;
     
        nocreado = true;
        int selectedIndex = PlayerPrefs.GetInt("SelectedCharacter", 0); // Carga el personaje elegido
        Debug.Log(selectedIndex);
        Debug.Log(characters[selectedIndex]);
        player = Instantiate(characters[selectedIndex],  SpawnPoint.Instance.Position.position, SpawnPoint.Instance.Position.rotation);
        Debug.Log(player);
        player.GetComponent<PlayerMovement>()._mainCamera = Camera.main;
        Camera.main.GetComponent<ThirdPersonCamera>().target = player.transform;
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
    public void CargarPartida()
    {
        _cargarPartida = true;
        SceneManager.LoadScene(c.GetMensajes("Escena"));
    }
    private void Cargar()
    {
        var a = c.GetPlayerData();
        foreach (var item in c.mData.dataTypes)
        {
            switch (item.id)
            {
                case "Player":
                    player = Instantiate(characters[a.type.type], new Vector3(a.position.x, a.position.y, a.position.z),
                        new Quaternion(a.rotation.x, a.rotation.y, a.rotation.z, a.rotation.w));
                    break;
            }
        }
        _cargarPartida = false;
    }
    public void GuardarPartida()
    {

    }
}