using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Guardado : MonoBehaviour
{
    private string filePath;
    public MessageData mData = new MessageData();

    private void Awake()
    {
        // Ruta dentro del proyecto: Assets/Resources
        string directoryPath = Path.Combine(Application.dataPath, "Resources");
        filePath = Path.Combine(directoryPath, "DatosPartida.json");

        // Crear la carpeta Resources si no existe
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }

    public void GuardarMensaje(string id, Data Data)
    {
        if (mData.dataTypes == null)
        {
            mData.dataTypes = new List<DataType>();
        }

        string playerJson = JsonUtility.ToJson(Data);

        bool found = false;

        foreach (var item in mData.dataTypes)
        {
            if (item.id == id)
            {
                item.data = playerJson;
                found = true;
                break;
            }
        }

        if (!found)
        {
            mData.dataTypes.Add(new DataType { id = id, data = playerJson });
        }

        GuardarArchivo();
    }

    private void GuardarArchivo()
    {
        string json = JsonUtility.ToJson(mData, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Datos guardados en: " + filePath);
    }

    public void Guarda()
    {
        GameManager.Instance.GuardarPartida();
    }
}
