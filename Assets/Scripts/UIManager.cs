using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField]
    private Slider healthBar; // Referencia al Slider de la barra de vida
    [SerializeField]
    private Image healthFill; // Referencia a la parte de relleno de la barra (opcional)
    [SerializeField]
    private Gradient healthGradient; // Color dinámico de la barra
    [SerializeField]
    private GameObject damageImage;

    [SerializeField]
    private float maxLife = 50;
    [SerializeField]
    private float health = 50;

    private float elapsed = 0;
    private float timeDamage = 0.5f;

    private bool active = false;

    public Texture texRamaNegra;
    public Texture texRamaNormal;

    [SerializeField]
    private RawImage[] branchImgs; 

    int branchesShown = 0; 


    private void Awake()
    {
        // Implementación del Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); // Mantiene el UIManager entre escenas
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void Hit()
    {
        active = true;
        damageImage.SetActive(true);
        elapsed = 0;
    }

    void Update()
    {
        if(GameManager.Instance.Player)
        UpdateHealthBar(GameManager.Instance.Player.GetComponent<LifeComponent>().vida, GameManager.Instance.Player.GetComponent<LifeComponent>().maxVida);
        UpdateBranches(GameManager.Instance.branchesCollected);
        if(active)
        {
            elapsed += Time.deltaTime;
            if(elapsed > timeDamage)
            {
                active = false;
                damageImage.SetActive(false);
            }
        }

        if (GameManager.Instance.Pause)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    // Actualiza la barra de vida
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if (healthBar == null || maxHealth <= 0) return; // Evita división por cero

        float healthPercentage = Mathf.Clamp01(currentHealth / maxHealth); // Asegura que esté entre 0 y 1
        healthBar.value = healthPercentage;

        // Cambiar color dinámicamente
        if (healthFill != null && healthGradient != null)
        {
            healthFill.color = healthGradient.Evaluate(healthPercentage);
        }
    }

    public void UpdateBranches(int num)
    {
        if(num > branchesShown)
        {
            for(int i = 0; i< num; i++)
            {
                branchImgs[i].texture = texRamaNormal;
            }

            branchesShown = num;
        }

    }

}