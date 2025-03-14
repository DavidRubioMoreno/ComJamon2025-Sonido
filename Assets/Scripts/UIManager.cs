using System.Collections;
using System.Collections.Generic;
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
    private float maxLife = 50;
    [SerializeField]
    private float health = 50;

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

    private void Update()
    {
        UpdateHealthBar(health, maxLife);
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



}