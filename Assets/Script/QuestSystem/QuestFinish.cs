using System;
using UnityEngine;

public class QuestFinish : MonoBehaviour
{

    bool playerIsNear = false;
    QuestState currentQuestState;
    string questId;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            //Todo mudar para a mesa 
            GameEventsManager.instance.miscEvents.ValidedTask();

            QuestManager questManager = FindObjectOfType<QuestManager>();
            
            if (questManager != null)
            {
                Quest questToFinish = null;

                foreach (Quest quest in questManager.GetAllQuests())
                {
                    if (quest.state == QuestState.CAN_FINISH)
                    {
                        GameEventsManager.instance.questEvents.FinishQuest(quest.info.id);
                    }
                }
            }
        }
    }
}
