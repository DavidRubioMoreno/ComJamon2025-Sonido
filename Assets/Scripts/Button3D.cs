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
                SoundManager.Instance.PlaySound(SoundManager.Instance.Jugarnueva);
                break;
            case "Load":
                SoundManager.Instance.PlaySound(SoundManager.Instance.jugarcargada);
                break;
            case "Options":
                SoundManager.Instance.PlaySound(SoundManager.Instance.opciones);
                break;
            case "Exit":
                SoundManager.Instance.PlaySound(SoundManager.Instance.salir);
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
                Debug.Log("Cargar partida guardada...");
                // Aquí cargarías datos de una partida guardada
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

