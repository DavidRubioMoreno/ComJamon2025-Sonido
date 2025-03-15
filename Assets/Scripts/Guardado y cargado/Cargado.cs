using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;


[System.Serializable]
public class DataType
{
    public string id;
    public string data;
}

[System.Serializable]
public class MessageData
{
    public List<DataType> dataTypes;
}

public class Cargado : MonoBehaviour
{
    public MessageData mData;

    private void Start()
    {
        // Cargar el archivo JSON desde Resources
        TextAsset jsonTextFile = Resources.Load<TextAsset>("DatosPartida");
        

        if (jsonTextFile != null)
        {
            mData = JsonUtility.FromJson<MessageData>(jsonTextFile.text);
        }
        else
        {
            Debug.Log("No hay partida guardada.");
        }
    }

    // Debug
    public void MostrarMensajes() 
    {
        if (mData != null && mData.dataTypes != null)
        {
            foreach (DataType message in mData.dataTypes)
            {
                Debug.Log(message.id + " " + message.data);
            }
        }
        else
        {
            Debug.Log("No hay datos para mostrar.");
        }

    }
    public string GetMensajes(string id)
    {
        string[] aux = null;
        if (mData != null && mData.dataTypes != null)
        {
            foreach (DataType message in mData.dataTypes)
            {
                if (message.id == id)
                {
                    return message.data;
                }
            }
        }
        else
        {
            Debug.Log("No hay mensajes para mostrar.");
        }
        return null;

    }
}
