using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class QuestPoint : MonoBehaviour
{
    [Header("Dialogue (optional)")]
    [SerializeField] string dialogueKnotName;

    [Header("Quest")]
    [SerializeField] QuestInfoSO questInfoForPoint;

    [Header("Config")]
    [SerializeField] bool startPoint = true;
    [SerializeField] bool finishPoint = true;

    bool playerIsNear = false;
    string questId;
    QuestState currentQuestState;

    QuestIcon questIcon;

    void Awake() 
    {
        questId = questInfoForPoint.id;

        questIcon = GetComponentInChildren<QuestIcon>();
        
    }

    void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
        // GameEventsManager.instance.inputEvents.onSubmitPressed += SubmitPressed;
    }

    void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
        // GameEventsManager.instance.inputEvents.onSubmitPressed -= SubmitPressed;
    }

    void Update()
    {
        if (playerIsNear)
        {
            if (currentQuestState.Equals(QuestState.CAN_START) && startPoint)
            {
                // GameEventsManager.instance.questEvents.StartQuest(questId);
                GameEventsManager.instance.dialogQuestEvents.ShowDialogedQuest(questId);
            }
            // else if (currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint)
            // {
            //     GameEventsManager.instance.questEvents.FinishQuest(questId);
            // }
        }

       
    }

    void QuestStateChange(Quest quest)
    {
        if (quest.state == QuestState.IN_PROGRESS)
        {
            Destroy(this.gameObject);
        }
        
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
        }

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;
            GameEventsManager.instance.dialogQuestEvents.ShowTextQuest(questInfoForPoint);
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }
}
