
using UnityEngine;

public class pruebas : MonoBehaviour
{
    public GameObject[] characters; // Array con los modelos de los personajes

    private void Start()
    {
        int selectedIndex = PlayerPrefs.GetInt("SelectedCharacter", 0); // Carga el personaje elegido
        Instantiate(characters[selectedIndex], Vector3.up, Quaternion.identity);
    }
}

