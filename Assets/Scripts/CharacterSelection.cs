using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    private Animator anim;
    public int characterIndex; // 0 o 1, depende del personaje
    private Renderer rend;
    private Color originalColor;
    [SerializeField]
    FMODUnity.EventReference knightEvent;
    [SerializeField]
    FMODUnity.EventReference mageEvent;

    FMOD.Studio.EventInstance knightEventInstance;
    FMOD.Studio.EventInstance mageEventInstance;

    private void Start()
    {
        anim = GetComponent<Animator>();
        knightEventInstance = FMODManager.instance.CreateEventInstance(knightEvent);
        mageEventInstance = FMODManager.instance.CreateEventInstance(mageEvent);
    }

    private void OnMouseEnter()
    {
        if (!GameManager.Instance.Pause)
        {
            anim.SetBool("Cambio", true); // Activa la animación al pasar el mouse
            if (characterIndex == 0)
            {
                knightEventInstance.start();
            }
            else
            {
                mageEventInstance.start();
            }
        }
        
    }

    private void OnMouseExit()
    {
        anim.SetBool("Cambio", false);
    }

    private void OnMouseDown()
    {
        if (!GameManager.Instance.Pause)
        {
            PlayerPrefs.SetInt("SelectedCharacter", characterIndex); // Guarda el índice
            SceneManager.LoadScene("PRINCIPAL"); // Carga la nueva escena
            GameManager.Instance.menu = false;
            if(characterIndex == 0)
            {
                knightEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }else mageEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}

