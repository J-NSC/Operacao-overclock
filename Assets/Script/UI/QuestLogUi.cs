using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class QuestLogUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject contentParent;
    [SerializeField] QuestLogScrollingList scrollingList;
    // [SerializeField] TextMeshProUGUI questDisplayNameText;
    // [SerializeField] TextMeshProUGUI questStatusText;
    // [SerializeField] TextMeshProUGUI goldRewardsText;
    // [SerializeField] TextMeshProUGUI experienceRewardsText;
    // [SerializeField] TextMeshProUGUI levelRequirementsText;
    // [SerializeField] TextMeshProUGUI questRequirementsText;

    Button firstSelectedButton;

    void OnEnable()
    {
        // GameEventsManager.instance.inputEvents.onQuestLogTogglePressed += QuestLogTogglePressed;
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    void OnDisable()
    {
        // GameEventsManager.instance.inputEvents.onQuestLogTogglePressed -= QuestLogTogglePressed;
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    void QuestLogTogglePressed()
    {
        if (contentParent.activeInHierarchy)
        {
            HideUI();
        }
        else
        {
            ShowUI();
        }
    }

    void ShowUI()
    {
        contentParent.SetActive(true);
        // GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        // note - this needs to happen after the content parent is set active,
        // or else the onSelectAction won't work as expected
        if (firstSelectedButton != null)
        {
            firstSelectedButton.Select();
        }
    }

    void HideUI()
    {
        contentParent.SetActive(false);
        // GameEventsManager.instance.playerEvents.EnablePlayerMovement();
        EventSystem.current.SetSelectedGameObject(null);
    }

   void QuestStateChange(Quest quest)
{
    if (quest.state == QuestState.IN_PROGRESS)
    {
        QuestLogButton questLogButton = scrollingList.CreateButtonIfNotExists(quest, () => {
            SetQuestLogInfo(quest);
        });

        if (firstSelectedButton == null)
        {
            firstSelectedButton = questLogButton.button;
        }

        questLogButton.SetState(quest.state);
    }
}


    void SetQuestLogInfo(Quest quest)
    {
    }
}
