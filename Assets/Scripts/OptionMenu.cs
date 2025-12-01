using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsMenu; // El panel del menú de opciones
    public GameObject exitConfirmationPanel; // El panel de confirmación de salida
    //public Slider sliderGeneral;
    //public Slider sliderMusic;
    [SerializeField] private Slider volumenSlider; // Slider para volumen general
    [SerializeField] private Slider musicaSlider; // Slider para música
    private void Start()
    {
        // Cargar valores guardados
        PlayerPrefs.SetFloat("Sound", volumenSlider.value);
        FMODManager.instance.SetSFXVolume(PlayerPrefs.GetFloat("Sound"));
        PlayerPrefs.SetFloat("Music", musicaSlider.value);
        FMODManager.instance.SetMusicVolume(PlayerPrefs.GetFloat("Music"));

        // Asignar los valores al AudioListener
        volumenSlider.onValueChanged.AddListener(ChangeSFXVolume);
        musicaSlider.onValueChanged.AddListener(ChangeMusicVolume);
    }

    public void OpenOptions()
    {
        optionsMenu.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsMenu.SetActive(false);
    }

    public void ChangeSFXVolume(float value)
    {
        FMODManager.instance.SetSFXVolume(value);
        PlayerPrefs.SetFloat("Sound", value); // Guardar configuración
    }

    public void ChangeMusicVolume(float value)
    {
        FMODManager.instance.SetMusicVolume(value);
        PlayerPrefs.SetFloat("Music", value); // Guardar configuración
    }

    public void ShowExitConfirmation()
    {
        Debug.Log("saletete");
        exitConfirmationPanel.SetActive(true);
    }

    public void HideExitConfirmation()
    {
        exitConfirmationPanel.SetActive(false);
    }

    public void LoadGame()
    {
        Debug.Log("Cargando partida guardada...");
        // Aquí puedes agregar la lógica para cargar datos guardados
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame(int val)
    {
        Debug.Log("Saliendo del juego...");
        if(val == 1)
        {
            GameManager.Instance.GuardarPartida();
        }
        Application.Quit();
    }
}
