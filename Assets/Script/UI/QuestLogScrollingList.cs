using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestLogScrollingList : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject contentParent;

    [Header("Rect Transforms")]
    [SerializeField] RectTransform scrollRectTransform;
    [SerializeField] RectTransform contentRectTransform;

    [Header("Quest Log Button")]
    [SerializeField] GameObject questLogButtonPrefab;

    Dictionary<string, QuestLogButton> idToButtonMap = new Dictionary<string, QuestLogButton>();

    // Below is code to test that the scrolling list is working as expected.
    // For it to work, you'll need to change the QuestInfoSO id field to be publicly settable
    // void Start()
    // {
    //     for (int i = 0; i < 20; i++) 
    //     {
    //         QuestInfoSO questInfoTest = ScriptableObject.CreateInstance<QuestInfoSO>();
    //         questInfoTest.id = "test_" + i;
    //         questInfoTest.displayName = "Test " + i;
    //         questInfoTest.questStepPrefabs = new GameObject[0];
    //         Quest quest = new Quest(questInfoTest);
    //
    //         QuestLogButton questLogButton = CreateButtonIfNotExists(quest, () => {
    //             Debug.Log("SELECTED: " + questInfoTest.displayName);
    //         });
    //
    //         if (i == 0)
    //         {
    //             questLogButton.button.Select();
    //         }
    //     }
    // }

    public QuestLogButton CreateButtonIfNotExists(Quest quest, UnityAction selectAction) 
    {
        QuestLogButton questLogButton = null;
        if (!idToButtonMap.ContainsKey(quest.info.id))
        {
            questLogButton = InstantiateQuestLogButton(quest, selectAction);
        }
        else 
        {
            questLogButton = idToButtonMap[quest.info.id];
        }
        return questLogButton;
    }

    QuestLogButton InstantiateQuestLogButton(Quest quest, UnityAction selectAction)
    {
        QuestLogButton questLogButton = Instantiate(
            questLogButtonPrefab,
            contentParent.transform).GetComponent<QuestLogButton>();
        questLogButton.gameObject.name = quest.info.id + "_button";
        RectTransform buttonRectTransform = questLogButton.GetComponent<RectTransform>();
        questLogButton.Initialize(quest.info.title, () => {
            selectAction();
            UpdateScrolling(buttonRectTransform);
        });
        idToButtonMap[quest.info.id] = questLogButton;
        return questLogButton;
    }

    void UpdateScrolling(RectTransform buttonRectTransform)
    {
        float buttonYMin = Mathf.Abs(buttonRectTransform.anchoredPosition.y);
        float buttonYMax = buttonYMin + buttonRectTransform.rect.height;

        float contentYMin = contentRectTransform.anchoredPosition.y;
        float contentYMax = contentYMin + scrollRectTransform.rect.height;

        if (buttonYMax > contentYMax)
        {
            contentRectTransform.anchoredPosition = new Vector2(
                contentRectTransform.anchoredPosition.x,
                buttonYMax - scrollRectTransform.rect.height
            );
        }
        else if (buttonYMin < contentYMin) 
        {
            contentRectTransform.anchoredPosition = new Vector2(
                contentRectTransform.anchoredPosition.x,
                buttonYMin
            );
        }
    }
}
