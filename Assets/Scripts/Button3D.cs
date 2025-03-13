using UnityEngine;
using UnityEngine.SceneManagement;

public class Button3D : MonoBehaviour
{
    public string action; // Define qué acción realizará el botón
    private Renderer rend;
    private Color originalColor;
    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    void OnMouseEnter()
    {
        rend.material.color = new Color(1f, 1f, 0.6f); // Cambia el color al pasar el mouse
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
                SceneManager.LoadScene("Mike"); // Cargar la escena del juego
                break;
            case "Load":
                Debug.Log("Cargar partida guardada...");
                // Aquí cargarías datos de una partida guardada
                break;
            case "Options":
                Debug.Log("Abrir menú de opciones...");
                // Aquí podrías mostrar un panel de opciones
                break;
            case "Exit":
                Application.Quit(); // Salir del juego
                break;
        }
    }
}

