using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Habilidad
{
    public string nombre;
    public int costo;
    public bool desbloqueada;
    public List<Habilidad> requisitos; // Habilidades necesarias antes de desbloquear esta

    public Habilidad(string nombre, int costo)
    {
        this.nombre = nombre;
        this.costo = costo;
        this.desbloqueada = false;
        this.requisitos = new List<Habilidad>();
    }

    // Método para comprobar si se puede desbloquear
    public bool EsDesbloqueable()
    {
        foreach (var req in requisitos)
        {
            if (!req.desbloqueada)
                return false;
        }
        return true;
    }
}
