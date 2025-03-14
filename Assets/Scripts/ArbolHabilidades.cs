using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Asegúrate de importar el espacio de nombres de TMPro

public class ArbolHabilidades : MonoBehaviour
{
    public int puntosHabilidad = 3;
    public GameObject botonPrefab; // Prefab del botón
    public Transform panelEspada, panelEscudo, panelMovilidad; // Paneles de cada rama

    private Dictionary<string, Habilidad> habilidades = new Dictionary<string, Habilidad>();
    private Dictionary<string, Transform> paneles = new Dictionary<string, Transform>();
    private Dictionary<string, GameObject> botones = new Dictionary<string, GameObject>(); // Diccionario de botones

    void Start()
    {
        // Asociar ramas con los paneles del UI
        paneles["Espada"] = panelEspada;
        paneles["Escudo"] = panelEscudo;
        paneles["Movilidad"] = panelMovilidad;

        CrearHabilidades();
        MostrarHabilidades();
    }

    void CrearHabilidades()
    {
        // Habilidades principales (DESBLOQUEADAS DESDE EL INICIO)
        var espada = new Habilidad("Espada", 0) { desbloqueada = true };
        var escudo = new Habilidad("Escudo", 0) { desbloqueada = true };
        var movilidad = new Habilidad("Movilidad", 0) { desbloqueada = true };

        // Sub-habilidades
        var dañoEspada = new Habilidad("Más daño", 1);
        var rangoEspada = new Habilidad("Más rango", 1);
        var giroEspada = new Habilidad("Ataque giratorio", 1);

        var bloquearMásTiempo = new Habilidad("Más tiempo de bloqueo", 1);
        var velocidadBloqueo = new Habilidad("Velocidad bloqueando", 1);
        var dobleBloqueo = new Habilidad("Doble tiempo bloqueando", 1);

        var velocidadMov = new Habilidad("Más velocidad", 1);
        var dash = new Habilidad("Dash", 1);

        // Asignar requisitos
        rangoEspada.requisitos.Add(espada);
        dañoEspada.requisitos.Add(espada);
        giroEspada.requisitos.Add(rangoEspada);

        bloquearMásTiempo.requisitos.Add(escudo);
        velocidadBloqueo.requisitos.Add(escudo);
        dobleBloqueo.requisitos.Add(velocidadBloqueo);

        velocidadMov.requisitos.Add(movilidad);
        dash.requisitos.Add(velocidadMov);

        // Guardar habilidades
        habilidades["Espada"] = espada;
        habilidades["Escudo"] = escudo;
        habilidades["Movilidad"] = movilidad;
        habilidades["Más daño"] = dañoEspada;
        habilidades["Más rango"] = rangoEspada;
        habilidades["Ataque giratorio"] = giroEspada;
        habilidades["Más tiempo de bloqueo"] = bloquearMásTiempo;
        habilidades["Velocidad bloqueando"] = velocidadBloqueo;
        habilidades["Doble tiempo bloqueando"] = dobleBloqueo;
        habilidades["Más velocidad"] = velocidadMov;
        habilidades["Dash"] = dash;
    }

    void MostrarHabilidades()
    {
        foreach (var habilidad in habilidades.Values)
        {
            GameObject btn = CrearBotonHabilidad(habilidad);
            if (btn != null)
            {
                // Asignar el botón al panel correcto
                Transform panelDestino = ObtenerPanelPorHabilidad(habilidad.nombre);
                if (panelDestino != null)
                {
                    btn.transform.SetParent(panelDestino, false);
                    botones[habilidad.nombre] = btn; // Guardar el botón en el diccionario
                }
            }
        }
    }

    GameObject CrearBotonHabilidad(Habilidad habilidad)
    {
        if (botonPrefab == null)
        {
            Debug.LogError("? ERROR: No se ha asignado el Prefab del botón.");
            return null;
        }

        GameObject btn = Instantiate(botonPrefab);
        btn.name = habilidad.nombre;

        // Buscar el componente TextMeshProUGUI en lugar de Text
        TMP_Text textoBoton = btn.GetComponentInChildren<TMP_Text>();
        if (textoBoton != null)
            textoBoton.text = habilidad.nombre;
        else
            Debug.LogError($"? ERROR: El Prefab {botonPrefab.name} no tiene un componente TMP_Text.");

        Button boton = btn.GetComponent<Button>();
        if (boton != null)
        {
            // Establecer el estado de interactuar según si la habilidad está desbloqueada o no
            boton.interactable = habilidad.EsDesbloqueable();  // Solo interactuable si puede desbloquearse.

            // Cambiar el color del botón según su estado (bloqueado/desbloqueado)
            CambiarColorBoton(boton, habilidad);

            // Si el botón es desbloqueado, no permitir que se haga clic en él.
            if (habilidad.desbloqueada)
            {
                boton.interactable = false; // Deshabilitar el botón
            }

            boton.onClick.AddListener(() => DesbloquearHabilidad(habilidad));
        }
        else
            Debug.LogError($"? ERROR: El Prefab {botonPrefab.name} no tiene un componente Button.");

        return btn;
    }

    Transform ObtenerPanelPorHabilidad(string nombre)
    {
        if (nombre == "Espada" || nombre == "Más daño" || nombre == "Más rango" || nombre == "Ataque giratorio")
            return panelEspada;
        if (nombre == "Escudo" || nombre == "Más tiempo de bloqueo" || nombre == "Velocidad bloqueando" || nombre == "Doble tiempo bloqueando")
            return panelEscudo;
        if (nombre == "Movilidad" || nombre == "Más velocidad" || nombre == "Dash")
            return panelMovilidad;

        return null;
    }

    void DesbloquearHabilidad(Habilidad habilidad)
    {
        if (puntosHabilidad >= habilidad.costo && habilidad.EsDesbloqueable() && !habilidad.desbloqueada)
        {
            habilidad.desbloqueada = true;
            puntosHabilidad -= habilidad.costo;
            Debug.Log($"Desbloqueada: {habilidad.nombre}");
            ActualizarHabilidades();
        }
    }

    void ActualizarHabilidades()
    {
        foreach (var habilidad in habilidades.Values)
        {
            if (botones.ContainsKey(habilidad.nombre)) // Verificar si el botón existe
            {
                GameObject btn = botones[habilidad.nombre];
                Button boton = btn.GetComponent<Button>();
                if (boton != null)
                {
                    // Si la habilidad ya está desbloqueada, deshabilitar el botón
                    boton.interactable = habilidad.EsDesbloqueable() && !habilidad.desbloqueada;

                    // Cambiar el color del botón si la habilidad ha sido desbloqueada
                    CambiarColorBoton(boton, habilidad);
                }
            }
        }
    }

    // Método para cambiar el color del botón según el estado de la habilidad
    void CambiarColorBoton(Button boton, Habilidad habilidad)
    {
        Image imagenBoton = boton.GetComponent<Image>();
        TMP_Text textoBoton = boton.GetComponentInChildren<TMP_Text>(); // Usar TMP_Text

        if (habilidad.desbloqueada)
        {
            // Si está desbloqueada, cambiar a verde
            if (imagenBoton != null)
                imagenBoton.color = Color.green;

            if (textoBoton != null)
                textoBoton.color = Color.white;
        }
        else
        {
            // Si está bloqueada, cambiar a gris
            if (imagenBoton != null)
                imagenBoton.color = Color.gray;

            if (textoBoton != null)
                textoBoton.color = Color.white;
        }
    }
}
