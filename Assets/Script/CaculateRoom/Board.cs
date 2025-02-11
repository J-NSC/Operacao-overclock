using System;
using UnityEngine;
using TMPro;

[System.Serializable] 
public class SerializableArray
{
    public string[] values; 
}

public class Board : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI display;

    [SerializeField] string[] values = new string[0];

    [SerializeField] GameObject Room;


    void OnEnable()
    {
        GameEventsManager.instance.caculateRoomEvents.ShowedRoomUi += ShowedRoomUi;
    }

    void OnDisable()
    {
        GameEventsManager.instance.caculateRoomEvents.ShowedRoomUi -= ShowedRoomUi;
    }


    void Start()
    {
        display.text = "( )";
        
        Room.SetActive(false);
    }

    public void AddValue(string value)
    {
        Array.Resize(ref values, values.Length + 1);
        values[values.Length - 1] = value;
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        string valuesString = string.Join(", ", values);
        display.text = $"({valuesString})";
    }

    public void SaveOperation() 
    {
        try
        {
            SerializableArray serializableArray = new SerializableArray();
            serializableArray.values = values;

            string serializeOperation = JsonUtility.ToJson(serializableArray);
            
            PlayerPrefs.SetString("operation", serializeOperation);
            PlayerPrefs.Save(); 

            Room.SetActive(false);
            GameEventsManager.instance.InputEvent.IsHudActive(false);

        }
        catch (Exception e)
        {
            Debug.LogError("Erro ao salvar operação: " + e.Message);
        }
    }
    
    void ShowedRoomUi()
    {
        Room.SetActive(true);
        GameEventsManager.instance.InputEvent.IsHudActive(true);
    }
}