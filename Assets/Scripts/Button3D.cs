using UnityEngine;
using UnityEngine.SceneManagement;

public class Button3D : MonoBehaviour
{
    public string action; // Define qué acción realizará el botón
    private Renderer rend;
    private Color originalColor;
    public GameObject optionsMenu;
    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    void OnMouseEnter()
    {
        if (!GameManager.Instance.Pause)
        {
            rend.material.color = new Color(1f, 1f, 0.6f); // Cambia el color al pasar el mouse
            FMODManager.instance.PlayButtonHover();
        }      
    }

    void OnMouseExit()
    {
        rend.material.color = originalColor; // Vuelve al color original
    }
    private void OnMouseDown()
    {
        if (!GameManager.Instance.Pause)
        {
            FMODManager.instance.PlayButtonPress();
            switch (action)
            {
                case "Play":
                    SceneManager.LoadScene("ElegirClase"); // Cargar la escena del juego
                    break;
                case "Load":
                    // Aquí cargarías datos de una partida guardada
                    GameManager.Instance.CargarPartida();
                    break;
                case "Options":
                    Debug.Log("Abrir menú de opciones...");
                    optionsMenu.SetActive(true);
                    // Aquí podrías mostrar un panel de opciones
                    break;
                case "Exit":
                    Application.Quit(); // Salir del juego
                    break;
            }
        }
        
    }
}

