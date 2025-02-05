using UnityEngine;
using System;

public class DialogQuestEvents
{
    public event Action<string> onShowDialogedQuest;
    public event Action<QuestInfoSO> onShowTextQuest;
    
    public void ShowDialogedQuest(string quest)
    {
        onShowDialogedQuest?.Invoke(quest);
    }


    public void ShowTextQuest(QuestInfoSO quest)
    {
        onShowTextQuest?.Invoke(quest);
    }
}
