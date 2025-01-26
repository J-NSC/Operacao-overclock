using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class QuestPoint : MonoBehaviour
{
    [Header("Quest")] 
    [SerializeField] QuestInfoSO questInfoForPoint;
    
    
    bool playerIsNear = false;
    string questId;
    QuestState currentQuestState;

    void Awake()
    {
        questId = questInfoForPoint.Id;
    }

    void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
        
    }
    
    void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    void Update()
    {
        if (playerIsNear)
        {
            GameEventsManager.instance.questEvents.StartQuest(questId);
            GameEventsManager.instance.questEvents.AdvanceQuest(questId);
            GameEventsManager.instance.questEvents.FinishQuest(questId);
        }
    }

    void QuestStateChange(Quest quest)
    {
        if (quest.info.Id.Equals(questId))
        {
            currentQuestState = quest.state;
            Debug.Log("Quest with id:" + questId + " updated to state" + currentQuestState);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }
}
