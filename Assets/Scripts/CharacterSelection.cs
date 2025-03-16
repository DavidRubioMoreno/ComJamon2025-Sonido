using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    private Animator anim;
    public int characterIndex; // 0 o 1, depende del personaje
    private Renderer rend;
    private Color originalColor;

    private void Start()
    {
        anim = GetComponent<Animator>();

        //rend = GetComponent<Renderer>();
        //originalColor = rend.material.color;
        //BackgroundMusic.Instance.bossfinal();
    }

    private void OnMouseEnter()
    {
        //rend.material.color = new Color(1f, 1f, 0.6f);
        anim.SetBool("Cambio",true); // Activa la animación al pasar el mouse
        if (characterIndex == 0)
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.eligemeC);
        }
        else
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.eligemeM);
        }
    }

    private void OnMouseExit()
    {
        //rend.material.color = originalColor; // Vuelve al color original
        anim.SetBool("Cambio", false);
    }

    private void OnMouseDown()
    {
        PlayerPrefs.SetInt("SelectedCharacter", characterIndex); // Guarda el índice
        SceneManager.LoadScene("PRINCIPAL"); // Carga la nueva escena
        GameManager.Instance.menu = false;
        //GameManager.Instance.createPlayer();
    }
}

