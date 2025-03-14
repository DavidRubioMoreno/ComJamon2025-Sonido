using UnityEngine;
using UnityEngine.InputSystem;

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
    public void AbrirCerrar(InputAction.CallbackContext callback)
    {
        Debug.Log("e");
        if (arbolHabilidadesUI.active)
        {
            Cerrar();
        }
        else
        {
            Abrir();
        }
    }
}
