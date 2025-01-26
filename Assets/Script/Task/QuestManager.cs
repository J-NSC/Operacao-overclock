using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    Dictionary<string, Quest> questsMap;


    void Awake()
    {
        questsMap = CreateQuestMap();
    }

    void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest += AdvanceQuest;
    }

    void OnDisable()
    {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest -= AdvanceQuest;
    }

    void Start()
    {
        foreach (var quest in questsMap.Values)
        {
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }
    }

    void StartQuest(string id)
    {
        Debug.Log("Start Quest" + id);
    }

    void AdvanceQuest(string id)
    {
        Debug.Log("Advance Quest" + id);
    }

    void FinishQuest(string id)
    {
        Debug.Log("Finish Quest" + id);
    }

    Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");
        
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();

        foreach (QuestInfoSO questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.Id))
            {
                Debug.LogWarning("Duplicate Id foind when creating quest map" + questInfo.Id);
            }
            
            idToQuestMap.Add(questInfo.Id, new Quest(questInfo));
        }
        
        return idToQuestMap;
    }

    private Quest GetQuestById(string id)
    {
        Quest quest = questsMap[id];

        if (quest == null)
        {
            Debug.LogError("ID not found in the quest map" + id);
        }

        return quest;
    }
}
