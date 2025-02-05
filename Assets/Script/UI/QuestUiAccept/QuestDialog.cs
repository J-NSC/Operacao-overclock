using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.QuestUiAccept
{
    public class QuestDialog : MonoBehaviour
    {
        [Header("Ui Elements")]
        [SerializeField] Button acceptQuestButton;
        [SerializeField] Button refuseQuestButton;
        [SerializeField] TextMeshProUGUI displayQuest; 
        [SerializeField] TextMeshProUGUI titleQuest; 


        [SerializeField] GameObject questDialogPanel;
        [SerializeField] QuestManager questManager;
        
        string questId;
        void Awake()
        {
            acceptQuestButton.onClick.AddListener(AcceptQuest);
            refuseQuestButton.onClick.AddListener(RefuseQuest);
        }


        void OnEnable()
        {
            GameEventsManager.instance.dialogQuestEvents.onShowDialogedQuest += ShowDialogQuest; 
            GameEventsManager.instance.dialogQuestEvents.onShowTextQuest += ShowTextQuest; 
        }
       

        void OnDisable()
        {
            GameEventsManager.instance.dialogQuestEvents.onShowDialogedQuest -= ShowDialogQuest;
            GameEventsManager.instance.dialogQuestEvents.onShowTextQuest -= ShowTextQuest;
        }
        void ShowTextQuest(QuestInfoSO obj)
        {
            titleQuest.text = obj.title;
            displayQuest.text = obj.displayName;
        }

        void ShowDialogQuest(string questId)
        {
            questDialogPanel.SetActive(true);
            this.questId = questId;
        }

        void AcceptQuest()
        {
            GameEventsManager.instance.questEvents.StartQuest(questId);
            GameEventsManager.instance.questEvents.QuestAccepted(questId);
            gameObject.SetActive(false);
        }

        void RefuseQuest()
        {
            questDialogPanel.SetActive(false);
        }
    }
}
