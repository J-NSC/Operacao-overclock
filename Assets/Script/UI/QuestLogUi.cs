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
        // add the button to the scrolling list if not already added
        QuestLogButton questLogButton = scrollingList.CreateButtonIfNotExists(quest, () => {
            SetQuestLogInfo(quest);
        });

        // initialize the first selected button if not already so that it's
        // always the top button
        if (firstSelectedButton == null)
        {
            firstSelectedButton = questLogButton.button;
        }

        // set the button color based on quest state
        questLogButton.SetState(quest.state);
    }

    void SetQuestLogInfo(Quest quest)
    {
        // quest name
        // questDisplayNameText.text = quest.info.displayName;
        //
        // // status
        // questStatusText.text = quest.GetFullStatusText();
        //
        // // requirements
        // levelRequirementsText.text = "Level " + quest.info.levelRequirement;
        // questRequirementsText.text = "";
        // foreach (QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites)
        // {
        //     questRequirementsText.text += prerequisiteQuestInfo.displayName + "\n";
        // }
        //
        // // rewards
        // goldRewardsText.text = quest.info.goldReward + " Gold";
        // experienceRewardsText.text = quest.info.experienceReward + " XP";
    }
}
