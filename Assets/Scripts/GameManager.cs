using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;


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
    private float timeToStartDancing = 120;
    [SerializeField]
    public GameObject canvas;
    [SerializeField]
    private float dancingTime = 10;

    private Cargado c;
    private bool _cargarPartida = false;
    private bool paused = false;

    //[SerializeField]
    private GameObject player;
    [SerializeField]
    public Camera camara;
    private float elapsedTime = 0;
    private Scene _currentScene;
    public enum State { DANCING, NORMAL, PAUSE }

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

        canvas.GetComponent<OptionsMenu>().ChangeGeneralVolume(PlayerPrefs.GetFloat("Music", 1f));
        canvas.GetComponent<OptionsMenu>().ChangeMusicVolume(PlayerPrefs.GetFloat("SFX", 1f));
    }

    private void Update()
    {
        
        elapsedTime += Time.deltaTime;
        updateState(CurrentState);
        if (!menu && !nocreado && cinematica && !_cargarPartida)
        {
            createPlayer();

        }
        if (_currentScene != SceneManager.GetActiveScene() && cinematica)
        {
            if (_cargarPartida)
            {
                _cargarPartida = false;
                Cargar();
            }
            _currentScene = SceneManager.GetActiveScene();
        }
       if (UIManager.Instance && player)
        {
            UIManager.Instance.UpdateHealthBar(player.GetComponent<LifeComponent>().vida, player.GetComponent<LifeComponent>().maxVida);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        nocreado = false;
    }

    public void createPlayer()
    {
        //camara = Camera.main;
        
        nocreado = true;
        int selectedIndex = PlayerPrefs.GetInt("SelectedCharacter", 0); // Carga el personaje elegido
        Debug.Log(selectedIndex);
        Debug.Log(characters[selectedIndex]);
        player = Instantiate(characters[selectedIndex], SpawnPoint.Instance.Position.position, SpawnPoint.Instance.Position.rotation);
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
                    player.GetComponent<PlayerMovement>()._mainCamera = Camera.main;
                    Camera.main.GetComponent<ThirdPersonCamera>().target = player.transform;
                    break;
            }
        }
        
    }
    public void GuardarPartida()
    {

    }
    public void Options(InputAction.CallbackContext callback)
    {
        if (canvas.active)
        {
            canvas.SetActive(false);
            paused = false;
        }
        else
        {
            canvas.SetActive(true);
            paused = true;
        }

    }
}