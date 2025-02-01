using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestLogScrollingList : MonoBehaviour
{
    [Header("Componentes")] [SerializeField]
    GameObject contentParent;

    [Header("Quest Log Butoon")] [SerializeField]
    GameObject questLogButtonPrefab;

    [Header("React Transforms")] [SerializeField]
    RectTransform scrollRectTransform;

    [SerializeField] RectTransform contentRectTransform;

    readonly Dictionary<string, QuestLogButton> idToLogButtonMap = new();

    // Remover depois do teste
    // void Start()
    // {
    //     for (var i = 0; i < 20; i++)
    //     {
    //         var questInfoSoTest = ScriptableObject.CreateInstance<QuestInfoSO>();
    //         questInfoSoTest.Id = "teste_" + i;
    //         questInfoSoTest.displayName = "teste " + i;
    //         questInfoSoTest.questStepsPrefabs = new GameObject[0];
    //         var quest = new Quest(questInfoSoTest);
    //
    //         var questLogButton =
    //             CreateButtonIfNotExist(quest, () => { Debug.Log($"Selected: {questInfoSoTest.displayName}"); });
    //
    //         if (i == 0) questLogButton.button.Select();
    //     }
    // }

    public QuestLogButton CreateButtonIfNotExist(Quest quest, UnityAction selectAction)
    {
        QuestLogButton questLogButton = null;

        if (!idToLogButtonMap.ContainsKey(quest.info.Id))
            questLogButton = InstantiateQuestLogButton(quest, selectAction);
        else
            questLogButton = idToLogButtonMap[quest.info.Id];
        return questLogButton;
    }

    QuestLogButton InstantiateQuestLogButton(Quest quest, UnityAction selectAction)
    {
        var questLogButton = Instantiate(
            questLogButtonPrefab,
            contentParent.transform).GetComponent<QuestLogButton>();

        questLogButton.gameObject.name = quest.info.Id + "_button";

        var buttonRectTransform = questLogButton.GetComponent<RectTransform>();

        questLogButton.Initialize(quest.info.displayName, () =>
        {
            selectAction();
            UpdateScrolling(buttonRectTransform);
        });

        idToLogButtonMap[quest.info.Id] = questLogButton;

        return questLogButton;
    }

    void UpdateScrolling(RectTransform buttonRectTransform)
    {
        var buttonYMin = Mathf.Abs(buttonRectTransform.anchoredPosition.y);
        var buttonYMax = buttonYMin + buttonRectTransform.rect.height;

        var contentYMin = contentRectTransform.anchoredPosition.y;
        var contentYMax = contentYMin + scrollRectTransform.rect.height;

        if (buttonYMax > contentYMax)
            contentRectTransform.anchoredPosition = new Vector2(contentRectTransform.anchoredPosition.x,
                buttonYMax - scrollRectTransform.rect.height);

        else if (buttonYMin < contentYMin)
            contentRectTransform.anchoredPosition = new Vector2(contentRectTransform.anchoredPosition.x,
                buttonYMin);
    }
}