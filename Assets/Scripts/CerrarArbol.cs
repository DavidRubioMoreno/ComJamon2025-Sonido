using UnityEngine;

public class CerrarArbol : MonoBehaviour
{
    public GameObject arbolHabilidadesUI;

    public void Cerrar()
    {
        arbolHabilidadesUI.SetActive(false);
    }
    public void Abrir()
    {
        arbolHabilidadesUI.SetActive(true);
    }
}
