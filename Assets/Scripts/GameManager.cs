using FMODUnity;
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

    private float value = 0;

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

    public StudioEventEmitter musicEmitter;

    bool musicPaused = false;


    //[SerializeField]
    //FMODUnity.EventReference ambientMusic;   // Seleccionado desde el inspector

    //private FMOD.Studio.EventInstance ambientEventInstance;

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
    [SerializeField]
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

    private void OnDestroy()
    {
        //FMODManager.instance.StopEvent(ambientEventInstance);
    }


    private void Start()
    {

        //branchGenerator.enabled = false;
        cargado = GetComponent<Cargado>();
        guardado = GetComponent<Guardado>();
        _currentScene = SceneManager.GetActiveScene();


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
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene(_currentScene.name);
        }

        //Atajo coger rama
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            addBranch();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("mazmorra4");
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

        if (WaveManager.Instance && WaveManager.Instance.EnemiesAlive > 0 && player)
        {
            value = Mathf.Min(value + WaveManager.Instance.EnemiesAlive * Time.deltaTime * 0.3f, 1); 
        }
        else
        {
            value = Mathf.Max(0, value - Time.deltaTime * 0.2f);           
        }

        musicEmitter.SetParameter("Combat", value);

        //if (WaveManager.Instance && !WaveManager.Instance.Final && ambientEventInstance.isValid())
        //{
        //    ambientEventInstance.setParameterByName("Combat", value);
        //}
        //else if(ambientEventInstance.isValid())
        //{
        //    ambientEventInstance.setParameterByName("Combat", 0);
        //}






        if (UIManager.Instance && player)
        {
            UIManager.Instance.UpdateHealthBar(player.GetComponent<LifeComponent>().vida, player.GetComponent<LifeComponent>().maxVida);
        }
        
    }

    private void OnLevelWasLoaded(int level)
    {

        if (level == 5 ||level == 2)
            cinematica = false;
        nocreado = false;
    }

    public void createPlayer()
    {
        //camara = Camera.main;
        
        nocreado = true;
        int selectedIndex = PlayerPrefs.GetInt("SelectedCharacter", 0); // Carga el personaje elegido
        player = Instantiate(characters[selectedIndex], SpawnPoint.Instance.Position.position, SpawnPoint.Instance.Position.rotation);
        player.GetComponent<PlayerMovement>()._mainCamera = Camera.main;
        Camera.main.GetComponent<ThirdPersonCamera>().target = player.transform;
        
        
    }
    private void updateState(State currentState)
    {
        if (currentState == State.DANCING)
        {
            //Debug.Log("Bailando");

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


        if (SceneManager.GetActiveScene().name == "Mazmorra4" && !musicPaused)
        {
            musicEmitter.EventInstance.setPaused(true);
            musicPaused = true;
        }
        else if (musicPaused && WaveManager.Instance && SceneManager.GetActiveScene().name != "Mazmorra4")
        {
            musicPaused = false;
            musicEmitter.EventInstance.setPaused(false);
        }
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
        if (canvas.activeSelf)
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