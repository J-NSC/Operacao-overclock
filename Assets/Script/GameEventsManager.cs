using System;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }

    public MiscEvents miscEvents;
    public QuestEvents questEvents;
    public InputEvent InputEvent;
    public DialogQuestEvents dialogQuestEvents;
    public CaculateRoomEvents caculateRoomEvents;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        }
        instance = this;

        // initialize all events
        miscEvents = new MiscEvents();
        questEvents = new QuestEvents();
        InputEvent = new InputEvent();
        dialogQuestEvents = new DialogQuestEvents();
        caculateRoomEvents = new CaculateRoomEvents();
    }
}
