using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuestLogUi : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject contentParent;
    [SerializeField] QuestLogScrollingList scrollingList;
    // [SerializeField] TextMeshProUGUI questDisplayNameText;
    // [SerializeField] TextMeshProUGUI questStatusText;
    // [SerializeField] TextMeshProUGUI questRequirementsText;

    Button firstSelectedButton;
    void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChanged;
    }

    void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChanged;
    }

    void QuestStateChanged(Quest quest)
    {

        QuestLogButton questLogButton = scrollingList.CreateButtonIfNotExist(quest, () =>
        {
            SetQuestLogInfo(quest);
        });

        if (firstSelectedButton == null)
        {
            firstSelectedButton = questLogButton.button;
            firstSelectedButton.Select();
        }
        
        questLogButton.SetState(quest.state);
    }


    private void SetQuestLogInfo(Quest quest)
    {
        // questDisplayNameText.text = quest.info.displayName;
        //
        // questRequirementsText.text = "";
        //
        // foreach (var prerequisite in quest.info.questPrerequisites)
        // {
        //     questRequirementsText.text = prerequisite.displayName + "\n";
        // }
    }
    
}
