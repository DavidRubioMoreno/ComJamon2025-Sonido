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
    public int branchesCollected = 0;
    public GameObject[] characters;

    [SerializeField]
    private float timeToStartDancing = 120;
    [SerializeField]
    public GameObject canvas;
    [SerializeField]
    private float dancingTime = 10;

    private Cargado cargado;
    private Guardado guardado;
    private bool _cargarPartida = false;
    private bool _partidaGuardada = false;
    private bool enterZone = false;
    public SceneData sData;
    public PlayerData pData;

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

    public struct PortalsBool
    {
        public bool dungeon_1;
        public bool dun1_finished;

        public bool dungeon_2;
        public bool dun2_finished;

        public bool dungeon_3;
        public bool dun3_finished;

        // Constructor opcional para inicializar los valores
        public PortalsBool(bool d1, bool d2, bool d3)
        {
            dungeon_1 = d1;
            dungeon_2 = d2;
            dungeon_3 = d3;
            dun1_finished = false;
            dun2_finished = false;
            dun3_finished = false;
        }
    }

    // Para guardar el avance de las dungeons que llevamos
    public PortalsBool portalsBool; 

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
        cargado = GetComponent<Cargado>();
        guardado = GetComponent<Guardado>();
        _currentScene = SceneManager.GetActiveScene();

        canvas.GetComponent<OptionsMenu>().ChangeGeneralVolume(PlayerPrefs.GetFloat("Music", 1f));
        canvas.GetComponent<OptionsMenu>().ChangeMusicVolume(PlayerPrefs.GetFloat("SFX", 1f));

        portalsBool = new PortalsBool(true, false, false);

        if (sData.sceneName != null)
        {
            _partidaGuardada = true;
        }
    }

    public void enterFinalZone()
    {
        if(branchesCollected >= 3)
        {
            enterZone = true;
        }
    }

    public bool ReadyToANim()
    {
        return branchesCollected >= 3 && enterZone;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GuardarPartida();
        }
        elapsedTime += Time.deltaTime;
        updateState(CurrentState);
        if (cinematica && _partidaGuardada)
        {
            if (_cargarPartida)
            {
                _cargarPartida = false;
                Cargar();
            }
        }
        if (!menu && !nocreado && cinematica && !_cargarPartida)
        {
            createPlayer();

        }
        if (_currentScene != SceneManager.GetActiveScene())
        {
            _currentScene = SceneManager.GetActiveScene();
        }


        
       
       if (UIManager.Instance && player)
        {
            UIManager.Instance.UpdateHealthBar(player.GetComponent<LifeComponent>().vida, player.GetComponent<LifeComponent>().maxVida);
        }
        
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 4 ||level == 1)
            cinematica = false;
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
        if (sData != null)
        {
            _cargarPartida = true;
            SceneManager.LoadScene(sData.sceneName);
        }
        else Debug.Log("No existe partida guardada");
       
    }
    private void Cargar()
    {
        var a = cargado.GetPlayerData();
        foreach (var item in cargado.mData.dataTypes)
        {
            switch (item.id)
            {
                case "Player":
                    player = Instantiate(characters[a.type.type], new Vector3(a.position.x, a.position.y, a.position.z),
                        new Quaternion(a.rotation.x, a.rotation.y, a.rotation.z, a.rotation.w));
                    player.GetComponent<PlayerMovement>()._mainCamera = Camera.main;
                    Camera.main.GetComponent<ThirdPersonCamera>().target = player.transform;
                    Debug.Log(characters[a.type.type]);
                    break;
            }
        }
        
    }
    public void GuardarPartida()
    {
        pData.position.x = player.transform.position.x;
        pData.position.y = player.transform.position.y;
        pData.position.z = player.transform.position.z;
        pData.type.type = PlayerPrefs.GetInt("SelectedCharacter", 0);
        guardado.GuardarMensaje("Player", pData);

        sData.sceneName = _currentScene.name;
        guardado.GuardarMensaje("Escena", sData);
        Debug.Log("Guardando partida...");
    }
    public void Options(InputAction.CallbackContext callback)
    {
        if (canvas.active)
        {
            canvas.SetActive(false);
            paused = false;
            
                player.GetComponent<PlayerInput>().enabled = true;
            
        }
        else
        {
            canvas.SetActive(true);
            paused = true;
            player.GetComponent<PlayerInput>().enabled = false;
        }

    }
}