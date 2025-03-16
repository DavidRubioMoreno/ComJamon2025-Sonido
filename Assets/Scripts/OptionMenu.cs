using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsMenu; // El panel del menú de opciones
    public GameObject exitConfirmationPanel; // El panel de confirmación de salida
    //public Slider sliderGeneral;
    //public Slider sliderMusic;
    [SerializeField] private AudioMixer audioMixer; // Mixer principal
    [SerializeField] private Slider volumenSlider; // Slider para volumen general
    [SerializeField] private Slider musicaSlider; // Slider para música
    private void Start()
    {
        // Cargar valores guardados
        volumenSlider.value = PlayerPrefs.GetFloat("Music", 1f);
        musicaSlider.value = PlayerPrefs.GetFloat("SFX", 1f);
        //ChangeGeneralVolume(volumenSlider.value);
        //ChangeMusicVolume(musicaSlider.value);
        // Asignar los valores al AudioListener
        volumenSlider.onValueChanged.AddListener(ChangeGeneralVolume);
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

    public void ChangeGeneralVolume(float value)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(value) * 20); // Ajustar volumen
        PlayerPrefs.SetFloat("Music", value); // Guardar configuración
    }

    public void ChangeMusicVolume(float value)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(value) * 20); // Ajustar música
        PlayerPrefs.SetFloat("SFX", value); // Guardar configuración
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
