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
        rend.material.color = new Color(1f, 1f, 0.6f); // Cambia el color al pasar el mouse
        switch (action)
        {
            case "Play":
                //SoundManager.Instance.PlaySound(SoundManager.Instance.Jugarnueva, 3);
                break;
            case "Load":
                //SoundManager.Instance.PlaySound(SoundManager.Instance.jugarcargada, 3);
                break;
            case "Options":
                //SoundManager.Instance.PlaySound(SoundManager.Instance.opciones, 3);
                break;
            case "Exit":
                //SoundManager.Instance.PlaySound(SoundManager.Instance.salir, 3);
                break;
        }
    }

    void OnMouseExit()
    {
        rend.material.color = originalColor; // Vuelve al color original
    }
    private void OnMouseDown()
    {
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

