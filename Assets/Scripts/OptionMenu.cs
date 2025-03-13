using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsMenu; // El panel del menú de opciones
    public GameObject exitConfirmationPanel; // El panel de confirmación de salida
    public Slider sliderGeneral;
    public Slider sliderMusic;

    private void Start()
    {
        // Cargar valores guardados
        sliderGeneral.value = PlayerPrefs.GetFloat("GeneralVolume", 1f);
        sliderMusic.value = PlayerPrefs.GetFloat("MusicVolume", 1f);

        // Asignar los valores al AudioListener
        sliderGeneral.onValueChanged.AddListener(ChangeGeneralVolume);
        sliderMusic.onValueChanged.AddListener(ChangeMusicVolume);
    }

    public void OpenOptions()
    {
        optionsMenu.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsMenu.SetActive(false);
    }

    public void ChangeGeneralVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("GeneralVolume", value);
    }

    public void ChangeMusicVolume(float value)
    {
        // Suponiendo que tienes un objeto con AudioSource para la música
        GameObject.Find("MusicPlayer").GetComponent<AudioSource>().volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void ShowExitConfirmation()
    {
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

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
