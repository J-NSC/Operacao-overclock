using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;


public class QuestLogButton : MonoBehaviour, ISelectHandler
{
    public Button button { get; private set; }
    UnityAction onSelectAction;
    TextMeshProUGUI buttoText;

    public void Initialize(string displayName,UnityAction onSelectAction)
    {
        button = GetComponent<Button>();
        buttoText = GetComponentInChildren<TextMeshProUGUI>();
        
        buttoText.text = displayName;
        this.onSelectAction = onSelectAction;
    }
    
    public void OnSelect(BaseEventData eventData)
    {
        onSelectAction();
    }

    public void SetState(QuestState state)
    {
        Debug.Log(state.ToString());
        switch (state)
        {
            case QuestState.REQUIREMENTS_NOT_MET:
            case QuestState.CAN_START:
                buttoText.color = Color.red;
                break;
            case QuestState.IN_PROGRESS:
                
            case QuestState.CAN_FINISH:
                buttoText.color = Color.yellow;
                break;
            case QuestState.FINISHED:
                buttoText.color = Color.green;
                break;
            default:
                Debug.LogWarning("Quest State not recognized");
                break;
        }
    }
}
