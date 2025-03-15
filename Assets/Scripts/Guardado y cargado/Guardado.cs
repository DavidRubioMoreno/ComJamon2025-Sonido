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
        filePath = Path.Combine(Application.persistentDataPath, "DatosPartida.json");
    }

    public void GuardarMensaje(string id, PlayerData playerData)
    {
        if (mData.dataTypes == null)
        {
            mData.dataTypes = new List<DataType>();
        }

        string playerJson = JsonUtility.ToJson(playerData);

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
}
