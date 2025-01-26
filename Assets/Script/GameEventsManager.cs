using System;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance;
    
    
    // public InputEvents inputEvents;
    public MiscEvents miscEvents;
    public QuestEvents questEvents;


    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one game Events Manager in the scene");
        }
        
        instance = this;

        miscEvents = new MiscEvents();
        questEvents = new QuestEvents();
    }
}
