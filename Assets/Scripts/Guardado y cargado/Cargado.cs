
using System.Collections.Generic;

using UnityEngine;



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

[System.Serializable]
public class Position
{
    public float x;
    public float y;
    public float z;
}

[System.Serializable]
public class Rotation
{
    public float x;
    public float y;
    public float z;
    public float w;
}

[System.Serializable]
public class Type
{
    public int type;
}

[System.Serializable]
public class Data
{

}

[System.Serializable]
public class PlayerData : Data
{
    public Position position = new Position();
    public Rotation rotation = new Rotation();
    public Type type = new Type();
}

[System.Serializable]
public class SceneData : Data
{
    public string sceneName;
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

            GameManager.Instance.sData = GetSceneData();
            GameManager.Instance.pData = GetPlayerData();
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

    public PlayerData GetPlayerData()
    {
        if (mData != null && mData.dataTypes != null)
        {
            foreach (DataType message in mData.dataTypes)
            {
                if (message.id == "Player")
                {
                    return JsonUtility.FromJson<PlayerData>(message.data);
                }
            }
        }
        Debug.LogWarning("No se encontraron datos del jugador.");
        return new PlayerData();
    }

    public SceneData GetSceneData()
    {
        if (mData != null && mData.dataTypes != null)
        {
            foreach (DataType message in mData.dataTypes)
            {
                if (message.id == "Escena")
                {
                    return JsonUtility.FromJson<SceneData>(message.data);
                }
            }
        }
        Debug.LogWarning("No se encontraron datos de la escena.");
        return new SceneData();
    }
}

